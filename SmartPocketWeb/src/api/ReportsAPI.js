import api from "../lib/axios";

export default {
    paymentMethodsProjection(data) {
        const token = localStorage.getItem('AUTH_TOKEN')
        return api.get(`/paymentMethodsProjection/`,{
            headers: {
                Authorization: `Bearer ${token}`
            },
            params: data,
        })
    }
}