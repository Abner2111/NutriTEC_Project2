import React, { Component } from "react";
import { NavbarCliente } from "../Templates/NavbarCliente";
import { agregarMedidas } from "../Api";
import { MDBContainer, MDBRow,  MDBInput, MDBBtn } from 'mdb-react-ui-kit';
		
import { ToastContainer } from "react-toastify";
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
class NuevoMedidas extends Component{
    constructor(props){
        super(props);
        this.state = {
            formValues: {
              fecha: '',
              medidacintura: '0',
              porcentajegrasa: '0',
              porcentajemusculo: '0',
              medidacadera: '0',
              medidacuello: '0',
              correocliente: ''
            }
        }
    }


    /* `handleRegistro` is a function that is called when the user submits the form to register new
    measurements. It first prevents the default form submission behavior, then gets the current date
    and formats it as a string. It then sets the `fecha` and `correocliente` properties of the
    `formValues` object in the component's state to the current date and the user's email address
    (retrieved from local storage), respectively. It then calls the `agregarMedidas` function (which
    is an API call to add the new measurements to a database) with the `formValues` object as a
    parameter, and waits for the response. If the API call is successful, it displays a success
    message using the `toast` function from the `react-toastify` library, and resets the
    `formValues` object in the component's state to its initial values. */
    handleRegistro = async (event) => {
        event.preventDefault();
        const date = new Date();
        let currentDay= String(date.getDate()).padStart(2, '0');

        let currentMonth = String(date.getMonth()+1).padStart(2,"0");
        
        let currentYear = date.getFullYear();

        let currentDate = `${currentYear}-${currentMonth}-${currentDay}`;
        this.state.formValues.fecha = currentDate;
        this.state.formValues.correocliente = localStorage.getItem('userEmail');
        console.log(this.state.formValues);
        const data = await agregarMedidas(this.state.formValues);
        toast.success("Medidas actualizadas");
        console.log(data);
        this.setState({formValues: {
            fecha: '',
            medidacintura: 0,
            porcentajegrasa: 0,
            porcentajemusculo: 0,
            medidacadera: 0,
            medidacuello: 0,
            correocliente: 0
          }}
            
        )
        
    }
    /* `handleInputChange` is a function that is called whenever there is a change in any of the input
    fields in the form. It takes an event object as a parameter, which contains information about
    the input field that triggered the change event. */
    handleInputChange = (event) => {
        const { name, value } = event.target;
        if (value === ''){
            this.setState((prevState) => ({
                formValues: {
                  ...prevState.formValues,
                  [name]: 0
                }
              }));
        } else if ((name === 'porcentajegrasa' || name === 'porcentajemusculo') && (value<0 || value >100)){
            toast.error("Valor de porcentaje debe estar entre 0 y 100");
            this.setState((prevState) => ({
                formValues: {
                  ...prevState.formValues,
                  [name]: 0
                }
              }));
        } else if ((name === 'medidacuello' || name === 'medidacadera'|| name === 'medidacintura') && (value<=0)){
            toast.error("Valor de medida debe ser mayor a 0");
            this.setState((prevState) => ({
                formValues: {
                  ...prevState.formValues,
                  [name]: 0
                }
              }));
        } else {
            this.setState((prevState) => ({
                formValues: {
                  ...prevState.formValues,
                  [name]: value
                }
              }));
        }
        
      };

    render() {
        if ('userEmail' in localStorage && localStorage.getItem('userEmail') !== ''){

            return ( 
                <div>
                    <ToastContainer
                    position="top-left"
                    autoClose={5000}
                    hideProgressBar={false}
                    newestOnTop={false}
                    closeOnClick
                    rtl={false}
                    pauseOnFocusLoss
                    draggable
                    pauseOnHover
                    theme="light"/>
                    <NavbarCliente/>
                    <h1 className="titulo">Registro de Medidas</h1>
                    <MDBContainer>
                        <MDBRow>
                            <form onSubmit={this.handleRegistro}>
                                <label className="form-label">Medida de cintura (cm)</label>
                                <MDBInput 
                                    type="number"
                                    name="medidacintura"
                                    value={this.state.formValues.medidacintura}
                                    onChange={this.handleInputChange}
                                />
                                <label className="form-label">Porcentaje de grasa (%)</label>
                                <MDBInput 
                                    type="number"
                                    name="porcentajegrasa"
                                    value={this.state.formValues.porcentajegrasa}
                                    onChange={this.handleInputChange}
                                />
                                <label className="form-label">Porcentaje de músculo (%)</label>
                                <MDBInput 
                                    type="number"
                                    name="porcentajemusculo"
                                    value={this.state.formValues.porcentajemusculo}
                                    onChange={this.handleInputChange}
                                />
                                <label className="form-label">Medida de cadera (cm)</label>
                                <MDBInput 
                                    type="number"
                                    name="medidacadera"
                                    value={this.state.formValues.medidacadera}
                                    onChange={this.handleInputChange}
                                />
                                <label className="form-label">Medida de cuello (cm)</label>
                                <MDBInput 
                                    type="number"
                                    name="medidacuello"
                                    value={this.state.formValues.medidacuello}
                                    onChange={this.handleInputChange}
                                />
                                <MDBBtn type="submit">Registrar</MDBBtn>
                            </form>
                        </MDBRow>
                    </MDBContainer>
                </div>
            ) } else {
                return(
                    
                    <div>
                        <NavbarCliente/>
                        <h1 className="titulo">Registro de Medidas</h1>
                        <h>NO SE HA INGRESADO UN USUARIO</h>
                    </div>
                )
            }
    }
}
export default NuevoMedidas;