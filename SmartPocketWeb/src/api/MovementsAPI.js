import api from "../lib/axios";

export default {
    all() {
        return api.get('/movements');
    },
    paginate(data) {
        return api.get('/movements', {
            params: data,
            paramsSerializer: {
                indexes: null, // no brackets at all
            }
        });
    },
    dashboard() {
        return api.get('/dashboard');
    },
    create(movementData){
        return api.post('/movement', movementData);
    },
    delete(id){
        return api.delete(`/movement/${id}`);
    },
    update(movementData){
        return api.put('/movement', movementData);
    },
    getTopCategories(){
        return api.get('/category/topCategories');
    },
    categories(){
        return api.get('/categories');
    },
    createCategory(categoryData){
        return api.post('/category', categoryData);
    },
    types(){
        return api.get('/movementTypes');
    },
    CreatePendingMovement(movementData){
        return api.post('/createNewMovementFromRecurringPayment', movementData);
    },
}