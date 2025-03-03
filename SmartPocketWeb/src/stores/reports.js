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

    return {
        paymentMethodsProjection,
        getProjections
    }
})