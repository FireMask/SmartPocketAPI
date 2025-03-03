<script setup>
import { ref, computed } from 'vue';
import { formatMoney, formatShowDate } from '../../helpers';
import { useRecurringPaymentsStore } from '../../stores/recurringPayments';
import { useMovementsStore } from '@/stores/movements';
import { GrEdit, GrTrash } from 'vue-icons-plus/Gr';
import { RiStickyNoteAddLine } from 'vue-icons-plus/ri';
import { QuestionCircleOutlined, EditOutlined } from '@ant-design/icons-vue';
import { message } from 'ant-design-vue';
import dayjs from 'dayjs';

const store = useRecurringPaymentsStore()
const movementsStore = useMovementsStore()
const filteredInfo = ref({});
const loading = ref(false);
const expandedRowKeys = ref([]);
const editDate = ref();

const dateFormat = 'YYYY-MM-DD';

const columns = computed(() => {
	const filtered = filteredInfo.value || {};
	return [
		{
			title: 'Payment Method',
			key: 'paymentMethod',
			name: 'paymentMethod',
			dataIndex: 'paymentMethodName',
			filteredValue: filtered.paymentMethod || null,
			filters: store.filterCatalogs?.paymentMethods,
			filterMultiple: true,
			width: 160,
			ellipsis: true,
			align: 'center',
		},
		{
			title: 'Category',
			key: 'category',
			name: 'category',
			dataIndex: 'categoryName',
			filteredValue: filtered.category || null,
			filters: store.filterCatalogs?.categories,
			filterMultiple: true,
			width: 130,
			ellipsis: true,
			align: 'center',
		},
		{
			title: 'Description',
			dataIndex: 'description',
			key: 'description',
		},
		{
			title: 'Frequency',
			key: 'frequency',
			name: 'frequency',
			dataIndex: 'frequencyName',
			width: 100,
		},
		{
			title: 'Amount',
			dataIndex: 'installmentAmountPerPeriod',
			key: 'installmentAmountPerPeriod',
			width: 100,
		},
		{
			title: 'Paid Count',
			dataIndex: 'paidCount',
			key: 'paidCount',
			width: 80,
		},
		{
			title: 'Start Date',
			key: 'startDate',
			name: 'startDate',
			dataIndex: 'startDate',
			width: 130,
		},
		{
			title: 'Last payment',
			key: 'lastInstallmentDate',
			name: 'lastInstallmentDate',
			dataIndex: 'lastInstallmentDate',
			width: 130,
		},
		{
			title: 'Next payment',
			key: 'nextInstallmentDate',
			name: 'nextInstallmentDate',
			dataIndex: 'nextInstallmentDate',
			width: 130,
		}
	]
});

const subColumns = computed(() => {
	return [
		{
			title: 'Movement Date',
			key: 'movementDate',
			name: 'movementDate',
			dataIndex: 'movementDate',
			width: 300
		},
		{
			title: 'Count',
			key: 'installmentNumber',
			name: 'installmentNumber',
			dataIndex: 'installmentNumber',
			width: 300
		},
		{
			title: 'Amount',
			key: 'amount',
			name: 'amount',
			dataIndex: 'amount',
			width: 300
		},
		{
			title: '',
			key: 'actions',
			name: 'actions',
			dataIndex: 'actions',
			width: 100
		},
	]
});

const clearFilters = async () => {
	loading.value = true;
	filteredInfo.value = null;
	store.filters.pageNumber = 1
	store.filters.pageSize = 10
	store.filters.isActive = true
	store.filters.categoryId = []
	store.filters.paymentMethodId = []
	store.filters.untilDate = null
	await store.getPendingMovements();
	loading.value = false;
};

const onChange = async (pagination, filters, sorter) => {
	loading.value = true;
	filteredInfo.value = filters;

	store.filters.pageNumber = pagination.current;

	if (store.filters.pageSize !== pagination.pageSize ||
		store.filters.categoryId !== filters.category ||
		store.filters.paymentMethodId !== filters.paymentMethod ||
		store.filters.isActive !== filters.isActive) {
		store.filters.pageNumber = 1;
	}

	store.filters.pageSize = pagination.pageSize;
	store.filters.categoryId = filters.category;
	store.filters.paymentMethodId = filters.paymentMethod;
	store.filters.isActive = filters.isActive;

	await store.getPendingMovements();
	loading.value = false;
};

const pagination = computed(() => ({
	total: store.totalCount,
	current: store.pageNumber,
	size: "middle",
	pageSize: store.pageSize,
	position: ['topRight']
}));

const handleExpand = (expanded, record) => {
	if (expanded) {
		expandedRowKeys.value.push(record.id); 
	} else {
    	expandedRowKeys.value = expandedRowKeys.value.filter(key => key !== record.id); 
  	}
};

const confirmDelete = async ( record) => {
	const deleted = await movementsStore.AddPendingMovement({...record, amount:0})

	if(deleted.success){
		message.success('Movement deleted');
		await store.getPendingMovements()
	}
	else
		message.error('An error occurred, try again later');
	
};

const addMovement = async ( record) => {
	const added = await movementsStore.AddPendingMovement(record)

	if(added.success){
		message.success('Movement added');
		await store.getPendingMovements()
	}
	else
		message.error('An error occurred, try again later');
	
};

const addEditedMovement = async ( record) => {
	const added = await movementsStore.AddPendingMovement({...record, movementDate: editDate.value.format(dateFormat)} )

	if(added.success){
		message.success('Movement added');
		await store.getPendingMovements()
	}
	else
		message.error('An error occurred, try again later');
	
};

