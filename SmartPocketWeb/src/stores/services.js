import { ref, onMounted} from 'vue';
import { defineStore } from 'pinia';
import MovementsAPI from '@/api/MovementsAPI';

export const useServicesStore = defineStore( 'services', () => {

    onMounted(async () => {
        try {
            const {data} = await MovementsAPI.all()
            console.log(data);

        } catch (error) {
            console.log(error);
        }
    })

    return {

    }
})