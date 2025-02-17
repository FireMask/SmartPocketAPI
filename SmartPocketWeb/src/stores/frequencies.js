import { ref, onMounted} from 'vue';
import { defineStore } from 'pinia';
import FrequenciesAPI from '@/api/FrequenciesAPI';

export const useServicesStore = defineStore( 'frequencies', () => {

    onMounted(async () => {
        try {
            const {data} = await FrequenciesAPI.all()
            console.log(data);

        } catch (error) {
            console.log(error);
        }
    })

    return {

    }
})