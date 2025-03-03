import api from "../lib/axios";

export default {
    filterCatalogs() {
        const token = localStorage.getItem('AUTH_TOKEN')
        return api.get('/filterCatalogs',{
            headers: {
                Authorization: `Bearer ${token}`
            }
        })
    }
}