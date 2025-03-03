import api from '../lib/axios'

export default {
    register(newUser){
        return api.post('/auth/create', newUser);
    },
    login(userData){
        return api.post('/auth/login', userData);
    },
    user(){
        const token = localStorage.getItem('AUTH_TOKEN')
        return api.get('/user', {
            headers: {
                Authorization: `Bearer ${token}`
            }
        });
    }
}