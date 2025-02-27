import { ref, onMounted, computed, inject } from 'vue';
import { defineStore } from 'pinia';
import RecurringPaymentAPI from '../api/RecurringPaymentAPI';

export const useRecurringPaymentsStore = defineStore( 'recurringPayments', () => {

    const userRecurringPayments = ref([]);
    const frequencies = ref([])
    const toast = inject('toast')

    onMounted(async () => {
        try {
            await getRecurringPayments();
            await getFrequencies();
        } catch (error) {
            console.log(error);
        }
    })

    const getRecurringPayments = async () => {
        const {data} = await RecurringPaymentAPI.all()
        userRecurringPayments.value = data.data.items;
    }
    const getFrequencies = async () => {
        const {data} = await RecurringPaymentAPI.frequencies()
        frequencies.value = data.data.map(freq => { return {label: freq.name, value: freq.id, id: freq.id}})      
    }

    const addRecurringPayment = async (paymentData) => {
        try {
            const { data } = await RecurringPaymentAPI.create(paymentData)
            
            if(data.success)
                await getRecurringPayments();
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
        frequencies,
        addRecurringPayment,
        deleteRecurringPayment,
    }
})