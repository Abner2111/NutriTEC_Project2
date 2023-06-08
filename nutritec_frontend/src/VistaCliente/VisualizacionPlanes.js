import React, { Component } from "react";
import { NavbarCliente } from "../Templates/NavbarCliente";
import { obtenerPlanesCliente,obtenerPlanes } from "../Api";
import {Column, HeaderCell, Table,Cell} from "rsuite-table";
import "rsuite-table/dist/css/rsuite-table.css";
class VisualizacionPlanes extends Component{
    constructor(props){
        super(props)
        this.state = {
            planesCliente: [],
            planes: []
        }
        if ('userEmail' in localStorage && localStorage.getItem('userEmail') !== ''){
            this.getPlanesCliente();
            this.getPlanes();
        }

    }

    
    /* These are two asynchronous functions that are used to retrieve data from the API. */
    getPlanesCliente = async() => {
        const data = await obtenerPlanesCliente(localStorage.getItem('userEmail'));
        console.log(data);
        this.setState({planesCliente : data});
    }

    getPlanes = async() => {
        const data = await obtenerPlanes();
        this.setState({planes : data});
    }
    
    render(){
        if ('userEmail' in localStorage && localStorage.getItem('userEmail') !== ''){
            return(
                
                <div>
                    <NavbarCliente/>
                    <h1>Visualizacion de Planes de Alimentación</h1>
                    <Table data={this.state.planesCliente} height={500}>
                        <Column width={300} align="center" fixed resizable>
                            <HeaderCell>Plan ID</HeaderCell>
                            <Cell dataKey = "planId"/>
                        </Column>
                        <Column width={300} align="center" fixed resizable>
                            <HeaderCell>Fecha de Inicio</HeaderCell>
                            <Cell dataKey = "fecha_inicio"/>
                        </Column>
                        <Column width={300} align="center" fixed resizable>
                            <HeaderCell>Fecha de Finalización</HeaderCell>
                            <Cell dataKey = "fecha_final"/>
                        </Column>

                    </Table>
                </div>
            )
        } else {
            return(
                <div>
                    <NavbarCliente/>
                    <h1>Visualizacion de Planes de Alimentación</h1>
                    <h>NO SE HA INGRESADO UN USUARIO</h>
                </div>
            )
        }
    }

}
export default VisualizacionPlanes;