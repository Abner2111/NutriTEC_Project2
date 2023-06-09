import React from 'react';
import { obtenerConsumos, agregarRetroalimentacion, aprobarProducto, eliminarProducto } from '../Api';
import '../Templates/diseñoH.css';
import '../Templates/diseño.css';
import { NavbarNutricionista } from "../Templates/NavbarNutricionista"


class SeguimientoPaciente extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            productos: [],
            formValues: { id: '', producto_o_Receta: '', tiempoComida: '', fecha: '', cliente: '' },
            retroValues: {_id:'', pacienteId:'', nutricionistaId:'', fecha:'', comentario:''},
            formMode: 'agregar',
            currentPacienteId: '',
            showPopup: false
        };
        this.handleOuterClick = this.handleOuterClick.bind(this);
    }

    // Función para obtener los productos desde la API
    getProductos = async () => {
        const data = await obtenerConsumos();
        this.setState({ productos: data });
    };

    // Función para manejar el envío del formulario
    handleSubmit = async (event) => {
        event.preventDefault();
        await agregarRetroalimentacion(this.state.retroValues);
        this.getProductos();
        this.setState({ formValues: { id: '', producto_o_Receta: '', tiempoComida: '', fecha: '', cliente: '' }, formMode: 'agregar', showPopup: false });
    };

    // Función para manejar el cambio de los inputs del formulario
    handleInputChange = (event) => {
        const { name, value } = event.target;
        this.setState({ retroValues: { ...this.state.retroValues, [name]: value } });
        console.log(this.state.retroValues)
    };

    // Función para manejar el clic en el botón de editar de una fila de la tabla
    handleEditClick = async (producto) => {
        this.setState({ retroValues: {...this.state.retroValues, pacienteId: producto.cliente, nutricionistaId: sessionStorage.getItem("miId") }});
        this.setState({ formValues: producto, formMode: 'agregar', currentPacienteId: producto.cliente, showPopup: true });
        console.log(this.state.retroValues)
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
            this.setState({ formValues: { id: '', producto_o_Receta: '', tiempoComida: '', fecha: '', cliente: '' }, formMode: 'agregar', showPopup: false });
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
    // Método para manejar el cambio de cliente seleccionado en el filtro
    handleClienteChange = (event) => {
        const cliente = event.target.value;
        this.setState({ clienteSeleccionado: cliente });
    };

    handleCerrarClick = () => {
        this.setState({ formValues: { id: '', producto_o_Receta: '', tiempoComida: '', fecha: '', cliente: '' }, formMode: 'agregar', showPopup: false });
        this.setState({ showPopup: false });
        document.removeEventListener('mousedown', this.handleOuterClick);
    }
    render() {
        const { productos, clienteSeleccionado, formValues, formMode, showPopup } = this.state;

        // Aplicar filtro por cliente seleccionado
        const productosFiltrados = clienteSeleccionado
            ? productos.filter((producto) => producto.cliente === clienteSeleccionado)
            : productos;

        return (
            <div className="gestion-productos-container">
                <NavbarNutricionista />
                <h1 style={{ margin: '50px 0', fontSize: '2.5rem', fontWeight: 'bold', textTransform: 'uppercase' }}>Seguimiento Paciente</h1>
                <select onChange={this.handleClienteChange}>
                    <option value="">Todos los clientes</option>
                    {[...new Set(productos.map((producto) => producto.cliente))].map((cliente) => (
                        <option key={cliente} value={cliente}>
                            {cliente}
                        </option>
                    ))}
                </select>
                <table className="tabla-productos">
                    <thead>
                        <tr>
                            <th>Producto o Receta</th>
                            <th>Tiempo de Comida</th>
                            <th>Fecha</th>
                            <th>Cliente</th>
                            <th>Acciones</th>
                        </tr>
                    </thead>
                    <tbody>
                        {productosFiltrados.map((producto) => (
                            <tr key={producto.codigoBarras}>
                                <td>{producto.producto_o_Receta}</td>
                                <td>{producto.tiempoComida}</td>
                                <td>{producto.fecha}</td>
                                <td>{producto.cliente}</td>
                                <td>
                                    <button className="btn-accion btn-editar" onClick={() => this.handleEditClick(producto)}>Agregar Retroalimentacion</button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
                {showPopup && (
                    <div className="popup-container">
                        <div className="popup">
                            <h2>Retroalimentacion</h2>
                            <form onSubmit={this.handleSubmit}>
                                <div className="form-group">
                                    <label htmlFor="pacienteId">Paciente ID:</label>
                                    <input type="text" id="pacienteId" name="pacienteId" onChange={this.handleInputChange} value={this.state.currentPacienteId} disabled />
                                </div>

                                <div className="form-group">
                                    <label htmlFor="nutricionistaId">Nutricionista ID:</label>
                                    <input type="text" id="nutricionistaId" name="nutricionistaId" value={sessionStorage.getItem("miId")} disabled />
                                </div>

                                <div className="form-group">
                                    <label htmlFor="fecha">Fecha:</label>
                                    <input type="date" id="fecha" name="fecha" onChange={this.handleInputChange} value={this.state.fecha} />
                                </div>
                                <div className="form-group">
                                    <label htmlFor="comentario">Comentario:</label>
                                    <textarea id="comentario" name="comentario" onChange={this.handleInputChange} value={this.state.comentario} />
                                </div>
                                <button type="submit" className="btn-submit">Agregar</button>
                                <button type="button" className="btn-cancelar" onClick={() => this.setState({ showPopup: false })}>Cancelar</button>
                            </form>
                        </div>
                    </div>
                )}

            </div>
        );
    }
}
export default SeguimientoPaciente;