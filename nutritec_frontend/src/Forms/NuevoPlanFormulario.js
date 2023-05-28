import React, { Component } from "react";
import axios from "axios";
import { Form } from 'react-bootstrap';


class NuevoPlanFormulario extends Component {
  constructor(props) {
    super(props);

    this.state = {
      nombre: "",
      nutricionistid: "",
      showModal: false,
    };

    this.handleChange = this.handleChange.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
    this.handleOuterClick = this.handleOuterClick.bind(this);
  }

  handleSubmit = (event) => {
    event.preventDefault();

    // Enviar los datos al backend para crear un nuevo plan
    axios
      .post("http://localhost:5295/api/plan", {
        nombre: this.state.nombre,
        nutricionistid: this.state.nutricionistid
      })
      .then((response) => {
        this.props.onNewPlan(); // Refresh site
      })
      .catch((error) => {
        this.setState({ error: error.message });
      });

    console.log("Nuevo plan agregado");
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
          maxWidth: '300px',
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
          <h2>Nuevo plan</h2>
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
            }}>Agregar plan</button>
            </div>

      </Form>
    </div>
            );
    }
}
export default NuevoPlanFormulario;