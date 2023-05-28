import React, { Component } from "react";
import axios from "axios";
import { Form } from 'react-bootstrap';


class EditarPlanFormulario extends Component {
  constructor(props) {
    super(props);

    this.state = {
      id: this.props.editName.plan.id,
      nombre: this.props.editName.plan.nombre,
      nutricionistid: this.props.editName.plan.nutricionistid,

      showModal: false,
    };

    this.handleChange = this.handleChange.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
    this.handleOuterClick = this.handleOuterClick.bind(this);
  }

  handleSubmit = (event) => {
    event.preventDefault();

    // Enviar los datos al backend para crear una nueva sucursal
    axios
      .put("http://localhost:5295/api/plan/"+this.state.id, {
        id: this.state.id,
        nombre: this.state.nombre,
        nutricionistid: this.state.nutricionistid,
      })
      .then((response) => {
        // Actualizar el estado de los pacientes con los nuevos datos ingresados
        this.props.onEditPlan();
      })
      .catch((error) => {
        this.setState({ error: error.message });
      });

    console.log("Plan editado");
    this.props.onClose();
  };

  handleChange = (event) => {
    this.setState({ [event.target.name]: event.target.value });
  };

  handleOuterClick(event) {
    const container = document.querySelector('.container1');
    if (container && !container.contains(event.target)) {
      this.props.onClose();
    }
  };

  componentDidMount() {
    document.addEventListener('mousedown', this.handleOuterClick);
  };

  componentWillUnmount() {
    document.removeEventListener('mousedown', this.handleOuterClick);
  };

  render() {    
    return (
      <div
        className="container1"
        style={{
          maxWidth: '250px',
          margin: '0 auto',
          marginTop: '20px',
          textAlign: 'center',
          position: 'absolute',
          top: '50%',
          left: '50%',
          transform: 'translate(-50%, -50%)',
          backgroundColor: 'white',
          padding: '20px',
          boxShadow: '0 2px 8px rgba(0, 0, 0, 0.2)',
          zIndex: '1',
        }}
      >
        <Form onSubmit={this.handleSubmit}>
          <h2>Editar plan</h2>
          <div className="form-input">
            <label htmlFor="id">Id:</label>
            <input
              type="text"
              name="id"
              value={this.state.id}
              onChange={this.handleChange}
              required
              disabled
            />
          </div>
          <div className="form-input">
            <label htmlFor="nombre">Nombre:</label>
            <input
              type="text"
              name="nombre"
              value={this.state.nombre}
              onChange={this.handleChange}
              required
            />
          </div>
          <div className="form-input">
            <label htmlFor="nutricionistid">Id del nutricionista:</label>
            <input
              type="text"
              name="nutricionistid"
              value={this.state.nutricionistid}
              onChange={this.handleChange}
              required
            />
          </div>
          
            <div style={{marginTop: "20px"}}>
            <button type="submit" style={{
              backgroundColor: "#fff",
              borderBottom: "1px solid #000",
              border: "1px solid #00008B",
              color: "#00008B",
              cursor: "pointer",
              display: "block",
              margin: "0 auto",
              padding: "10px 20px"
            }}>Editar sucursal</button>
            </div>

      </Form>
    </div>
            );
    }
}
export default EditarPlanFormulario;