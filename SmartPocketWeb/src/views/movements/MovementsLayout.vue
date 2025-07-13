<script setup>
    import { ref, computed } from 'vue';
    import { useRouter } from 'vue-router';
    import { useMovementsStore } from '../../stores/movements';
    import { formatShowDate, formatMoney } from '../../helpers';
    import { GrEdit, GrTrash } from 'vue-icons-plus/Gr';
    import { AiOutlineMinusCircle, AiOutlinePlusCircle } from 'vue-icons-plus/ai';
    import { SearchOutlined, DeleteOutlined } from '@ant-design/icons-vue';
    import PageHeader from '@/components/PageHeader.vue';
    
    const router = useRouter()
    const store = useMovementsStore()
    const searchInput = ref();
    const dateFilter = ref();
    const filteredInfo = ref({});
    const loading = ref(false);

    const headerButtons = [
        { name: 'new-movement', text: 'New Movement' },
        { name: 'new-recurring-payment', text: 'New Recurring payment' },
    ]
    
    const columns = computed(() => {
        const filtered = filteredInfo.value || {};
        return [
            {
                title: 'Date',
                key: 'date',
                dataIndex: 'movementDate',
                width: 100,
            },
            {
                title: 'Type',
                key: 'type',
                name: 'type',
                filteredValue: filtered.type || null,
                filters: store.filterCatalogs?.movementTypes,
                filterMultiple: true,
                width: 145,
                ellipsis: true,
            },
            {
                title: 'Category',
                key: 'category',
                name: 'category',
                filteredValue: filtered.category || null,
                filters: store.filterCatalogs?.categories,
                filterMultiple: true,
                width: 130,
                ellipsis: true,
            },
            {
                title: 'Description',
                dataIndex: 'description',
                key: 'description',
                width: 200,
                filteredValue: filtered.description || null,
                customFilterDropdown: true,
                onFilterDropdownOpenChange: visible => {
                    if (visible) {
                        setTimeout(() => {
                        searchInput.value.focus();
                        }, 100);
                    }
                },
                ellipsis: true,
            },
            {
                title: 'Payment Method',
                key: 'paymentMethod',
                name: 'paymentMethod',
                filteredValue: filtered.paymentMethod || null,
                filters: store.filterCatalogs?.paymentMethods,
                filterMultiple: true,
                width: 160,
                ellipsis: true,
            },
            {
                title: 'Amount',
                dataIndex: 'amount',
                key: 'amount',
                width: 90,
            },
            {
                title: '',
                key: 'actions',
                name: 'actions',
                width: 100,
            },
        ]}
    );

    const clearFilters = async () => {
        loading.value = true;
        filteredInfo.value = null;
        searchInput.value = ''
        store.filters.pageNumber = 1
        store.filters.pageSize = 10,
        store.filters.search = "", 
        store.filters.categoryId = [],
        store.filters.paymentMethodId = [],
        store.filters.movementTypeId = [],
        store.filters.startDate = null, 
        store.filters.endDate = null 
        await store.getMovements();
        loading.value = false;
    };

    const filterByDates = async (date, dateString) => {
        loading.value = true;
        store.filters.startDate = dateString[0];
        store.filters.endDate = dateString[1];
        store.filters.pageNumber = 1
        await store.getMovements();
        loading.value = false;
    };

    const onChange = async (pagination, filters, sorter) => {
        loading.value = true;
        filteredInfo.value = filters;

        store.filters.pageNumber = pagination.current;

        if(store.filters.pageSize !== pagination.pageSize ||
            store.filters.categoryId !== filters.category ||
            store.filters.paymentMethodId !== filters.paymentMethod ||
            store.filters.movementTypeId !== filters.type ) 
        {
            store.filters.pageNumber = 1;
        }

        store.filters.pageSize = pagination.pageSize;
        store.filters.categoryId = filters.category;
        store.filters.paymentMethodId = filters.paymentMethod;
        store.filters.movementTypeId = filters.type;

        await store.getMovements();
        loading.value = false;
    };

    const handleSearch = async (selectedKeys, confirm, dataIndex) => {
        loading.value = true;
        store.filters.search = selectedKeys;
        store.filters.pageNumber = 1
        await store.getMovements();
        loading.value = false;
    };

    const handleReset = async clearFilters => {
        loading.value = true;
        searchInput.value = ''
        store.filters.search = '';
        store.filters.pageNumber = 1
        store.filters.pageSize = 10
        await store.getMovements();
        clearFilters({
            confirm: true,
            closeDropdown: true
        });
        loading.value = false;
    };

    const pagination = computed(() => ({
        total: store.totalCount,
        current: store.pageNumber,
        size: "middle",
        pageSize: store.pageSize,
        showSizeChanger: true,
        position: ['topRight']
    }));

    const edit = async movement => {
        store.movementToUpdate.value = movement
        router.push({name : 'edit-movement', params: { id: movement.id }})
    };
    
