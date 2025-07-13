import { ref, onMounted, inject, computed } from 'vue';
import { defineStore } from 'pinia';
import MovementsAPI from '../api/MovementsAPI';
import CatalogsAPI from '@/api/CatalogsAPI';
import { useRecurringPaymentsStore } from './recurringPayments';

export const useMovementsStore = defineStore( 'movements', () => {
    const toast = inject('toast')

    const recurringPaymentsStore = useRecurringPaymentsStore();
    
    const categories = ref([]);
    const types = ref([]);
    const paymentTypeId = 1;
    const incomeTypeId = 2;
    const purchaseTypeId = 3;
    const creditCardPaymentTypeId = 4;
    
    //DashboardData
    const topMovements = ref([]);
    const pendingRecurringMovementsCount = ref(0);
    const summaryByPaymentMethods = ref(0);
    const monthIncome = ref(0);
    const monthMovementsCount = ref(0);
    const monthSpent = ref(0);

    //Movements table
    const userMovements = ref([])
    const filters = ref(
    {
        pageNumber: 1, 
        pageSize: 10,
        search: "", //description
        categoryId: [],
        paymentMethodId: [],
        movementTypeId: [],
        startDate: null, //YYYY-MM-DD
        endDate: null //YYYY-MM-DD
    })
    const pageNumber = ref(1)
    const pageSize = ref(10)
    const totalCount = ref(0)
    const totalPages = ref(0)
    const movementToUpdate = ref({});
    const filterCatalogs = ref({})

    onMounted(async () => {
        reload();
    })

    const reload = async () => {
        try {
            await getCategories();
            await getTypes();
            await getFilters();
            await reloadMovements();
        } catch (error) {
            console.log(error);
        }
    }

    const reloadMovements = async () => {
        try {
            await getDashboard();
            await getPendingMovements();
            await getMovements();
        } catch (error) {
            console.log(error);
        }
    }

    const getDashboard = async () => {
        try {
            const {data: {data : {
                top20Movements, 
                pendingMovementsRecurring, 
                summaryPaymentMethods,
                thisMonthIncome,
                thisMonthMovementsCount,
                thisMonthSpent
            }}} = await MovementsAPI.dashboard()
            
            topMovements.value = top20Movements;
            pendingRecurringMovementsCount.value = pendingMovementsRecurring
            summaryByPaymentMethods.value = summaryPaymentMethods
            monthIncome.value =thisMonthIncome
            monthMovementsCount.value = thisMonthMovementsCount
            monthSpent.value = thisMonthSpent
        }
        catch (error)
        {
            console.log(error);
            toast.open({ message: error.response.data.message, type: 'error' })
        }
    }

    const getMovements = async () => {
        try {            
            const {data: { data : { items, pageNumber: pn, totalCount: tc, totalPages:tp, pageSize:pz } }} = await MovementsAPI.paginate(filters.value)            
            userMovements.value = items;
            pageNumber.value = pn
            totalCount.value = tc
            totalPages.value = tp
            pageSize.value = pz
        } catch (error)
        {
            console.log(error);
            toast.open({ message: error?.response?.data?.message, type: 'error' })
        }   
    }

    const getCategories = async () => {
        const {data} = await MovementsAPI.categories();
        categories.value = data.data
            .map(category => { return {label: category.name, text: category.name, value: category.id, id: category.id}})
            .sort(function(a, b) {
                var nameA = a.label.toUpperCase();
                var nameB = b.label.toUpperCase();
                return (nameA < nameB) ? -1 : (nameA > nameB) ? 1 : 0;
            });
    }

    const getTypes = async () => {
        const {data} = await MovementsAPI.types();
        types.value = data.data.map(type => { return {label: type.name, value: type.id, id: type.id}})
    }

    const getFilters = async () => {
        const {data: {data : {categories, frequencies, paymentMethods, movementTypes}}} = await CatalogsAPI.filterCatalogs();
        filterCatalogs.value.categories = categories.map(item => { return {label: item.name, text: item.name, value: item.id, id: item.id}})
        filterCatalogs.value.frequencies = frequencies.map(item => { return {label: item.name, text: item.name, value: item.id, id: item.id}})
        filterCatalogs.value.paymentMethods = paymentMethods.map(item => { return {label: item.name, text: item.name, value: item.id, id: item.id}})
        filterCatalogs.value.movementTypes = movementTypes.map(item => { return {label: item.name, text: item.name, value: item.id, id: item.id}})
    }

    const getPendingMovements = async () => {
        recurringPaymentsStore.reload();
    }

    const AddPendingMovement = async(pendingData) => {
        try {
            const { data } = await MovementsAPI.CreatePendingMovement(pendingData)
            
            if(data.success){
                await reloadMovements();
            }
            else 
                toast.open({ message: message, type: 'error' });
            return data;
        } catch (error)
        {
            console.log(error);
            toast.open({ message: error.response.data.message, type: 'error' })
        }
    }

    const AddCategory = async(categoryData) => {
        try {
            const { data } = await MovementsAPI.createCategory(categoryData)
            
            if(data.success){
                toast.open({ message: 'Category added', type: 'success' });
                await getCategories();
            }else 
                toast.open({ message: message, type: 'error' });
            return data;
        } catch (error)
        {
            console.log(error);
            toast.open({ message: error.response.data.message, type: 'error' })
        }
    }

    async function addMovement(movementData) {
        try {
            const { data } = await MovementsAPI.create(movementData)
            
            if(data.success){
                await reloadMovements();
            } else 
                toast.open({ message: message, type: 'error' });
            return data;
        } catch (error)
        {
            console.log(error);
            toast.open({ message: error.response.data.message, type: 'error' })
        }
    }

    async function deleteMovement(id) {
        if(confirm('¿Do you want to delete this movement?')) {
            try {
                const { data } = await MovementsAPI.delete(id)
                toast.open({
                    message: 'Deleted',
                    type: 'success'
                })

                await reloadMovements();
            } catch (error)
            {
                console.log(error);
                toast.open({ message: error.response.data.message, type: 'error' })
            }
        }
    }

    async function updateMovement(movementData) {
        try {
            const { data } = await MovementsAPI.update(movementData)
            
            if(data.success) {
                await reloadMovements();
            } else 
                toast.open({ message: message, type: 'error' });
            return data;
        } catch (error)
        {
            console.log(error);
            toast.open({ message: error.response.data.message, type: 'error' })
        }
    }

    return {
        filterCatalogs,
        userMovements,
        pageNumber,
        pageSize,
        totalCount,
        totalPages,
        filters,
        topMovements,
        pendingRecurringMovementsCount,
        summaryByPaymentMethods,
        monthIncome,
        monthMovementsCount,
        monthSpent,
        categories,
        types,
        paymentTypeId,
        incomeTypeId,
        purchaseTypeId,
        creditCardPaymentTypeId,
        movementToUpdate,
        addMovement,
        deleteMovement,
        updateMovement,
        getMovements,
        AddCategory,
        AddPendingMovement,
        reload,
    }
})