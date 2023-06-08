import React from "react";
import { validarAdministrador } from "../Api";
import '../Templates/diseñoH.css';
import '../Templates/diseño.css';
import { MDBContainer, MDBRow, MDBCol, MDBInput } from 'mdb-react-ui-kit';


class LoginAdministrador extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      currentemail: '',
      currentpass: '',
      showPopup: 0
    };
    this.handleOuterClick = this.handleOuterClick.bind(this);
  }

  handleSubmit = async (event) => {
    event.preventDefault();
      console.log(this.state.currentemail, this.state.currentpass);
      const data = await validarAdministrador(this.state.currentemail, this.state.currentpass);
      console.log(data);
      window.location = "/vistaadministrador"
  };
  
  handleInputChange = (event) => {
    const { name, value } = event.target;
    this.setState({
      [name]: value
    });
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
    }
  }

  render() {
    return (
      <div className="gestion-productos-container">
        <h1 className="titulo">Inicio de sesion administrador</h1>
        <div className="centered-container">
          <MDBContainer>
            <MDBRow>
            <MDBCol md="7" className="form-container">
                <form onSubmit={(e) => this.handleSubmit(e)}>

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
              </MDBCol>
              
            </MDBRow>
          </MDBContainer>
        </div>
      </div>
    );
  }
}

export default LoginAdministrador;