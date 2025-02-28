<script setup>
    import { ref, reactive, watch, computed } from 'vue';
    import { useMovementsStore } from '../../stores/movements';
    import { formatShowDate, formatMoney } from '../../helpers';
    import { GrEdit, GrTrash } from 'vue-icons-plus/Gr';
    import { AiOutlineMinusCircle, AiOutlinePlusCircle } from 'vue-icons-plus/ai';
    import { SearchOutlined, DeleteOutlined } from '@ant-design/icons-vue';
    
    const store = useMovementsStore()
    const searchInput = ref();
    const dateFilter = ref();
    
    const columns = ref([
        {
            title: 'Date',
            key: 'date',
            dataIndex: 'movementDate',
            width: 150,
        },
        {
            title: 'Type',
            key: 'type',
            name: 'type',
            filters: [],
            filterMultiple: true,
            width: 155,
        },
        {
            title: 'Category',
            key: 'category',
            name: 'category',
            filters: [],
            filterMultiple: true,
            width: 130,
        },
        {
            title: 'Description',
            dataIndex: 'description',
            key: 'description',
            customFilterDropdown: true,
            onFilter: (value, record) => console.log('onFilter desc',value, record),
            onFilterDropdownOpenChange: visible => {
            if (visible) {
                setTimeout(() => {
                searchInput.value.focus();
                }, 100);
            }
            },
        },
        {
            title: 'Payment Method',
            key: 'paymentMethod',
            name: 'paymentMethod',
            filters: [],
            filterMultiple: true,
            width: 160,
        },
        {
            title: 'Amount',
            dataIndex: 'amount',
            key: 'amount',
            width: 100,
        },
        {
            title: '',
            key: 'actions',
            name: 'actions',
            width: 100,
        },
    ]);

    const clearFilters = async () => {
        store.filters.pageNumber = 1
        store.filters.pageSize = 10,
        store.filters.search = "", 
        store.filters.categoryId = [],
        store.filters.paymentMethodId = [],
        store.filters.movementTypeId = [],
        store.filters.startDate = null, 
        store.filters.endDate = null 
        await store.getMovements();
    };

    const filterByDates = async (date, dateString) => {
        console.log(dateString);
        
        store.filters.startDate = dateString[0];
        store.filters.endDate = dateString[1];
        store.filters.pageNumber = 1
        await store.getMovements();
    };

    const onChange = async (pagination, filters, sorter) => {
        store.filters.pageNumber = pagination.current;
        store.filters.categoryId = filters.category;
        store.filters.paymentMethodId = filters.paymentMethod;
        store.filters.movementTypeId = filters.type;

        loading.value = true;
        await store.getMovements();
        loading.value = false;
    };

    const handleSearch = async (selectedKeys, confirm, dataIndex) => {
        store.filters.search = selectedKeys;
        store.filters.pageNumber = 1
        await store.getMovements();
    };

    const handleReset = async clearFilters => {
        console.log('handleReset', clearFilters); 
        searchInput.value = ''
        store.filters.search = '';
        store.filters.pageNumber = 1
        await store.getMovements();
        clearFilters({
            confirm: true,
            closeDropdown: true
        });
    };

    watch(store.filterCatalogs, (newFilters) => {
        if(newFilters)
        {
            console.log(newFilters);
            
            columns.value = columns.value.map(column => {
                if (column.key === 'category') {
                    return {
                        ...column,
                        filters: newFilters.categories,
                    };
                }
                else if (column.key === 'paymentMethod') {
                    return {
                        ...column,
                        filters: newFilters.paymentMethods,
                    };
                }
                else if (column.key === 'type') {
                    return {
                        ...column,
                        filters: newFilters.movementTypes,
                    };
                }
                return column;
            });
        }
    });

    const pagination = computed(() => ({
        total: store.totalCount,
        current: store.pageNumber,
        pageSize: 10,
        position: ['topRight'],
        onChange: (page, pageSize) => {
            console.log(page, pageSize);
          }
    }));

    const loading = ref(false);
    
</script>

<template>
    <header class="flex sticky justify-between mb-4 flex-col lg:flex-row">
        <h3 class="text-3xl font-medium text-gray-700">
            Movements
        </h3>
        <div class="w-auto text-lg lg:text-xl mt-3 lg:mt-0">
            <p>
                <RouterLink :to="{ name: 'new-movement' }" class="text-indigo-600 hover:text-indigo-900 font-medium">
                    New Movement
                </RouterLink>
            </p>
            <p>
                <RouterLink :to="{ name: 'new-recurring-payment' }"
                    class="text-indigo-600 hover:text-indigo-900 font-medium">
                    New Recurring payment
                </RouterLink>
            </p>
        </div>
    </header>

    <main class="max-w-6xl">
        <a-form-item
            class="w-fit m-0"
            label="Filter movements by dates"
            name="dates">
            <a-range-picker v-model:value="dateFilter" @change="filterByDates"/>
        </a-form-item>
        <div class="w-fit mt-2">
            <a-button type="link" class="p-0" @click='clearFilters'>Clear All filters</a-button>
        </div>

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
                        <RouterLink :to="{ name: 'edit-movement', params: { id: record.id } }" class="cursor-pointer rounded-full text-cyan-700 p-2 h-fit">
                            <GrEdit size="17"/>
                        </RouterLink>
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