<script setup>
    import { ref, computed } from 'vue';
    import { useRouter } from 'vue-router';
    import { formatShowDate, formatMoney } from '../../helpers';
    import { useRecurringPaymentsStore } from '../../stores/recurringPayments';
    import { GrEdit, GrTrash } from 'vue-icons-plus/Gr';

    const router = useRouter()
    const store = useRecurringPaymentsStore()
    const dateFilter = ref();
    const filteredInfo = ref({});
    const loading = ref(false);

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
            },
            {
                title: '',
                key: 'actions',
                name: 'actions',
                width: 60,
                align: 'right'
            },
        ]
    });

    const clearFilters = async () => {
        loading.value = true;
        dateFilter.value = null;
        filteredInfo.value = null;
        store.filters.pageNumber = 1
        store.filters.pageSize = 10,
            store.filters.isActive = null,
            store.filters.categoryId = [],
            store.filters.startDate = null
        store.filters.endDate = null
        store.filters.paymentMethodId = [],
            store.filters.untilDate = [],
            store.filters.hasPendingMovements = null,
            await store.getRecurringPayments();
        loading.value = false;
    };

    const filterByDates = async (date, dateString) => {
        loading.value = true;
        store.filters.startDate = dateString[0];
        store.filters.endDate = dateString[1];
        store.filters.pageNumber = 1
        await store.getRecurringPayments();
        loading.value = false;
    };

    const onChange = async (pagination, filters, sorter) => {
        loading.value = true;
        filteredInfo.value = filters;

        store.filters.pageNumber = pagination.current;

        if (store.filters.pageSize !== pagination.pageSize ||
            store.filters.categoryId !== filters.category ||
            store.filters.paymentMethodId !== filters.paymentMethod ||
            store.filters.isActive !== filters.isActive) 
        {
            store.filters.pageNumber = 1;
        }

        store.filters.pageSize = pagination.pageSize;
        store.filters.categoryId = filters.category;
        store.filters.paymentMethodId = filters.paymentMethod;
        store.filters.isActive = filters.isActive;

        await store.getRecurringPayments();
        loading.value = false;
    };

    const pagination = computed(() => ({
        total: store.totalCount,
        current: store.pageNumber,
        size: "middle",
        pageSize: store.pageSize,
        position: ['topRight']
    }));

    const edit = async recurring => {
        //store.movementToUpdate.value = movement
        //router.push({name : 'edit-movement', params: { id: movement.id }})
    };

</script>

<template>
    <div class="overflow-auto h-fit max-w-7xl">
        <div class="w-fit mt-2">
            <a-button type="link" class="p-0" @click='clearFilters'>Clear All filters</a-button>
        </div>

        <a-form-item class="w-fit mb-2" label="Filter by start date" name="dates">
            <a-range-picker v-model:value="dateFilter" @change="filterByDates" />
        </a-form-item>

        <a-table 
            :columns="columns" 
            :data-source="store.userRecurringPayments" 
            :pagination="pagination"
            :loading="loading" 
            @change="onChange" 
            size="small">
            <template #bodyCell="{ column, record }">
                <template v-if="column.key === 'startDate'">
                    <span class="text-xs">{{ formatShowDate(record.startDate) }} </span>
                </template>
                <template v-if="column.key === 'lastInstallmentDate'">
                    <span class="text-xs">{{ formatShowDate(record.lastInstallmentDate) }} </span>
                </template>
                <template v-if="column.key === 'nextInstallmentDate'">
                    <span class="text-xs">{{ formatShowDate(record.nextInstallmentDate) }} </span>
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
        </a-table>
    </div>
</template>