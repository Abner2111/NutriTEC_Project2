/* eslint-disable */
import React, { Component } from 'react';
import { NavbarNutricionista } from '../Templates/NavbarNutricionista';
import axios from 'axios';
import { Form } from 'react-bootstrap';


class BusquedaAsociacionPacientes extends Component {
  constructor(props) {
    super(props);
  
    this.state = {
      idCliente: sessionStorage.getItem("miId"),
      clientes: [], // lista de planes obtenidos desde el API
      mispacientes: [],
      id: "",
      
      error: null, // variable para guardar posibles errores del API
    };
  }

   asociarCliente = (cliente) => {
    console.log(cliente);
    
    axios
      .post("http://localhost:5295/api/nutricionista/Asociar", {
        nutricionista: this.state.idCliente,
        cliente: cliente.correo,
      })
      .then((response) => {
        this.handleMisPacientes(); // Refresh site
      })
      .catch((error) => {
        this.setState({ error: error.message });
      });
  };

  componentDidMount() {
    this.handleMisPacientes(); // se obtiene la lista de sucursales
  }

  handleMisPacientes = () => {
    axios.get('http://localhost:5295/api/nutricionista/Asociados/'+this.state.idCliente) // obtiene la lista de sucursales desde el API
      .then(response => {
        this.setState({ mispacientes: response.data }); // guarda la lista de sucursales en el estado
      })
      .catch(error => {
        this.setState({ error: error.message }); // guarda el error en el estado en caso de que haya alguno
      });
  }


  handleSubmit = (event) => {
    event.preventDefault();

    var critBusqueda = document.getElementById('busqueda').options[document.getElementById('busqueda').selectedIndex].value;
    var paraBuscar = document.getElementById('aBuscar').value;

    if (critBusqueda == 'Correo'){
        axios.get('http://localhost:5295/api/Cliente/PorCorreo/'+paraBuscar) // obtiene la lista de sucursales desde el API
        .then(response => {
            this.setState({ clientes: response.data }); // guarda la lista de sucursales en el estado
        })
            .catch(error => {
            this.setState({ error: error.message }); // guarda el error en el estado en caso de que haya alguno
        });
    }

    if (critBusqueda == 'Nombre'){
        axios.get('http://localhost:5295/api/Cliente/PorNombre/'+paraBuscar) // obtiene la lista de sucursales desde el API
        .then(response => {
            this.setState({ clientes: response.data }); // guarda la lista de sucursales en el estado
        })
            .catch(error => {
            this.setState({ error: error.message }); // guarda el error en el estado en caso de que haya alguno
        });
    }
  }


  // función que renderiza el componente
render() {
  const { clientes, mispacientes, error } = this.state;

  return (
    <div style={{ backgroundColor: '#fff', textAlign: 'center' }}>
        <NavbarNutricionista/>
  <h1 style={{ margin: '50px 0', fontSize: '2.5rem', fontWeight: 'bold', textTransform: 'uppercase' }}>Búsqueda y asociación de pacientes</h1>
      {error && <div>Error: {error}</div>}

      <Form onSubmit={this.handleSubmit}>
          <h2>Nuevo plan</h2>
          <div className="form-input">
            <label htmlFor="aBuscar">Buscar:</label>
            <br></br>
            <input
              type="text"
              id="aBuscar"
              required
            />
          </div>
          <div className="form-input">
            <label htmlFor="busqueda">Criterio de búsqueda:</label>
            <br></br>
            <select name="busqueda" id="busqueda">
              <option value="Correo">Correo</option>
              <option value="Nombre">Nombre</option>
            </select>
          </div>

          <div style={{marginTop: "20px", marginBottom: "20px"}}>
            <button type="submit" style={{
              backgroundColor: "#fff",
              borderBottom: "1px solid #000",
              border: "1px solid #00008B",
              color: "#00008B",
              cursor: "pointer",
              display: "block",
              margin: "0 auto",
              padding: "10px 20px"
            }}>Buscar</button>
            </div>
        </Form>
        
      <table style={{ borderCollapse: 'collapse', width: '80%', margin: '0 auto 2em'}} className="table border shadow">
        <thead>
          <tr>
            <th style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>Correo</th>
            <th style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>Nombre completo</th>
            <th style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>Fecha registro</th>
            <th style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>País</th>
            <th style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>Estatura</th>
            <th style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>Peso</th>
            <th style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>Asociar</th>
          </tr>
        </thead>
        
        <tbody>
            {clientes.map(cliente => (
            <tr key={(cliente.correo)}>
              <td style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>{cliente.correo}</td>
              <td style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>{cliente.nombre}</td>
              <td style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>{cliente.fecha_registro}</td>
              <td style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>{cliente.pais}</td>
              <td style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>{cliente.estatura}</td>
              <td style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>{cliente.peso}</td>
              <td style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}> 
                <button style={{ borderRadius: '5px', backgroundColor: '#fff', color: '#4CAF50', border: '2px solid #4CAF50', cursor: 'pointer' }} 
                onClick={() => this.asociarCliente(cliente)}>Asociar</button>
              </td>
            </tr>
          ))}
        </tbody>
        
      </table>


      <h1 style={{ margin: '50px 0', fontSize: '2.5rem', fontWeight: 'bold', textTransform: 'uppercase' }}>Mis pacientes</h1>
      {error && <div>Error: {error}</div>}
        
      <table style={{ borderCollapse: 'collapse', width: '80%', margin: '0 auto 2em'}} className="table border shadow">
        <thead>
          <tr>
            <th style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>Correo</th>
            <th style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>Nombre completo</th>
            <th style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>Fecha registro</th>
            <th style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>País</th>
            <th style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>Estatura</th>
            <th style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>Peso</th>
          </tr>
        </thead>
        
        <tbody>
            {mispacientes.map(cliente => (
            <tr key={(cliente.correo)}>
              <td style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>{cliente.correo}</td>
              <td style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>{cliente.nombre} {cliente.apellido1} {cliente.apellido2}</td>
              <td style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>{cliente.fecha_registro}</td>
              <td style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>{cliente.pais}</td>
              <td style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>{cliente.estatura}</td>
              <td style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>{cliente.peso}</td>
            </tr>
          ))}
        </tbody>
        
      </table>

      </div>
    );
  }
}

export default BusquedaAsociacionPacientes;