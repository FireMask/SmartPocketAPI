import { ref, onMounted, inject, computed } from 'vue';
import { defineStore } from 'pinia';
import MovementsAPI from '../api/MovementsAPI';

export const useMovementsStore = defineStore( 'movements', () => {
    const toast = inject('toast')
    
    const categories = ref([]);
    const types = ref([]);
    const userPendingMovements = ref([]);
    const userMovementsPaginate = ref([]);
    const paymentTypeId = 1;
    const incomeTypeId = 2;
    const purchaseTypeId = 3;
    const creditCardPaymentTypeId = 4;
    
    //DashboardData
    const userMovements = ref([]);
    const pendingRecurringMovementsCount = ref(0);
    const summaryByPaymentMethods = ref(0);
    const monthIncome = ref(0);
    const monthMovementsCount = ref(0);
    const monthSpent = ref(0);


    onMounted(async () => {
        try {
            await getDashboard();
            await getCategories();
            await getTypes();
            await getPendingMovements();
            await getMovements();
        } catch (error) {
            console.log(error);
        }
    })

    const reload = async () => {
        try {
            await getDashboard();
            await getCategories();
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
            
            userMovements.value = top20Movements;
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
        const {data} = await MovementsAPI.all()
        userMovementsPaginate.value = data.data;
    }

    const getCategories = async () => {
        const {data} = await MovementsAPI.categories();
        categories.value = data.data.map(category => { return {label: category.name, value: category.id, id: category.id}})
    }

    const getTypes = async () => {
        const {data} = await MovementsAPI.types();
        types.value = data.data.map(type => { return {label: type.name, value: type.id, id: type.id}})
    }

    const getPendingMovements = async () => {
        const {data} = await MovementsAPI.pendingMovements();
        userPendingMovements.value = data.data;
    }

    const AddPendingMovement = async(pendingData) => {
        try {
            const { data } = await MovementsAPI.CreatePendingMovement(pendingData)
            
            if(data.success){
                toast.open({ message: 'Movement added', type: 'success' });
                await getPendingMovements();
                await getDashboard();
                await getMovements();
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
                await getDashboard();
                await getMovements();
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

                await getDashboard();
                await getMovements();
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
            
            if(data.success) {
                await getDashboard();
                await getMovements();
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
        userMovements,
        userPendingMovements,
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
        userMovementsPaginate,
        addMovement,
        deleteMovement,
        updateMovement,
        getMovements,
        AddCategory,
        AddPendingMovement,
        reload,
    }
})