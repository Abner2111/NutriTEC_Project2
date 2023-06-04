import React, { Component } from 'react';
import { NavbarNutricionista } from '../Templates/NavbarNutricionista';


class SeguimientoPaciente extends Component {
  constructor(props) {
    super(props);
  
    this.state = {
      
    };
  }

  // función que renderiza el componente
render() {
  const { error } = this.state;

  return (
    <div style={{ backgroundColor: '#fff', textAlign: 'center' }}>
        <NavbarNutricionista/>
  <h1 style={{ margin: '50px 0', fontSize: '2.5rem', fontWeight: 'bold', textTransform: 'uppercase' }}>Seguimiento paciente</h1>
      {error && <div>Error: {error}</div>}
      
      

      </div>
    );
  }
}

export default SeguimientoPaciente;