import { ref, onMounted, inject, computed } from 'vue';
import { defineStore } from 'pinia';
import MovementsAPI from '../api/MovementsAPI';

export const useMovementsStore = defineStore( 'movements', () => {
    const toast = inject('toast')
    
    const categories = ref([]);
    const types = ref([]);
    const frequencies = ref([]);
    const paymentTypeId = 1;
    const incomeTypeId = 2;
    const purchaseTypeId = 3;
    const creditCardPaymentTypeId = 4;

    //DashboardData
    const userMovements = ref([]);
    const pendingRecurringMovements = ref([]);
    const summaryByPaymentMethods = ref([]);
    const monthIncome = ref(0);
    const monthMovementsCount = ref(0);
    const monthSpent = ref(0);


    onMounted(async () => {
        try {
            getDashboard();
            getCategories();
            getTypes();
            getFrequencies();
        } catch (error) {
            console.log(error);
        }
    })

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
            
            userMovements.value = top20Movements;
            pendingRecurringMovements.value = pendingMovementsRecurring
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
        const {data} = await MovementsAPI.all()
        userMovements.value = data.data;
    }

    const getCategories = async () => {
        const {data} = await MovementsAPI.categories();
        categories.value = data.data.map(category => { return {label: category.name, value: category.id, id: category.id}})
    }

    const getTypes = async () => {
        const {data} = await MovementsAPI.types();
        types.value = data.data.map(type => { return {label: type.name, value: type.id, id: type.id}})
    }

    const getFrequencies = async () => {
        const {data} = await MovementsAPI.frecuencies();
        frequencies.value = data.data.map(type => { return {label: type.name, value: type.id, id: type.id}})
    }

    async function addMovement(movementData) {
        try {
            console.log('store', movementData);
            
            const { data } = await MovementsAPI.create(movementData)
            console.log("data", data);
            
            if(data.success)
                await getDashboard();
            else 
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

                await getDashboard();
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
            console.log("movement data response", data);
            
            if(data.success)
                await getDashboard();
            else 
                toast.open({ message: message, type: 'error' });
            return data;
        } catch (error)
        {
            console.log(error);
            toast.open({ message: error.response.data.message, type: 'error' })
        }
    }

    return {
        userMovements,
        pendingRecurringMovements,
        summaryByPaymentMethods,
        monthIncome,
        monthMovementsCount,
        monthSpent,
        categories,
        types,
        frequencies,
        paymentTypeId,
        incomeTypeId,
        purchaseTypeId,
        creditCardPaymentTypeId,
        addMovement,
        deleteMovement,
        updateMovement,
        getMovements,
    }
})