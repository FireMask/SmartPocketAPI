import { ref, onMounted, computed } from 'vue'
import { defineStore } from 'pinia'
import { useRouter } from 'vue-router'
import AuthAPI from '../api/AuthAPI'

export const useUserStore = defineStore('user', () => {
    const router = useRouter()
    const user = ref({})

    onMounted(async () => {
        try {
            const { data } = await AuthAPI.user()
            user.value = data.data
        } catch (error) {
            console.log(error)
        } 
    })

    function logout() {
        localStorage.removeItem('AUTH_TOKEN')
        user.value = {}
        router.push({name : 'login'})
    }

    const getUserName = computed( () => {
        return user.value?.name ? user.value?.name : ''
        }
    )

    return {
        user,
        logout,
        getUserName,
    }
})