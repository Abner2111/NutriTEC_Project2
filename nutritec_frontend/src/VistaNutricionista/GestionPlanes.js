import React, { Component } from 'react';
import { NavbarNutricionista } from '../Templates/NavbarNutricionista';
import axios from 'axios';

import { CSSTransition } from 'react-transition-group';

import NuevoPlanFormulario from '../Forms/NuevoPlanFormulario';
import EditarPlanFormulario from '../Forms/EditarPlanFormulario';


class GestionPlanes extends Component {
  constructor(props) {
    super(props);
  
    this.state = {
      planes: [], // lista de planes obtenidos desde el API
      comidas: [],
      showForm: false, // variable para mostrar/ocultar el formulario para agregar sucursales
      showtwoForm: false, // variable para mostrar/ocultar el formulario para añadir información adicional a una sucursal existente
      showthreeForm: false,

      showDialog: false, // variable para mostrar/ocultar el diálogo para agregar nuevos sucursal
      showtwoDialog: false, // variable para mostrar/ocultar el diálogo para añadir información adicional a una sucursal existente
      
      error: null, // variable para guardar posibles errores del API
    };
  }

  /* FORMS */

  /* PARA AGREGAR SUCURSALES */
  // Función para alternar la visibilidad del formulario para agregar sucursal
  toggleForm = () => {
    this.setState({ showForm: !this.state.showForm });
  };

  // Función para alternar la visibilidad del formulario para editar sucursal
  togglet = () => {
    this.setState({ showtwoForm: !this.state.showtwoForm });
  };

  // Función para alternar la visibilidad del formulario para eliminar sucursal
  toggleth = () => {
    this.setState({ showthreeForm: !this.state.showthreeForm });
  };



  /* DIÁLOGOS */
  // Función para alternar la visibilidad del diálogo para agregar nueva sucursal
  toggleDialog = () => {
    this.setState(prevState => ({ showDialog: !prevState.showDialog }));
  };

  // Función para alternar la visibilidad del diálogo para editar sucursal
  toggletD = () => {
    this.setState(prevState => ({ showtwoDialog: !prevState.showtwoDialog }));
  };

  // Función para alternar la visibilidad del diálogo para eliminar sucursal
  togglethD = () => {
    this.setState(prevState => ({ showthreeDialog: !prevState.showthreeDialog }));
  };

  getPlan = (x) => {
    this.setState({plan: x})
    this.toggletD()
  }

  deletePlan = (id) => {
    axios
      .delete("http://localhost:5295/api/plan/"+id, {
        
      })
      .then((response) => {
        this.handlePlan();
      })
      .catch((error) => {
        alert("No ha sido posible eliminar este plan")
      });

    console.log("Plan eliminado");
  };

  /* PARA COMPONENTES */
  // función que se ejecuta cuando se carga el componente
  componentDidMount() {
    this.handlePlan(); // se obtiene la lista de sucursales
  }

  // función para obtener la lista de sucursales, y teléfonos desde el API
  handlePlan = () => {
    axios.get('http://localhost:5295/api/Plan') // obtiene la lista de sucursales desde el API
      .then(response => {
        this.setState({ planes: response.data }); // guarda la lista de sucursales en el estado
      })
      .catch(error => {
        this.setState({ error: error.message }); // guarda el error en el estado en caso de que haya alguno
      });

    axios.get('http://localhost:5295/api/Plan/Comidas') // obtiene la lista de teléfonos para cada paciente desde el API
      .then(response => {
        const comidas = {};
        response.data.forEach(comida => {
          if (!comidas[comida.id]) { // si no existe una entrada para el paciente actual en la lista de teléfonos, se crea una
            comidas[comida.id] = [];
          }
          comidas[comida.id].push(comida.tiempocomida); // se agrega el teléfono actual a la lista de teléfonos del paciente
          comidas[comida.id].push(": ");
          comidas[comida.id].push(comida.comida);
          comidas[comida.id].push(<br></br>);
        });
        this.setState({ comidas }); // se guarda la lista de teléfonos en el estado
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
  const { planes, error, showDialog, showtwoDialog } = this.state;

  return (
    <div style={{ backgroundColor: '#fff', textAlign: 'center' }}>
        <NavbarNutricionista/>
  <h1 style={{ margin: '50px 0', fontSize: '2.5rem', fontWeight: 'bold', textTransform: 'uppercase' }}>Planes</h1>
      {error && <div>Error: {error}</div>}
      {planes.map(plan => (
        
      <table style={{ borderCollapse: 'collapse', width: '80%', margin: '0 auto 2em'}} className="table border shadow">
        <thead>
          <tr>
            <th style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>Nombre</th>
            <th style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>Nutricionista</th>
            <th style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>Comida</th>
            <th style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>Total calorías</th>
            <th style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>Editar</th>
            <th style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>Eliminar</th>
          </tr>
        </thead>
        
        <tbody>
          
            <tr key={(plan)}>
              <td style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>{plan.nombre}</td>
              <td style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>{plan.nutricionistid}</td>
              <td style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>{this.state.comidas[plan.id] ? this.state.comidas[plan.id] : '' }</td>
              <td style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}>UC</td>
              <td style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}> 
                <button style={{ borderRadius: '5px', backgroundColor: '#fff', color: '#ccdb19', border: '2px solid #ccdb19', cursor: 'pointer' }} 
                onClick={() => this.getPlan(plan)}>Editar</button> 
              </td>
              <td style={{ padding: '10px', borderBottom: '1px solid #1c3a56' }}> 
                <button style={{ borderRadius: '5px', backgroundColor: '#fff', color: '#c92d15', border: '2px solid #c92d15', cursor: 'pointer' }} 
                onClick={() => this.deletePlan(plan.id)}>Eliminar</button>
              </td>
            </tr>
          
        </tbody>
        
      </table>
      
      ))}
      <div> 
        <button style={{ marginTop: '20px', padding: '10px 20px', borderRadius: '5px', backgroundColor: '#fff', color: '#4CAF50', border: '2px solid #4CAF50', cursor: 'pointer' }} 
        onClick={this.toggleDialog}>Nuevo plan</button>
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
              <NuevoPlanFormulario 
                onClose={this.toggleDialog}
                onNewPlan={this.handlePlan}
              />
              
            </div>
          </CSSTransition>
            </div>
          </div>
          )}
      </div>
      

      {showtwoDialog && (
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
            <EditarPlanFormulario
              editName={this.state}
              onClose={this.toggletD}
              onEditPlan={this.handlePlan}
            />
            
          </div>
        </CSSTransition>
          </div>
        </div>
        )}

      </div>
    );
  }
}

export default GestionPlanes;