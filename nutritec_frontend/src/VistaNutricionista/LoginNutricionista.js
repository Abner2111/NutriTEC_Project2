import React from "react";
import { registrarNutricionista, validarNutricionista } from "../Api";
import '../Templates/diseñoH.css';
import '../Templates/diseño.css';
import { MDBContainer, MDBRow, MDBCol, MDBInput, MDBBtn } from 'mdb-react-ui-kit';


class LoginNutricionista extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      formValues: {
        Cedula: '',
        Foto: '',
        Nombre: '',
        Apellido1: '',
        Apellido2: '',
        Correo: '',
        FechaNacimiento: '',
        TipoCobro: '',
        Codigo: '',
        TarjetaCredito: '',
        Contrasena: '',
        Direccion: '',
        Estatura: '',
        Peso: ''
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
      const data = await registrarNutricionista(obj);
      console.log(data);
    }
    if (tipo === 2) {
      console.log(this.state.currentemail, this.state.currentpass);
      const data = await validarNutricionista(this.state.currentemail, this.state.currentpass);
      console.log(data);
      window.location = "/vistanutricionista"
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
    // Validar número de tarjeta de crédito usando una expresión regular
    if (name === 'TarjetaCredito') {
      const creditCardRegex = /^(?:[0-9]{4}-){3}[0-9]{4}$|^([0-9]{4} ){3}[0-9]{4}$|^[0-9]{16}$/;
      if (!creditCardRegex.test(value)) {
        // Mostrar mensaje de error o realizar alguna acción de validación
        console.log('Número de tarjeta de crédito inválido');
      }
    }
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
    const data = await registrarNutricionista(this.state.formValues);
    console.log(data);
    this.setState({ showPopup: 0 })
  };
  handleImageChange = (event) => {
    const file = event.target.files[0];

    try {
      const imageUrl = URL.createObjectURL(file);

      this.setState((prevState) => ({
        formValues: {
          ...prevState.formValues,
          Foto: imageUrl,
        },
      }));

      console.log(imageUrl);
    } catch (error) {
      console.error('Error al cargar la imagen:', error);
    }
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
          Cedula: '',
          Foto: '',
          Nombre: '',
          Apellido1: '',
          Apellido2: '',
          Correo: '',
          FechaNacimiento: '',
          TipoCobro: '',
          Codigo: '',
          TarjetaCredito: '',
          Contrasena: '',
          Direccion: '',
          Estatura: '',
          Peso: ''
        }, showPopup: 0
      });
    }
  }

  render() {
    const { showPopup } = this.state;
    return (
      <div className="gestion-productos-container">
        <h1 className="titulo">Inicio de sesion Nutricionista</h1>
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
-                <button style={{ color: '#FFF', backgroundColor: '#008CBA', borderRadius: '12px', padding: '12px', border: '2px solid #008CBA' }} onClick={() => this.setState({ showPopup: 1 })}>Registrarse</button>
              </MDBCol>
              {showPopup === 1 && (
                <div className="popup-container">
                  <div className="popup">
                    <h2>Registrar Nutricionista</h2>
                    <MDBCol>
                      <form onSubmit={this.handleRegistro}>
                        <label className="form-label">Cédula</label>
                        <MDBInput
                          type="text"
                          name="Cedula"
                          value={this.state.formValues.Cedula}
                          onChange={this.handleInputChangeR}
                        />

                        <label className="form-label">Foto</label>
                        <MDBInput
                          type="file"
                          name="Foto"
                          accept="image/*"
                          onChange={this.handleImageChange}
                        />

                        <label className="form-label">Nombre</label>
                        <MDBInput
                          type="text"
                          name="Nombre"
                          value={this.state.formValues.Nombre}
                          onChange={this.handleInputChangeR}
                        />

                        <label className="form-label">Prinmer Apellido</label>
                        <MDBInput
                          type="text"
                          name="Apellido1"
                          value={this.state.formValues.Apellido1}
                          onChange={this.handleInputChangeR}
                        />

                        <label className="form-label">Segundo Apellido</label>
                        <MDBInput
                          type="text"
                          name="Apellido2"
                          value={this.state.formValues.Apellido2}
                          onChange={this.handleInputChangeR}
                        />

                        <label className="form-label">Correo</label>
                        <MDBInput
                          type="email"
                          name="Correo"
                          value={this.state.formValues.Correo}
                          onChange={this.handleInputChangeR}
                        />

                        <label className="form-label">Fecha de Nacimiento</label>
                        <MDBInput
                          type="date"
                          name="FechaNacimiento"
                          value={this.state.formValues.FechaNacimiento}
                          onChange={this.handleInputChangeR}
                        />

                        <label className="form-label">Tipo de Cobro</label>
                        <MDBInput
                          type="text"
                          name="TipoCobro"
                          value={this.state.formValues.TipoCobro}
                          onChange={this.handleInputChangeR}
                        />

                        <label className="form-label">Codigo</label>
                        <MDBInput
                          type="text"
                          name="Codigo"
                          value={this.state.formValues.Codigo}
                          onChange={this.handleInputChangeR}
                        />

                        <label className="form-label">Tarjeta de Credito</label>
                        <MDBInput
                          type="text"
                          name="TarjetaCredito"
                          value={this.state.formValues.TarjetaCredito}
                          onChange={this.handleInputChangeR}
                        />

                        <label className="form-label">Contraseña</label>
                        <MDBInput
                          type="password"
                          name="Contrasena"
                          value={this.state.formValues.Contrasena}
                          onChange={this.handleInputChangeR}
                        />

                        <label className="form-label">Direccion</label>
                        <MDBInput
                          type="text"
                          name="Direccion"
                          value={this.state.formValues.Direccion}
                          onChange={this.handleInputChangeR}
                        />
                        <label className="form-label">Estatura</label>
                        <MDBInput
                          type="text"
                          name="Estatura"
                          value={this.state.formValues.Estatura}
                          onChange={this.handleInputChangeR}
                        />
                        <label className="form-label">Peso</label>
                        <MDBInput
                          type="text"
                          name="Peso"
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

export default LoginNutricionista;