import { ref, onMounted} from 'vue';
import { defineStore } from 'pinia';
import ReportsAPI from '@/api/ReportsAPI';

export const useReportsStore = defineStore( 'reports', () => {

    const paymentMethodsProjection = ref([])

    onMounted(async () => {
        try {
            await getProjections(6);
        } catch (error) {
            console.log(error);
        }
    })

    const getProjections = async (months) => {
        const {data} = await ReportsAPI.paymentMethodsProjection(months);
        paymentMethodsProjection.value = data.data;
    }

    const resetProjectionsCheck = () => {
        paymentMethodsProjection.value = paymentMethodsProjection.value.map(
            paymentMethod => {
             return {...paymentMethod, periods: paymentMethod.periods.map(
                projection => {return {...projection, checked: false}} 
            )} 
        }); 
    }


    return {
        paymentMethodsProjection,
        getProjections,
        resetProjectionsCheck
    }
})