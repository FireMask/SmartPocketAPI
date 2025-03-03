import { ref, onMounted, computed, inject } from 'vue';
import { defineStore } from 'pinia';
import RecurringPaymentAPI from '../api/RecurringPaymentAPI';
import CatalogsAPI from '@/api/CatalogsAPI';

export const useRecurringPaymentsStore = defineStore( 'recurringPayments', () => {

    const frequencies = ref([])
    const toast = inject('toast')
    
    //Pending table
    const userPendingMovements = ref([]);
    //Recurring table 
    const userRecurringPayments = ref([]);
    //Pagination
    const filters = ref(
    {
        pageNumber: 1, 
        pageSize: 10,
        isActive: null,
        categoryId: [],
        startDate: null, //YYYY-MM-DD
        endDate: null, //YYYY-MM-DD
        paymentMethodId: [],
        untilDate: null,
        hasPendingMovements: null,
    })

    const pageNumber = ref(1)
    const pageSize = ref(10)
    const totalCount = ref(0)
    const totalPages = ref(0)
    const filterCatalogs = ref({})

    onMounted(async () => {
        try {
            await getRecurringPayments();
            await getPendingMovements();
            await getFrequencies();
            await getFilters();
        } catch (error) {
            console.log(error);
        }
    })

    const getRecurringPayments = async () => {
        userRecurringPayments.value = await getData({...filters.value, hasPendingMovements: null});
    }

    const getPendingMovements = async () => {           
        userPendingMovements.value = await getData({...filters.value, hasPendingMovements: true});
    }

    const getData = async (filters) => {
        const {data: { data : { items, pageNumber: pn, totalCount: tc, totalPages:tp, pageSize:pz } }} = await RecurringPaymentAPI.paginate(filters)            
        pageNumber.value = pn
        totalCount.value = tc
        totalPages.value = tp
        pageSize.value = pz

        return items;
    }

    const getFrequencies = async () => {
        const {data} = await RecurringPaymentAPI.frequencies()
        frequencies.value = data.data.map(freq => { return {label: freq.name, value: freq.id, id: freq.id}})      
    }

    const getFilters = async () => {
        const {data: {data : {categories, frequencies, paymentMethods, movementTypes}}} = await CatalogsAPI.filterCatalogs();
        filterCatalogs.value.categories = categories.map(item => { return {label: item.name, text: item.name, value: item.id, id: item.id}})
        filterCatalogs.value.frequencies = frequencies.map(item => { return {label: item.name, text: item.name, value: item.id, id: item.id}})
        filterCatalogs.value.paymentMethods = paymentMethods.map(item => { return {label: item.name, text: item.name, value: item.id, id: item.id}})
        filterCatalogs.value.movementTypes = movementTypes.map(item => { return {label: item.name, text: item.name, value: item.id, id: item.id}})
    }

    const addRecurringPayment = async (paymentData) => {
        try {
            const { data } = await RecurringPaymentAPI.create(paymentData)
            
            if(data.success){
                await getRecurringPayments();
                await getPendingMovements();
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

    const deleteRecurringPayment = async(id) => {
        if(confirm('¿Do you want to delete this recurring payment?')) {
            try {
                const { data } = await RecurringPaymentAPI.delete(id)
                toast.open({
                    message: 'Deleted',
                    type: 'success'
                })

                await getRecurringPayments();
            } catch (error)
            {
                console.log(error);
                toast.open({ message: error.response.data.message, type: 'error' })
            }
        }
    }

    async function updateRecurringPayment(paymentData) {
        console.log("data", paymentData);
    }

    return {
        userRecurringPayments,
        userPendingMovements,
        frequencies,
        pageNumber,
        pageSize,
        totalCount,
        totalPages,
        filters,
        filterCatalogs,
        addRecurringPayment,
        deleteRecurringPayment,
        getRecurringPayments,
        getPendingMovements,
    }
})