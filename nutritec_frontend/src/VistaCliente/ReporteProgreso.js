import React, { Component } from "react";
import { obtenerMedidasCliente } from "../Api";
import '../Templates/diseñoH.css';
import '../Templates/diseño.css';

import { NavbarCliente } from "../Templates/NavbarCliente";

import jsPDF from 'jspdf';
import 'jspdf-autotable'

class ReporteProgreso extends Component{
    constructor(props){
        super(props);
        this.state = {
            medidas: [],
            columnas: [
                {title: "Fecha", field: "fecha"},
                {title: "Cuello (cm)", field: "medidacuello"},
                {title: "Cintura (cm)", field: "medidacintura"},
                {title: "Cadera (cm)", field: "medidacadera"},
                {title: "Grasa (%)", field: "porcentajegrasa"},
                {title: "Músculo (%)", field: "porcentajemusculo"},
            ],
            error: null
        };


        

        
    }
    /* `getMedidas` is an asynchronous function that is using the `await` keyword to wait for the
        `obtenerMedidasCliente` function to return a value before continuing. The
        `obtenerMedidasCliente` function is being passed the value of `localStorage('userEmail')` as
        an argument. Once the value is returned, it is being stored in the `data` variable. Finally,
        the `setState` method is being used to update the `medidas` property of the component's
        state with the value of `data`. */
    getMedidas = async () =>{
        const data = await obtenerMedidasCliente(localStorage.getItem('userEmail'));
        this.setState({medidas : data});
        console.log(this.state.medidas)
    }

    generatePDF = () => {
        const doc = new jsPDF()
        doc.text("Reporte de progreso",14,10)
        doc.autoTable({
          columns: this.state.columnas.map(col=>({...col,dataKey:col.field})),
          body: this.state.medidas.map(dat=>({...dat, dataKey:dat.field})),
          theme: 'grid'
        })
        doc.save('ReporteProgreso.pdf')
      }

    componentDidMount(){
        this.getMedidas();
    }

    render(){
        const{medidas} = this.state;
        return (
            <div className="gestion-productos-container">
                <NavbarCliente/>
                <h1 style={{ margin: '50px 0', fontSize: '2.5rem', fontWeight: 'bold', textTransform: 'uppercase' }}>Reporte avance</h1>
                <table className="tabla-productos">
                    <thead>
                        <tr>
                            <th>Fecha</th>
                            <th>Cuello (cm)</th>
                            <th>Cintura (cm)</th>
                            <th>Cadera (cm)</th>
                            <th>% Grasa</th>
                            <th>% Músculo</th>
                        </tr>
                    </thead>
                    <tbody>
                        {medidas.map((medida) =>(
                            <tr key={medida.correocliente + medida.fecha}>
                                <td>{medida.fecha}</td>
                                <td>{medida.medidacuello}</td>
                                <td>{medida.medidacintura}</td>
                                <td>{medida.medidacadera}</td>
                                <td>{medida.porcentajegrasa}</td>
                                <td>{medida.porcentajemusculo}</td>
                            </tr>
                        ))}
                    </tbody>
                </table>
                <button style={{ borderRadius: '5px', backgroundColor: '#fff', color: '#ccdb19', border: '2px solid #ccdb19', cursor: 'pointer', marginTop: '20px' }} 
                onClick={() => this.generatePDF()}>Generar PDF</button> 

            </div>
        )
    }
}
export default ReporteProgreso;