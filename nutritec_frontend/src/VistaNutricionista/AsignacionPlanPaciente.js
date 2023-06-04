import React, { Component } from 'react';
import { NavbarNutricionista } from '../Templates/NavbarNutricionista';
import axios from 'axios';

import { CSSTransition } from 'react-transition-group';

import AsignarPlanFormulario from '../Forms/AsignarPlanFormulario';


class AsignacionPlanPaciente extends Component {
  constructor(props) {
    super(props);
  
    this.state = {
      clientes: [], // lista de clientes obtenidos desde el API
      
      showForm: false, // variable para mostrar/ocultar el formulario para asignar planes

      showDialog: false, // variable para mostrar/ocultar el diálogo para asignar nuevos planes
      
      error: null, // variable para guardar posibles errores del API
    };
  }

  /* FORMS */

  /* PARA AGREGAR SUCURSALES */
  // Función para alternar la visibilidad del formulario para agregar sucursal
  toggleForm = () => {
    this.setState({ showForm: !this.state.showForm });
  };



  /* DIÁLOGOS */
  // Función para alternar la visibilidad del diálogo para agregar nueva sucursal
  toggleDialog = () => {
    this.setState(prevState => ({ showDialog: !prevState.showDialog }));
  };



  /* PARA COMPONENTES */
  // función que se ejecuta cuando se carga el componente
  componentDidMount() {
    this.handleCliente(); // se obtiene la lista de sucursales
  }

  // función para obtener la lista de sucursales, y teléfonos desde el API
  handleCliente = () => {
    axios.get('http://localhost:5295/api/Cliente/planes') // obtiene la lista de sucursales desde el API
      .then(response => {
        this.setState({ clientes: response.data }); // guarda la lista de sucursales en el estado
        console.log(response.data);
      })
      .catch(error => {
        this.setState({ error: error.message }); // guarda el error en el estado en caso de que haya alguno
      });
  }

  // función para abrir el diálogo para agregar nuevas sucursales
  openDialog() {
    this.setState({ isOpen: true });
    document.body.style.overflow = "hidden";
    document.getElementById("root").classList.add("blur");
    document.querySelector(".dialog").classList.add("dialog-enter");
  }

  // función para cerrar el diálogo para agregar nuevas sucursales
  closeDialog() {
    this.setState({ isOpen: false });
    document.body.style.overflow = "auto";
    document.getElementById("root").classList.remove("blur");
    document.querySelector(".dialog").classList.add("dialog-exit");
    setTimeout(() => {
      document.querySelector(".dialog").classList.remove("dialog-enter", "dialog-exit");
    }, 500); // espera a que termine la transición antes de remover las clases
  }

  // función que renderiza el componente
render() {
  const { clientes, error, showDialog } = this.state;

  return (
    <div style={{ backgroundColor: '#fff', textAlign: 'center' }}>
        <NavbarNutricionista/>
  <h1 style={{ margin: '50px 0', fontSize: '2.5rem', fontWeight: 'bold', textTransform: 'uppercase' }}>Planes de clientes</h1>
      {error && <div>Error: {error}</div>}
      
        
      <table style={{ borderCollapse: 'collapse', width: '80%', margin: '0 auto 2em'}} className="table border shadow">
        <thead>
          <tr>
            <th style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>Cliente</th>
            <th style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>Plan</th>
            <th style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>Fecha inicio</th>
            <th style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>Fecha final</th>
          </tr>
        </thead>
        
        <tbody>
            {clientes.map(cliente => (
            <tr key={(cliente.cliente)}>
              <td style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>{cliente.cliente}</td>
              <td style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>{cliente.planId}</td>
              <td style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>{cliente.fecha_inicio}</td>
              <td style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>{cliente.fecha_final}</td>
            </tr>
          ))}
        </tbody>
        
      </table>
      
      
      <div>
        <button style={{ marginTop: '20px', padding: '10px 20px', borderRadius: '5px', backgroundColor: '#fff', color: '#4CAF50', border: '2px solid #4CAF50', cursor: 'pointer' }} 
        onClick={this.toggleDialog}>Asignar plan</button>
        {showDialog && (
            <div
              style={{
                position: "fixed",
                top: 0,
                left: 0,
                width: "100%",
                height: "100%",
                background: "rgba(0, 0, 0, 0.5)", // fondo semitransparente
                display: "flex",
                justifyContent: "center",
                alignItems: "center",
                zIndex: 999 // asegurarse de que el diálogo esté por encima del resto del contenido
              }}
            >
              <div>
            <CSSTransition in={this.state.isOpen} classNames="dialog" timeout={500}>
            <div
              className="dialog"
              style={{
                backgroundColor: "#fff",
                padding: "20px",
                borderRadius: "5px",
                maxWidth: "80%",
                maxHeight: "80%",
                overflow: "auto",
                marginBottom: "5px",
                display: "flex",
                alignItems: "center",
                justifyContent: "center",
                boxShadow: "0px 0px 10px rgba(0, 0, 0, 0.5)", // sombra para dar profundidad
              }}
            >
              {/* contenido del diálogo */}
              <AsignarPlanFormulario 
                onClose={this.toggleDialog}
                onNewPlan={this.handleCliente}
              />
              
            </div>
          </CSSTransition>
            </div>
          </div>
          )}
      </div>
      

      </div>
    );
  }
}

export default AsignacionPlanPaciente;