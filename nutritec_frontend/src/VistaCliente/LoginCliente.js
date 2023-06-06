import React, { useEffect } from "react";
import { registrarCliente, validarCliente} from "../Api";
import '../Templates/diseñoH.css';
import '../Templates/diseño.css';
import { MDBContainer, MDBRow, MDBCol, MDBInput, MDBBtn } from 'mdb-react-ui-kit';

class LoginCliente extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      formValues: {
        correo: '',
        nombre: '',
        apellido1: '',
        apellido2: '',
        contrasena: '',
        pais: '',
        fecha_registro: '',
        fecha_nacimiento: '',
        estatura: '',
        peso: ''
      },
      currentemail: '',
      currentpass: '',
      showPopup: 0
    };
    this.handleOuterClick = this.handleOuterClick.bind(this);
    


    
  }
  
  


  handleSubmit = async (event, tipo, obj) => {
    event.preventDefault();
    if (tipo === 1) {
      const data = await registrarCliente(obj);
      console.log(data);
    }
    if (tipo === 2) {
      console.log(this.state.currentemail, this.state.currentpass);
      const data = await validarCliente(this.state.currentemail, this.state.currentpass);
      console.log(data);
      localStorage.setItem('userEmail',this.state.currentemail);
      window.location = '/vistacliente';
      
      
    }
  };
  
  handleInputChange = (event) => {
    const { name, value } = event.target;
    this.setState({
      [name]: value
    });
  };

  handleInputChangeR = (event) => {
    const { name, value } = event.target;
  
    this.setState((prevState) => ({
      formValues: {
        ...prevState.formValues,
        [name]: value
      }
    }));
  };
  
  handleRegistro = async (event) => {
    event.preventDefault();
    console.log(this.state.formValues);
    const date = new Date();
      let currentDay= String(date.getDate()).padStart(2, '0');

      let currentMonth = String(date.getMonth()+1).padStart(2,"0");
      
      let currentYear = date.getFullYear();

      let currentDate = `${currentYear}-${currentMonth}-${currentDay}`;
    this.state.formValues.FechaRegistro = currentDate;
    const data = await registrarCliente(this.state.formValues);
    console.log(data);
    this.setState({ showPopup: 0 })
  };

  componentDidMount() {
    document.addEventListener('mousedown', this.handleOuterClick);
    this.setState({ showPopup: 0 })
  }

  componentWillUnmount() {
    document.removeEventListener('mousedown', this.handleOuterClick);
    this.setState({ showPopup: 0 })
  }

  handleOuterClick(event) {
    const container = document.querySelector('.popup');
    if (container && !container.contains(event.target)) {
      
      this.setState({ showPopup: 0 });
      this.setState({
        formValues: {
            correo: '',
            nombre: '',
            apellido1: '',
            apellido2: '',
            contrasena: '',
            pais: '',
            fecha_registro: '',
            fecha_nacimiento: '',
            estatura: '',
            peso: ''
          }, showPopup: 0
      });
    }
  }

 
  

  render() {
    const { showPopup } = this.state;
    return (
      <div className="gestion-productos-container">
        <h1 className="titulo">Inicio de sesión clientes</h1>
        <div className="centered-container">
          <MDBContainer>
            <MDBRow>
            <MDBCol md="7" className="form-container">
                <form onSubmit={(e) => this.handleSubmit(e, 2)}>

                  <label className="form-label">Correo</label>
                  <MDBInput
                    type="text"
                    name="currentemail"
                    value={this.state.currentemail}
                    onChange
                    ={this.handleInputChange}
                  />

                  <label className="form-label">Contraseña</label>
                  <MDBInput
                    type="password"
                    name="currentpass"
                    value={this.state.currentpass}
                    onChange={this.handleInputChange}
                  />
                  <button style={{ color: '#FFF', backgroundColor: '#008CBA', borderRadius: '12px', padding: '12px', border: '2px solid #008CBA' }}>Iniciar sesión</button>
                </form>
                <button style={{ color: '#FFF', backgroundColor: '#008CBA', borderRadius: '12px', padding: '12px', border: '2px solid #008CBA' }} onClick={() => this.setState({ showPopup: 1 })}>Registrarse</button>
              </MDBCol>
              {showPopup === 1 && (
                <div className="popup-container">
                  <div className="popup">
                    <h2>Registrar Nutricionista</h2>
                    <MDBCol>
                      <form onSubmit={this.handleRegistro}>

                        <label className="form-label">Nombre</label>
                        <MDBInput
                          type="text"
                          name="nombre"
                          value={this.state.formValues.Nombre}
                          onChange={this.handleInputChangeR}
                        />

                        <label className="form-label">Primer Apellido</label>
                        <MDBInput
                          type="text"
                          name="apellido1"
                          value={this.state.formValues.Apellido1}
                          onChange={this.handleInputChangeR}
                        />

                        <label className="form-label">Segundo Apellido</label>
                        <MDBInput
                          type="text"
                          name="apellido2"
                          value={this.state.formValues.Apellido2}
                          onChange={this.handleInputChangeR}
                        />

                        <label className="form-label">Correo</label>
                        <MDBInput
                          type="email"
                          name="correo"
                          value={this.state.formValues.Correo}
                          onChange={this.handleInputChangeR}
                        />

                        <label className="form-label">Fecha de Nacimiento</label>
                        <MDBInput
                          type="date"
                          name="fecha_nacimiento"
                          value={this.state.formValues.FechaNacimiento}
                          onChange={this.handleInputChangeR}
                        />


                        <label className="form-label">Contraseña</label>
                        <MDBInput
                          type="password"
                          name="contrasena"
                          value={this.state.formValues.Contrasena}
                          onChange={this.handleInputChangeR}
                        />

                        <label className="form-label">Pais</label>
                        <MDBInput
                          type="text"
                          name="pais"
                          value={this.state.formValues.Direccion}
                          onChange={this.handleInputChangeR}
                        />
                        <label className="form-label">Estatura</label>
                        <MDBInput
                          type="text"
                          name="estatura"
                          value={this.state.formValues.Estatura}
                          onChange={this.handleInputChangeR}
                        />
                        <label className="form-label">Peso</label>
                        <MDBInput
                          type="text"
                          name="peso"
                          value={this.state.formValues.Peso}
                          onChange={this.handleInputChangeR}
                        />
                        <MDBBtn type="submit">Registrarse</MDBBtn>
                      </form>
                    </MDBCol>
                  </div>
                </div>
              )}
            </MDBRow>
          </MDBContainer>
        </div>
      </div>
    );
  }
}

export default LoginCliente;