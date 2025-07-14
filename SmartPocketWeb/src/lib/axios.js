import axios from "axios";

const config = {
    baseURL : import.meta.env.VITE_API_URL,
    headers: {
        Authorization: `Bearer ${localStorage.getItem('AUTH_TOKEN')}`
    },
    'Content-Type': 'application/json'
};

const api = axios.create(config);

export default api