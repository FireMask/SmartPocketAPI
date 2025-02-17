import api from "../lib/axios";

export default {
    all() {
        const token = localStorage.getItem('AUTH_TOKEN')
        return api.get('/paymentMethods',{
            headers: {
                Authorization: `Bearer ${token}`
            }
        })
    },
    create(cardData){
        const token = localStorage.getItem('AUTH_TOKEN')
        return api.post('/paymentMethod', cardData, {
            headers: {
                Authorization: `Bearer ${token}`
            }
        });
    },
    delete(id){
        const token = localStorage.getItem('AUTH_TOKEN')
        return api.delete(`/paymentMethod/${id}`, {
            headers: {
                Authorization: `Bearer ${token}`
            }
        });
    },
    update(cardData){
        const token = localStorage.getItem('AUTH_TOKEN')
        return api.put('/paymentMethod', cardData, {
            headers: {
                Authorization: `Bearer ${token}`
            }
        });
    },
    types() {
        const token = localStorage.getItem('AUTH_TOKEN')
        return api.get('/paymentMethodTypes', {
            headers: {
                Authorization: `Bearer ${token}`
            }
        });
    }
}