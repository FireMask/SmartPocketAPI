import api from "../lib/axios";

export default {
    paymentMethodsProjection(months) {
        const token = localStorage.getItem('AUTH_TOKEN')
        return api.get(`/paymentMethodsProjection/${months}`,{
            headers: {
                Authorization: `Bearer ${token}`
            }
        })
    }
}