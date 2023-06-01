import React, {Component} from 'react';
import ConsumableRow from './ConsumableRow';
import SelectedConsumableRow from './SelectedConsumableRow';
import axios from 'axios';
class ConsumableList extends Component {
    constructor(props){
        super(props);
        this.state = {
            productos : [],
            recetas : [],
            productosFiltrados : [],
            data : []
        }


        axios.get('http://localhost:5295/api/producto').then(response => {
            this.setState({ productos: response.data });
            this.setState({ productosFiltrados: response.data });
        })
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

        updatedList = updatedList.filter((item)=> item.nombre !== query);

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
            obj.nombre.toLowerCase().indexOf(query.toLowerCase()) >=0);

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


    render(){
        return(
            <div className='row'>
                <div className="search-header">
                        <div className="search-text">Buscar:</div>
                        <input id="search-box" onChange={this.filterBySearch} />
                    </div>
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
            
            
        )
    }

    
}
export default ConsumableList;