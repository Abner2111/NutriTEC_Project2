import axios from 'axios';
const API_URL =  'http://localhost:5295/api';

export const registrarNutricionista = async (nutricionista)=>{
    const response = await axios.post(`${API_URL}/nutricionista/registrar`, nutricionista);
    return response.data;
};

export const validarNutricionista = async (correo, contrasena)=> {
    const response = await axios.get(`${API_URL}/nutricionista/validar/${correo}/${contrasena}`);
    return response.data;
};

export const validarAdministrador = async (correo, contrasena)=> {
  const response = await axios.get(`${API_URL}/administrador/validar/${correo}/${contrasena}`);
  return response.data;
};

export const obtenerProductos = async () => {
    const response = await axios.get(`${API_URL}/Producto`);
    return response.data;
  };
  
  export const obtenerProducto = async (id) => {
    const response = await axios.get(`${API_URL}/Producto/${id}`);
    return response.data;
  };
  
  export const agregarProducto = async (producto) => {
    const response = await axios.post(`${API_URL}/Producto/agregar`, producto);
    return response.data;
  };
  
  export const actualizarProducto = async (id, producto) => {
    const response = await axios.put(`${API_URL}/Producto/editar/${id}`, producto);
    return response.data;
  };
  export const eliminarProducto = async (id) => {
    const response = await axios.delete(`${API_URL}/Producto/eliminar/${id}`);
    return response.data;
  };
  export const aprobarProducto = async (id) =>{
    const response = await axios.post(`${API_URL}/Producto/aprobar/${id}`);
    return response.data;
  }