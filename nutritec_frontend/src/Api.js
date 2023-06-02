import axios from 'axios';

const API_URL = 'http://localhost:5295/api/';

export const obtenerTiemposComida=() => {
    const endpoint =API_URL+"TiempoComida";

    return axios.get(endpoint).then(response => {
        return response.data;
    })
    .catch(error =>{
        console.error('Error al obtener tiempos de comida', error);
        return null;
    });
};

export const obtenerProductos = () => {
    const endpoint = `${API_URL}producto`;

    return axios.get(endpoint)
    .then(response => {
        return response.data;
    })
    .catch(error=> {
        console.error('Error al obtener los productos', error);
        return null;
    })
}