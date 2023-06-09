import React from 'react';
import { obtenerProductos, agregarProducto, aprobarProducto, eliminarProducto } from '../Api';
import '../Templates/diseñoH.css';
import '../Templates/diseño.css';
import { NavbarAdministrador } from "../Templates/NavbarAdministrador"


class GestionProductos extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            productos: [],
            formValues: { productoId:'', codigoBarras: '', nombre: '', aprobado: false, grasa: '', tamanoPorcion: '', energia: '', proteina: '', sodio: '', carbohidratos: '', hierro: '', vitaminaD: '', vitaminaB6: '', vitaminaC: '', vitaminaK: '', vitaminaB: '', vitaminaB12: '', vitaminaA: '', calcio: '' },
            formMode: 'agregar',
            currentProductId: '',
            showPopup: false
        };
        this.handleOuterClick = this.handleOuterClick.bind(this);
    }

    // Función para obtener los productos desde la API
    getProductos = async () => {
        const data = await obtenerProductos();
        this.setState({ productos: data });
    };

    // Función para manejar el envío del formulario
    handleSubmit = async (event) => {
        event.preventDefault();
        if (this.state.formMode === 'agregar') {
            console.log(this.state.formValues);
            await agregarProducto(this.state.formValues);
        } else {
            await aprobarProducto(this.state.currentProductId);
        }
        this.getProductos();
        this.setState({ formValues: { productoId:'', codigoBarras: '', nombre: '', aprobado: false, grasa: '', tamanoPorcion: '', energia: '', proteina: '', sodio: '', carbohidratos: '', hierro: '', vitaminaD: '', vitaminaB6: '', vitaminaC: '', vitaminaK: '', vitaminaB: '', vitaminaB12: '', vitaminaA: '', calcio: '' }, formMode: 'agregar', showPopup: false });
    };

    // Función para manejar el cambio de los inputs del formulario
    handleInputChange = (event) => {
        const { name, value } = event.target;
        this.setState({ formValues: { ...this.state.formValues, [name]: value } });
    };

    // Función para manejar el clic en el botón de editar de una fila de la tabla
    handleEditClick = async(producto) => {
        this.setState({ formValues: producto, formMode: 'editar', currentProductId: producto.id, showPopup: true });
        await aprobarProducto(producto.id);

    };

    // Función para manejar el clic en el botón de eliminar de una fila de la tabla
    handleDeleteClick = async (codigoBarras) => {
        await eliminarProducto(codigoBarras);
        this.getProductos();
    };
    handleOuterClick(event) {
        const container = document.querySelector('.popup');
        if (container && !container.contains(event.target)) {
            this.setState({ showPopup: false });
            this.setState({ formValues: { productoId:'', codigoBarras: '', nombre: '', aprobado: false, grasa: '', tamanoPorcion: '', energia: '', proteina: '', sodio: '', carbohidratos: '', hierro: '', vitaminaD: '', vitaminaB6: '', vitaminaC: '', vitaminaK: '', vitaminaB: '', vitaminaB12: '', vitaminaA: '', calcio: '' }, formMode: 'agregar', showPopup: false });
        }
    };
    // Se ejecuta al cargar el componente 
    componentDidMount() {
        this.getProductos();
        document.addEventListener('mousedown', this.handleOuterClick);
    }
    componentWillUnmount() {
        document.removeEventListener('mousedown', this.handleOuterClick);
    };

    handleCerrarClick = () => {
        this.setState({ formValues: { productoId:'', codigoBarras: '', nombre: '', aprobado: false, grasa: '', tamanoPorcion: '', energia: '', proteina: '', sodio: '', carbohidratos: '', hierro: '', vitaminaD: '', vitaminaB6: '', vitaminaC: '', vitaminaK: '', vitaminaB: '', vitaminaB12: '', vitaminaA: '', calcio: '' }, formMode: 'agregar', showPopup: false });
        this.setState({ showPopup: false });
        document.removeEventListener('mousedown', this.handleOuterClick);
    }
    render() {
        const { productos} = this.state;
        return (
            <div className="gestion-productos-container">
                <NavbarAdministrador />
                <h1 style={{ margin: '50px 0', fontSize: '2.5rem', fontWeight: 'bold', textTransform: 'uppercase' }}>Aprobar Productos</h1>
                <table className="tabla-productos">
                    <thead>
                        <tr>
                            <th style={{ padding: '10px' }}>Descripción</th>
                            <th style={{ padding: '10px' }}>Tamaño de porcion (g/ml)</th>
                            <th style={{ padding: '10px' }}>Grasa (g)</th>
                            <th style={{ padding: '10px' }}>Energia (Kcal)</th>
                            <th style={{ padding: '10px' }}>Proteina (g)</th>
                            <th style={{ padding: '10px' }}>Sodio (mg)</th>
                            <th style={{ padding: '10px' }}>Carbohidratos (g)</th>
                            <th style={{ padding: '10px' }}>Hierro (mg)</th>
                            <th style={{ padding: '10px' }}>Vitaminas</th>
                            <th style={{ padding: '10px' }}>Calcio (mg)</th>
                            <th style={{ padding: '10px' }}>Aprobado</th>
                            <th style={{ padding: '10px' }}>Acciones</th>
                        </tr>
                    </thead>
                    <tbody>
                        {productos
                            .filter((producto) => !producto.aprobado)
                            .map((producto) => (
                                <tr key={producto.codigoBarras}>
                                    <td style={{ padding: '10px' }}>{producto.nombre}</td>
                                    <td style={{ padding: '10px' }}>{producto.tamanoPorcion}</td>
                                    <td style={{ padding: '10px' }}>{producto.grasa}</td>
                                    <td style={{ padding: '10px' }}>{producto.energia}</td>
                                    <td style={{ padding: '10px' }}>{producto.proteina}</td>
                                    <td style={{ padding: '10px' }}>{producto.sodio}</td>
                                    <td style={{ padding: '10px' }}>{producto.carbohidratos}</td>
                                    <td style={{ padding: '10px' }}>{producto.hierro}</td>
                                    <td style={{ padding: '10px' }}>{producto.vitaminaA ? 'A ' : ''} {producto.vitaminaB ? 'B ' : ''}
                                    {producto.vitaminaB12 ? 'B12 ' : ''} {producto.vitaminaB6 ? 'B6 ' : ''}{producto.vitaminaC ? 'C ' : ''} {producto.vitaminaD ? 'D ' : ''}
                                    {producto.vitaminaK ? 'K' : ''}</td>
                                    
                                    <td style={{ padding: '10px' }}>{producto.calcio}</td>
                                    <td style={{ padding: '10px' }}>{producto.aprobado? 'Si' : 'No'}</td>
                                    <td>
                                        <button className="btn-accion btn-editar" onClick={() => this.handleEditClick(producto)}>Aprobar</button>
                                        <button className="btn-accion btn-eliminar" onClick={() => this.handleDeleteClick(producto.codigoBarras)}>Eliminar</button>
                                    </td>
                                </tr>
                            ))}

                    </tbody>
                </table>
                
            </div>
        );
    }
}
export default GestionProductos;