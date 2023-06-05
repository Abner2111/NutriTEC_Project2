import React from 'react';
import { obtenerProductos, agregarProducto, actualizarProducto, eliminarProducto } from '../Api';
import '../Templates/diseñoH.css';
import '../Templates/diseño.css';
import { NavbarNutricionista } from "../Templates/NavbarNutricionista"


class GestionProductos extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            productos: [],
            formValues: { codigoBarras: '', nombre: '', aprobado: false, grasa: '', tamanoPorcion: '', energia: '', proteina: '', sodio: '', carbohidratos: '', hierro: '', vitaminaD: '', vitaminaB6: '', vitaminaC: '', vitaminaK: '', vitaminaB: '', vitaminaB12: '', vitaminaA: '', calcio: '' },
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
            await actualizarProducto(this.state.currentProductId, this.state.formValues);
        }
        this.getProductos();
        this.setState({ formValues: { codigoBarras: '', nombre: '', aprobado: false, grasa: '', tamanoPorcion: '', energia: '', proteina: '', sodio: '', carbohidratos: '', hierro: '', vitaminaD: '', vitaminaB6: '', vitaminaC: '', vitaminaK: '', vitaminaB: '', vitaminaB12: '', vitaminaA: '', calcio: '' }, formMode: 'agregar', showPopup: false });
    };

    // Función para manejar el cambio de los inputs del formulario
    handleInputChange = (event) => {
        const { name, value } = event.target;
        this.setState({ formValues: { ...this.state.formValues, [name]: value } });
    };

    // Función para manejar el clic en el botón de editar de una fila de la tabla
    handleEditClick = (producto) => {
        this.setState({ formValues: producto, formMode: 'editar', currentProductId: producto.codigoBarras, showPopup: true });
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
            this.setState({ formValues: { codigoBarras: '', nombre: '', aprobado: false, grasa: '', tamanoPorcion: '', energia: '', proteina: '', sodio: '', carbohidratos: '', hierro: '', vitaminaD: '', vitaminaB6: '', vitaminaC: '', vitaminaK: '', vitaminaB: '', vitaminaB12: '', vitaminaA: '', calcio: '' }, formMode: 'agregar', showPopup: false });
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
        this.setState({ formValues: { codigoBarras: '', nombre: '', aprobado: false, grasa: '', tamanoPorcion: '', energia: '', proteina: '', sodio: '', carbohidratos: '', hierro: '', vitaminaD: '', vitaminaB6: '', vitaminaC: '', vitaminaK: '', vitaminaB: '', vitaminaB12: '', vitaminaA: '', calcio: '' }, formMode: 'agregar', showPopup: false });
        this.setState({ showPopup: false });
        document.removeEventListener('mousedown', this.handleOuterClick);
    }
    render() {
        const { productos, formValues, formMode, showPopup } = this.state;
        return (
            <div className="gestion-productos-container">
                <NavbarNutricionista />
                <h1 style={{ margin: '50px 0', fontSize: '2.5rem', fontWeight: 'bold', textTransform: 'uppercase' }}>Gestión de productos</h1>
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
                            <th style={{ padding: '10px' }}>Acciones</th>
                        </tr>
                    </thead>
                    <tbody>
                        {productos
                            .filter((producto) => producto.aprobado)
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

                                    <td>
                                        <button className="btn-accion btn-editar" onClick={() => this.handleEditClick(producto)}>Editar</button>
                                        <button className="btn-accion btn-eliminar" onClick={() => this.handleDeleteClick(producto.codigoBarras)}>Eliminar</button>
                                    </td>
                                </tr>
                            ))}

                    </tbody>
                </table>
                <button className="btn-agregar" onClick={() => this.setState({ showPopup: true })}>Agregar</button>
                {showPopup && (
                    <div className="popup-container">
                        <div className="popup">
                            <h2>{formMode === 'agregar' ? 'Agregar Producto' : 'Actualizar Producto'}</h2>
                            <form onSubmit={this.handleSubmit}>
                                <div>
                                    <label htmlFor="codigoBarras">Código de barras:</label>
                                    <input type="text" id="codigoBarras" name="codigoBarras" value={formValues.codigoBarras} disabled={formMode === 'editar'} onChange={this.handleInputChange} />
                                </div>
                                <div>
                                    <label htmlFor="nombre">Nombre:</label>
                                    <input type="text" id="nombre" name="nombre" value={formValues.nombre} onChange={this.handleInputChange} />
                                </div>
                                <div>
                                <label htmlFor="tamanoPorcion">Tamaño de Porcion:</label>
                                    <input type="text" id="tamanoPorcion" name="tamanoPorcion" value={formValues.tamanoPorcion} onChange={this.handleInputChange} />
                                </div>
                                <div>
                                <label htmlFor="grasa">Grasa:</label>
                                    <input type="text" id="grasa" name="grasa" value={formValues.grasa} onChange={this.handleInputChange} />
                                </div>
                                <div>
                                <label htmlFor="energia">Energia:</label>
                                    <input type="text" id="energia" name="energia" value={formValues.energia} onChange={this.handleInputChange} />
                                </div>
                                <div>
                                <label htmlFor="proteina">Proteina:</label>
                                    <input type="text" id="proteina" name="proteina" value={formValues.proteina} onChange={this.handleInputChange} />
                                </div>
                                <div>
                                <label htmlFor="sodio">Sodio:</label>
                                    <input type="text" id="sodio" name="sodio" value={formValues.sodio} onChange={this.handleInputChange} />
                                </div>
                                <div>
                                <label htmlFor="carbohidratos">Carbohidratos:</label>
                                    <input type="text" id="carbohidratos" name="carbohidratos" value={formValues.carbohidratos} onChange={this.handleInputChange} />
                                </div>
                                <div>
                                <label htmlFor="hierro">Hierro:</label>
                                    <input type="text" id="hierro" name="hierro" value={formValues.hierro} onChange={this.handleInputChange} />
                                </div>
                                <div>
                                <label htmlFor="vitaminaA">VitaminaA:</label>
                                    <input type="text" id="vitaminaA" name="vitaminaA" value={formValues.vitaminaA} onChange={this.handleInputChange} />
                                </div>
                                <div>
                                <label htmlFor="vitaminaB">VitaminaB:</label>
                                    <input type="text" id="vitaminaB" name="vitaminaB" value={formValues.vitaminaB} onChange={this.handleInputChange} />
                                </div>
                                <div>
                                <label htmlFor="vitaminaB12">VitaminaB12:</label>
                                    <input type="text" id="vitaminaB12" name="vitaminaB12" value={formValues.vitaminaB12} onChange={this.handleInputChange} />
                                </div>
                                <div>
                                <label htmlFor="vitaminaB6">VitaminaB6:</label>
                                    <input type="text" id="vitaminaB6" name="vitaminaB6" value={formValues.vitaminaB6} onChange={this.handleInputChange} />
                                </div>
                                <div>
                                <label htmlFor="vitaminaC">VitaminaC:</label>
                                    <input type="text" id="vitaminaC" name="vitaminaC" value={formValues.vitaminaC} onChange={this.handleInputChange} />
                                </div>
                                <div>
                                <label htmlFor="vitaminaD">VitaminaD:</label>
                                    <input type="text" id="vitaminaD" name="vitaminaD" value={formValues.vitaminaD} onChange={this.handleInputChange} />
                                </div>
                                <div>
                                <label htmlFor="vitaminaK">VitaminaK:</label>
                                    <input type="text" id="vitaminaK" name="vitaminaK" value={formValues.vitaminaK} onChange={this.handleInputChange} />
                                </div>
                                <div>
                                <label htmlFor="calcio">Calcio:</label>
                                    <input type="text" id="calcio" name="calcio" value={formValues.calcio} onChange={this.handleInputChange} />
                                </div>

                                <button type="submit" className="btn-submit">{formMode === 'agregar' ? 'Agregar' : 'Actualizar'}</button>
                                {formMode === 'editar' && (<button type="button" className="btn-cancelar" onClick={() => this.setState({ showPopup: false })}>Cancelar</button>)}
                            </form>
                        </div>
                    </div>
                )}
            </div>
        );
    }
}
export default GestionProductos;