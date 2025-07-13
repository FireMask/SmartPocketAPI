import api from "../lib/axios";

export default {
    all() {
        const token = localStorage.getItem('AUTH_TOKEN')
        return api.get('/movements', {
            headers: {
                Authorization: `Bearer ${token}`
            }
        });
    },
    paginate(data) {
        const token = localStorage.getItem('AUTH_TOKEN')
        return api.get('/movements', {
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
    dashboard() {
        const token = localStorage.getItem('AUTH_TOKEN')
        return api.get('/dashboard', {
            headers: {
                Authorization: `Bearer ${token}`
            }
        });
    },
    create(movementData){
        const token = localStorage.getItem('AUTH_TOKEN')
        return api.post('/movement', movementData, {
            headers: {
                Authorization: `Bearer ${token}`
            }
        });
    },
    delete(id){
        const token = localStorage.getItem('AUTH_TOKEN')
        return api.delete(`/movement/${id}`, {
            headers: {
                Authorization: `Bearer ${token}`
            }
        });
    },
    update(movementData){
        const token = localStorage.getItem('AUTH_TOKEN')
        return api.put('/movement', movementData, {
            headers: {
                Authorization: `Bearer ${token}`
            }
        });
    },
    categories(){
        const token = localStorage.getItem('AUTH_TOKEN')
        return api.get('/categories', {
            headers: {
                Authorization: `Bearer ${token}`
            }
        });
    },
    createCategory(categoryData){
        const token = localStorage.getItem('AUTH_TOKEN')
        return api.post('/category', categoryData, {
            headers: {
                Authorization: `Bearer ${token}`
            }
        });
    },
    types(){
        const token = localStorage.getItem('AUTH_TOKEN')
        return api.get('/movementTypes', {
            headers: {
                Authorization: `Bearer ${token}`
            }
        });
    },
    CreatePendingMovement(movementData){
        const token = localStorage.getItem('AUTH_TOKEN')
        return api.post('/createNewMovementFromRecurringPayment', movementData, {
            headers: {
                Authorization: `Bearer ${token}`
            }
        });
    },
}