</script>

<template>
    <PageHeader title="Movements" :buttons="headerButtons"/>

    <main class="max-w-6xl">
        <div class="w-fit mt-2">
            <a-button type="link" class="p-0" @click='clearFilters'>Clear All filters</a-button>
        </div>

        <a-form-item
            class="w-fit mb-2"
            label="Filter movements by dates"
            name="dates">
            <a-range-picker v-model:value="dateFilter" @change="filterByDates"/>
        </a-form-item>   
        
        <a-table 
            :columns="columns" 
            :data-source="store.userMovements" 
            :pagination="pagination"
            :loading="loading"
            @change="onChange" 
            size="small">
            <template #bodyCell="{ column, record }">
                <template v-if="column.key === 'date'">
                    <div class="h-11">
                        {{ formatShowDate(record.movementDate) }}
                    </div>
                </template>
                <template v-if="column.key === 'category'">
                    {{ record.category.name }}
                </template>
                <template v-else-if="column.key === 'paymentMethod'">
                    {{ record.paymentMethod.name }}
                    <div class="text-sm text-gray-500" v-if="record.installmentNumber">
                        <span v-if="record.recurringPayment?.installmentCount > 0"> Installment {{ record.installmentNumber }} / {{ record.recurringPayment?.installmentCount }} </span>
                        <span v-else>Payment # {{ record.installmentNumber }}</span>
                    </div>
                </template>
                <template v-else-if="column.key === 'type'">
                    <div class="flex items-center">
                        <div v-if="record.movementTypeId == store.incomeTypeId" class="w-5 h-5 rounded-full text-lime-600 bg-green-100 flex items-center">
                        <AiOutlinePlusCircle />
                        </div>
                        <div v-else class="w-5 h-5 rounded-full text-red-600 bg-red-100 flex items-center">
                        <AiOutlineMinusCircle />
                        </div>

                        <div class="ml-2">
                            <div class="font-normal text-" :class="record.creditCardPaymentId? 'text-xs' : 'text-sm'">{{ record.movementType.name }}</div>
                            <div class="text-gray-500" v-if="record.creditCardPaymentId">
                                {{ record.creditCardPayment?.name }}
                            </div>
                        </div>
                    </div>
                </template>
                <template v-else-if="column.key === 'amount'">
                    <div class="font-semibold">
                        {{ formatMoney(record.amount) }}
                    </div>
                </template>
                <template v-else-if="column.key === 'actions'" class="font-semibold">
                    <div class="flex flex-row space-x-2 h-fit text-gray-500">
                        <div @click="edit(record)" class="cursor-pointer rounded-full text-cyan-700 hover:text-sky-600 p-2 h-fit">
                            <GrEdit size="17"/>
                        </div>
                        <div @click="store.deleteMovement(record.id)" class="cursor-pointer rounded-full text-rose-600 hover:text-red-500 p-2 h-fit">
                            <GrTrash size="17"/>
                        </div>
                    </div>
                </template>
            </template>
            <template #customFilterDropdown="{ setSelectedKeys, selectedKeys, confirm, clearFilters, column }" >
                <div style="padding: 8px">
                    <a-input
                        ref="searchInput"
                        :placeholder="`Search ${column.dataIndex}`"
                        :value="selectedKeys[0]"
                        style="width: 188px; margin-bottom: 8px; display: block"
                        @change="e => setSelectedKeys(e.target.value ? [e.target.value] : [])"
                        @pressEnter="handleSearch(selectedKeys, confirm, column.dataIndex)"
                    />
                    <a-button
                        type="primary"
                        size="small"
                        style="width: 90px; margin-right: 8px"
                        @click="handleSearch(selectedKeys, confirm, column.dataIndex)"
                    >
                    <template #icon><SearchOutlined /></template>
                        Search
                    </a-button>
                    <a-button size="small" style="width: 90px" @click="handleReset(clearFilters)">
                        Reset
                    </a-button>
                </div>
            </template>
            <template #customFilterIcon="{ filtered }">
                <search-outlined :style="{ color: filtered ? '#108ee9' : undefined }" />
            </template>
        </a-table>
    </main>
</template>