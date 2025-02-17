import api from "../lib/axios";

export default {
    all() {
        const token = localStorage.getItem('AUTH_TOKEN')
        return api.get('/recurringPayments',{
            headers: {
                Authorization: `Bearer ${token}`
            }
        })
    },
    create(paymentData){
        const token = localStorage.getItem('AUTH_TOKEN')
        return api.post('/recurringPayment', paymentData, {
            headers: {
                Authorization: `Bearer ${token}`
            }
        });
    },
    delete(id){
        const token = localStorage.getItem('AUTH_TOKEN')
        return api.delete(`/recurringPayment/${id}`, {
            headers: {
                Authorization: `Bearer ${token}`
            }
        });
    },
    frequencies() {
        const token = localStorage.getItem('AUTH_TOKEN')
        return api.get('/frequencies',{
            headers: {
                Authorization: `Bearer ${token}`
            }
        })
    }
}