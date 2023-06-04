import React, { Component } from 'react';
import axios from 'axios';

import { NavbarAdministrador } from '../Templates/NavbarAdministrador';


class ReporteCobro extends Component {
  constructor(props) {
    super(props);
  
    this.state = {
      reportes: [], // lista de reportes obtenidos desde el API

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
          </tr>
        </thead>
        
        <tbody>
          
            <tr key={(reporte)}>
              <td style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>{reporte.correo}</td>
              <td style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>{reporte.nombre} {reporte.apellido1} {reporte.apellido2}</td>
              <td style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>{reporte.tarjeta_credito}</td>
              <td style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>{reporte.descripcion}</td>
              <td style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>${reporte.clientes}</td>
              <td style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>{reporte.descuento}%</td>
              <td style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>${reporte.monto_cobrar}</td>
            </tr>
          
        </tbody>
        
      </table>
      
      ))}
      
      </div>
    );
  }
}

export default ReporteCobro;