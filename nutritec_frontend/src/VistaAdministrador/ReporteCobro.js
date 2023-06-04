import React, { Component } from 'react';
import axios from 'axios';

import { NavbarAdministrador } from '../Templates/NavbarAdministrador';

import jsPDF from 'jspdf';
import 'jspdf-autotable'

class ReporteCobro extends Component {
  constructor(props) {
    super(props);
  
    this.state = {
      reportes: [], // lista de reportes obtenidos desde el API
      columnas: [
        {title: "Correo", field: "correo"},
        {title: "Nombre", field: "nombre_completo"},
        {title: "Tarjeta", field: "tarjeta_credito"},
        {title: "Tipo de cobro", field: "descripcion"},
        {title: "Monto total ($)", field: "clientes"},
        {title: "Descuento (%)", field: "descuento"},
        {title: "Monto a cobrar ($)", field: "monto_cobrar"},
      ],
      error: null, // variable para guardar posibles errores del API
    };
  }


  /* PARA COMPONENTES */
  // función que se ejecuta cuando se carga el componente
  componentDidMount() {
    this.handleReporteCobro(); // se obtiene la lista de sucursales
  }

  // función para obtener la lista de sucursales, y teléfonos desde el API
  handleReporteCobro = () => {
    axios.get('http://localhost:5295/api/ReporteCobro') // obtiene la lista de sucursales desde el API
      .then(response => {
        this.setState({ reportes: response.data }); // guarda la lista de sucursales en el estado
        console.log(response.data);
      })
      .catch(error => {
        this.setState({ error: error.message }); // guarda el error en el estado en caso de que haya alguno
      });
  }

  generatePDF = (x) => {
    console.log(x)
    const doc = new jsPDF()
    doc.text("Reporte de cobro de "+x.nombre_completo,14,10)
    doc.autoTable({
      columns: this.state.columnas.map(col=>({...col,dataKey:col.field})),
      body: [x],
      theme: 'grid'
    })
    doc.save('ReporteCobro.pdf')
  }

  // función que renderiza el componente
render() {
  const { reportes, error } = this.state;

  return (
    <div style={{ backgroundColor: '#fff', textAlign: 'center' }}>
        <NavbarAdministrador/>
  <h1 style={{ margin: '50px 0', fontSize: '2.5rem', fontWeight: 'bold', textTransform: 'uppercase' }}>Reportes de cobro</h1>
      {error && <div>Error: {error}</div>}
      {reportes.map(reporte => (
        
      <table style={{ borderCollapse: 'collapse', width: '80%', margin: '0 auto 2em'}} className="table border shadow">
        <thead>
          <tr>
            <th style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>Correo del nutricionista</th>
            <th style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>Nombre del nutricionista</th>
            <th style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>Número de tarjeta</th>
            <th style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>Tipo de cobro</th>
            <th style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>Monto total</th>
            <th style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>Descuento</th>
            <th style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>Monto a cobrar</th>
            <th style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>PDF</th>
          </tr>
        </thead>
        
        <tbody>
          
            <tr key={(reporte)}>
              <td style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>{reporte.correo}</td>
              <td style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>{reporte.nombre_completo}</td>
              <td style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>{reporte.tarjeta_credito}</td>
              <td style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>{reporte.descripcion}</td>
              <td style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>${reporte.clientes}</td>
              <td style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>{reporte.descuento}%</td>
              <td style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>${reporte.monto_cobrar}</td>
              <td style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}> 
                <button style={{ borderRadius: '5px', backgroundColor: '#fff', color: '#ccdb19', border: '2px solid #ccdb19', cursor: 'pointer' }} 
                onClick={() => this.generatePDF(reporte)}>Generar PDF</button> 
              </td>
            </tr>
          
        </tbody>
        
      </table>
      
      ))}
      
      </div>
    );
  }
}

export default ReporteCobro;