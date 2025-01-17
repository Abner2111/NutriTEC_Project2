import axios from 'axios';
const API_URL =  'https://nutritecrestapi.azurewebsites.net/api';

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
    const endpoint = `${API_URL}/producto`;
    return axios.get(endpoint).then(response => {
      return response.data;
    })
    .catch(error => {
      console.error('Error al obtener productos',error);
      return null;
    });
  };

  export const obtenerRecetas = async () => {
    const endpoint = `${API_URL}/Receta`;
    return axios.get(endpoint).then(response => {
      return response.data;
    })
    .catch(error => {
      console.error('Error al obtener recetas',error);
      return null;
    });
  };
export const registrarCliente = async (cliente) =>{
  const response = await axios.post(`${API_URL}/Cliente`,cliente);
  return response.data;
  };
export const validarCliente = async (correo, contrasena) => {
  const response = await axios.get(`${API_URL}/Cliente/Login/${correo}/${contrasena}`);
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
  export const obtenerTiemposComida = () => {
    const endpoint = API_URL+"/TiempoComida";
    return axios.get(endpoint).then(response => {
      return response.data;
    })
    .catch(error => {
      console.error('Error al obtener tiempos de comida',error);
      return null;
    });
  };
export const agregarConsumoProducto = async (correo, idProducto, mealtime) => {
  const endpoint = API_URL+"/Consumo/producto"
  const date = new Date();
  let currentDay= String(date.getDate()).padStart(2, '0');

  let currentMonth = String(date.getMonth()+1).padStart(2,"0");
      
  let currentYear = date.getFullYear();

  let currentDate = `${currentYear}-${currentMonth}-${currentDay}`;
  console.log(currentDate);
  const consumo = {
    inptcorreo: correo,
    inptfecha: currentDate,
    inpttiempocomidaid: Number(mealtime),
    inptproductoid: Number(idProducto)
  }
  console.log(JSON.stringify(consumo));
  const response = await axios.post(endpoint, consumo);
  return response.data;
}
export const agregarConsumoReceta = async (correo, nombreReceta, mealtime) => {
  const endpoint = API_URL+"/Consumo/receta"
  const date = new Date();
  let currentDay= String(date.getDate()).padStart(2, '0');

  let currentMonth = String(date.getMonth()+1).padStart(2,"0");
      
  let currentYear = date.getFullYear();

  let currentDate = `${currentYear}-${currentMonth}-${currentDay}`;
  console.log(currentDate);
  const consumo = {
    inptcorreo: correo,
    inptfecha: currentDate,
    inpttiempocomidaid: Number(mealtime),
    inptrecetaname: nombreReceta
  }
  console.log(JSON.stringify(consumo));
  const response = await axios.post(endpoint, consumo);
  return response.data;
}

export const agregarMedidas = async (consumo) => {
  const endpoint = API_URL+"/Medida"
  const response = await axios.post(endpoint, consumo);
  return response.data;
}

export const obtenerPlanesCliente = async (correo) => {
  const endpoint = API_URL+"/Cliente/planes/"+correo;
  const response = await axios.get(endpoint);
  return response.data;
}

export const obtenerPlanes = async () => {
  const endpoint = API_URL+"/Plan";
  const response = await axios.get(endpoint);
  return response.data;
}
 
export const obtenerConsumos = async()=>{
  const response = await axios.get(`${API_URL}/Consumo`);
  return response.data;
}
export const obtenerRetroalimentacion = async()=>{
  const response = await axios.get(`${API_URL}/retroalimentacion`);
  return response.data;
}
export const agregarRetroalimentacion = async(retroalimentacion)=>{
  const response = await axios.post(`${API_URL}/retroalimentacion`,retroalimentacion);
  return response.data
}
export const obtenerMedidasCliente = async(correo)=>{
  const response = await axios.get(`${API_URL}/Medida/${correo}`);
  return response.data
}