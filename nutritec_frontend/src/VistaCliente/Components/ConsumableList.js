import React, {Component} from 'react';
import ConsumableRow from './ConsumableRow';
import SelectedConsumableRow from './SelectedConsumableRow';
import MealTimeSelector from './MealTimeSelector';
import { agregarConsumoProducto, obtenerProductos } from '../../Api';
class ConsumableList extends Component {
    constructor(props){
        super(props);
        this.state = {
            //product list
            productos : [],
            //recipe list
            recetas : [],
            //filtered products
            productosFiltrados : [],
            data : [],
            selectedMealTime:''
        }

        this.getProducts();
        console.log(this.state.productos)

        
    } 

    /**
     * gets products from a api
     */
    getProducts = async () => {
        const data = await obtenerProductos();
        this.setState({productos: data});
        this.setState({productosFiltrados: data});
    }
    /**
     * adds selected consumables to the list of selected consumables
     * @param {*} childdata 
     */
    childToParentAdd = (childdata) =>{
        this.setState({data: [...this.state.data, childdata]});
        console.log(this.state.data);
    }

    /**
     * deletes the consumables from the selected consumables
     * @param {*} childdata 
     */
    childToParentDelete = (childdata) =>{
        const query = childdata.nombre;
        
        var updatedList = this.state.data;

        const index = updatedList.findIndex((i) => i.nombre === query);
        updatedList.splice(index,1);
        this.setState({data:updatedList})
    }
    /**
     * filtering consumables
     * @param {*} event 
     */
    filterBySearch = (event) => {
        const query = event.target.value;
        
        var updatedList = this.state.productos;

        updatedList = updatedList.filter(obj => 
            obj.nombre.toLowerCase().indexOf(query.toLowerCase()) >=0 || obj.codigoBarras.toLowerCase().indexOf(query.toLowerCase()) >=0);

        this.setState({productosFiltrados:updatedList})
        
        
    }
    /**
     * crea una fila de consumible basado en un json de producto
     * @param {*} product 
     * @returns 
     */
    createProduct = ({id, nombre, codigoBarras}) => {
        return < ConsumableRow 
                id = {id}
                nombre = {nombre}
                codigoBarras = {codigoBarras}
                childToParent = {this.childToParentAdd}
                />
    }

    /**
     * creates the elements of the selected products area
     * @param {*} param0 
     * @returns 
     */
    createProductSelection = ({id, nombre, codigoBarras}) => {
        return < SelectedConsumableRow 
                id = {id}
                nombre = {nombre}
                codigoBarras = {codigoBarras}
                childToParent = {this.childToParentDelete}
                />
    }

    registerConsume = () => {
        for (let consumable in this.stateproductos){

        }
    }

    handleCallbackFromMealSelector = (childData) => {
        this.setState({selectedMealTime: childData.value})
        console.log(this.selectedMealTime);
    }
    
    render(){
        return(
            <div>
                <div className='row'>
                    
                    <div className="col search-header">
                            <div className="search-text">Buscar:</div>
                            <input id="search-box" onChange={this.filterBySearch} />
                    </div>
                    <MealTimeSelector parentCallback = {this.handleCallbackFromMealSelector}/>

                </div>
                <div className='row'>
                    <div className='col search'>
                        <h3>Consumibles</h3>
                        <div className="container main-content">
                            
                            {this.state.productosFiltrados.map(this.createProduct)}
                        </div>
                    </div>
                    <div className='col added'>
                        <h3>Tu consumo</h3>
                        <div className="container main-selected-content">
                            {this.state.data.map(this.createProductSelection)}
                        </div>
                    </div>
                    
                </div>
                <button style={{ color: '#FFF', backgroundColor: '#008CBA', borderRadius: '12px', padding: '12px', border: '2px solid #008CBA' }}>Registrar Consumo</button>
            </div>
            
            
            
        )
    }

    
}
export default ConsumableList;