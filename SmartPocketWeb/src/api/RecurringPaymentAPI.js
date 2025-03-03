import api from "../lib/axios";

export default {
    paginate(data) {
        const token = localStorage.getItem('AUTH_TOKEN')
        return api.get('/pendingMovements', {
            headers: {
                Authorization: `Bearer ${token}`,
                'Content-Type': 'application/json'
            },
            params: data,
            paramsSerializer: {
                indexes: null, // no brackets at all
              }
        });
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