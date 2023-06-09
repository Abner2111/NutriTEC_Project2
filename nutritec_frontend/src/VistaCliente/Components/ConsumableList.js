import React, {Component} from 'react';
import ConsumableRow from './ConsumableRow';
import SelectedConsumableRow from './SelectedConsumableRow';
import MealTimeSelector from './MealTimeSelector';
import { agregarConsumoProducto, agregarConsumoReceta, obtenerProductos, obtenerRecetas } from '../../Api';

class ConsumableList extends Component {
    constructor(props){
        super(props);
        this.state = {

            productos : [], //productos obtenidos del api
            recetas : [], //recetas obtenidas del api
            consumibles: [],
            productosFiltrados : [],
            selectedConsumibles : [],
            selectedMealTime:''
        }

        
        this.getProducts();
        this.getRecetas();
        
        
    } 

    /** 
     * gets products from a api
     */
    getProducts = async () => {
        const data = await obtenerProductos();
        this.setState({productos: data});
    }
    /**
     * gets recipes from api
     */
    getRecetas = async () =>{
        const data = await obtenerRecetas();
        
        this.setState({recetas: data});
        
        setTimeout(()=>{
            let consumableList = [...this.state.productos,...this.state.recetas];
            
            this.setState({consumibles : consumableList});
            this.setState({productosFiltrados : consumableList});
            console.log(this.state.productosFiltrados.length);
            console.log(this.state.consumibles.length);
        },500);
        
        
    }

   

    
    /**
     * adds selected consumables to the list of selected consumables
     * @param {*} childdata 
     */
    childToParentAdd = (childdata) =>{
        
        this.setState({selectedConsumibles: [...this.state.selectedConsumibles, childdata]});
    }

    /**
     * deletes the consumables from the selected consumables
     * @param {*} childdata 
     */
    childToParentDelete = (childdata) =>{
        const query = childdata.nombre;
        
        var updatedList = this.state.selectedConsumibles;

        const index = updatedList.findIndex((i) => i.nombre === query);
        updatedList.splice(index,1);
        this.setState({selectedConsumibles:updatedList})
    }
    /**
     * filtering consumables
     * @param {*} event 
     */
    filterBySearch = (event) => {
        
        const query = event.target.value;
        
        var updatedList = this.state.consumibles;

       
        
        updatedList = updatedList.filter(function (obj){
            if(obj.id!=null){
                return obj.nombre.toLowerCase().indexOf(query.toLowerCase()) >=0 || obj.codigoBarras.toLowerCase().indexOf(query.toLowerCase()) >=0;
            } else {
                return obj.nombre.toLowerCase().indexOf(query.toLowerCase()) >=0;
            }
            
        });

        this.setState({productosFiltrados : updatedList}); 
        
        
    }
    /**
     * crea una fila de consumible basado en un json de producto
     * @param {*} product 
     * @returns 
     */
    createProduct = (consumible) => {
        
        if(consumible.id!=null){
            return < ConsumableRow 
                id = {consumible.id}
                nombre = {consumible.nombre}
                codigoBarras = {consumible.codigoBarras}
                childToParent = {this.childToParentAdd}
                />
        } else {
            return < ConsumableRow 
                id = {"N/A"}
                nombre = {consumible.nombre}
                codigoBarras = "N/A"
                childToParent = {this.childToParentAdd}
                />
        }
        
    }

    /**
     * creates the elements of the selected products area
     * @param {*} param0 
     * @returns 
     */
    createProductSelection = (consumible) => {
        if(consumible.id!=null){
            return < SelectedConsumableRow 
                id = {consumible.id}
                nombre = {consumible.nombre}
                codigoBarras = {consumible.codigoBarras}
                childToParent = {this.childToParentDelete}
                />
        } else {
            return < SelectedConsumableRow 
                id = {"N/A"}
                nombre = {consumible.nombre}
                codigoBarras = "N/A"
                childToParent = {this.childToParentDelete}
                />
        }
        
    }

    /**
     * *registers consumables using api function to do it
     */
    registerConsumables = () => {
        let selectedMealTime = this.state.selectedMealTime;
        this.state.selectedConsumibles.forEach(function(item,index){
            if (item.id!=="N/A"){
                agregarConsumoProducto(localStorage.getItem('userEmail'), item.id, selectedMealTime)
            } else {
                agregarConsumoReceta(localStorage.getItem('userEmail'),item.nombre, selectedMealTime)
            }
            
            
        })
        this.setState({selectedConsumibles: []});
    }

    handleCallbackFromMealSelector = (childData) => {
        console.log(childData);
        this.setState({selectedMealTime: childData})
    }

    printSelectec = () => {
        console.log(this.state.selectedMealTime);
    }
    render(){
        if ('userEmail' in localStorage && localStorage.getItem('userEmail') !== ''){
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
                                {this.state.selectedConsumibles.map(this.createProductSelection)}
                            </div>
                        </div>
                        
                    </div>
                    <button onClick={this.registerConsumables} style={{ color: '#FFF', backgroundColor: '#008CBA', borderRadius: '12px', padding: '12px', border: '2px solid #008CBA' }}>Registrar Consumo</button>
                </div>
                
                
                
            );
        } else {
            return(
                <div>
                    <h>NO SE HA INGRESADO UN USUARIO</h>
                </div>
            )
        }
        } 
       
        

    
}
export default ConsumableList;