const handleCopyDate = (currentDate) => {
	editDate.value = dayjs(currentDate, dateFormat)
}
</script>

<template>
	<div class="overflow-auto h-fit max-w-7xl">
		<div class="w-fit mt-2">
			<a-button type="link" class="p-0" @click='clearFilters'>Clear All filters</a-button>
		</div>

		<a-table 
			:columns="columns" 
			:data-source="store.userPendingMovements" 
			:pagination="pagination"
			:loading="loading" 
			@change="onChange" 
			size="small" 
			:rowKey="record => record.id"
			:expandedRowKeys="expandedRowKeys"
  			@expand="handleExpand"
			>
			<template #bodyCell="{ column, record }">
				<template v-if="column.key === 'startDate'">
					<span class="text-xs">{{ formatShowDate(record.startDate) }} </span>
				</template>
				<template v-else-if="column.key === 'lastInstallmentDate'">
					<span class="text-xs">{{ formatShowDate(record.lastInstallmentDate) }} </span>
				</template>
				<template v-else-if="column.key === 'nextInstallmentDate'">
					<span class="text-xs">{{ formatShowDate(record.nextInstallmentDate) }} </span>
				</template>
				<template v-else-if="column.key === 'installmentAmountPerPeriod'">
					<span class="text-xs">{{ formatMoney(record.installmentAmountPerPeriod) }} </span>
				</template>
				<template v-else-if="column.key === 'actions'" class="font-semibold">
					<div class="flex flex-row w-full justify-end space-x-2 h-fit text-gray-500">
						<div @click="edit(record)"
							class="cursor-pointer rounded-full text-cyan-700 hover:text-sky-600 p-0 h-fit">
							<GrEdit size="17" />
						</div>
						<div v-if="record.canDelete" @click="store.deleteRecurringPayment(record.id)"
							class="cursor-pointer rounded-full text-rose-600 hover:text-red-500 p-0 h-fit">
							<GrTrash size="17" />
						</div>
						<div v-else class="rounded-full text-gray-400 p-0 h-fit">
							<GrTrash size="17" />
						</div>
					</div>
				</template>
			</template>
			<template #expandedRowRender="{ record }">
				<div class="w-fit mx-auto">
					<a-table
						:data-source="record.pendingMovements" 
						:columns="subColumns"
						:show-header="false"
						:pagination="false">
						<template #bodyCell="{ column, record }">
							<template v-if="column.key === 'movementDate'">
								<span class="text-xs">{{ formatShowDate(record.movementDate) }} </span>
							</template>
							<template v-else-if="column.key === 'installmentNumber'">
								<div v-if="record.installmentCount <= 0" class="text-xs">
									<span>Payment #</span>
									<span>{{ record.installmentNumber }}</span>
								</div>
								<div v-else class="text-xs">
									<span>Installment </span>
									<span>{{ record.installmentNumber }}/{{ record.installmentCount }}</span>
								</div>
							</template>
							<template v-else-if="column.key === 'amount'">
								<span class="text-xs">{{ formatMoney(record.amount) }} </span>
							</template>
							<template v-else-if="column.key === 'actions'" class="font-semibold">
								<div class="flex flex-row w-full justify-end space-x-2 h-fit text-gray-500">
									<div @click="addMovement(record)"
										class="cursor-pointer rounded-full text-lime-700 hover:text-lime-600 p-0 h-fit">
										<RiStickyNoteAddLine size="17" />
									</div>
									<a-popconfirm @confirm="addEditedMovement(record)"
										class="max-w-sm"
										ok-text="Yes"
										cancel-text="No">
										<template #icon><edit-outlined style="color: darkcyan" /></template>
										<template #title>
											<p class="mb-3">Edit your movement before add</p>
											<a-form-item 
												class="mb-2 w-full"
												label="Dates" 
												:labelCol="{ span: 8 }"
												:wrapperCol="{ span: 16 }">
												<a-date-picker v-model:value="editDate" class="w-full"/>
											</a-form-item>
											<a-form-item 
												class="mb-2 w-full"
												label="Description" 
												:labelCol="{ span: 8 }"
												:wrapperCol="{ span: 16 }"
												>
												<a-input v-model:value="record.description" placeholder="Description" class="block w-full"/>
											</a-form-item>
											<a-form-item class="mb-2 w-full"
												label="Amount" 
												:labelCol="{ span: 8 }"
												:wrapperCol="{ span: 16 }">
												<a-input-number
													v-model:value="record.amount"
													class="block w-full"
													:formatter="value => {
														if (!value) return '$ 0.00'; 
														const num = parseFloat(value).toFixed(2); 
														return `$ ${num.replace(/\B(?=(\d{3})+(?!\d))/g, ',')}`;
													}"
													:parser="value => { return parseFloat(value.replace(/[^0-9.-]+/g, '')).toFixed(2) || '0.00'; }"
													/>
											</a-form-item>
										</template>
										<div class="cursor-pointer rounded-full text-cyan-700 hover:text-sky-600 p-0 h-fit">
											<GrEdit size="17" @click="handleCopyDate(record.movementDate)" />
										</div>
									</a-popconfirm>
									<a-popconfirm title="Are you sure ignore this pending movement?"
										@confirm="confirmDelete(record)"
										ok-text="Yes"
										cancel-text="No">
										<template #icon><question-circle-outlined style="color: red" /></template>
										<div class="cursor-pointer rounded-full text-rose-600 hover:text-red-500 p-0 h-fit">
											<GrTrash size="17" />
										</div>
									</a-popconfirm>
								</div>
							</template>
						</template>
					</a-table>
				</div>
			</template>
		</a-table>
	</div>
</template>
