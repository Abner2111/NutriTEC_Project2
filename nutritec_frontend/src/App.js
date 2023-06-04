import './App.css';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'
import React from "react";

import "../node_modules/bootstrap/dist/css/bootstrap.min.css";


import VistaPrincipal from './VistaPrincipal';

// Administrador
import LoginAdministrador from './VistaAdministrador/LoginAdministrador';
import VistaAdministrador from './VistaAdministrador/VistaAdministrador';
import ReporteCobro from './VistaAdministrador/ReporteCobro';

// Nutricionista
import LoginNutricionista from './VistaNutricionista/LoginNutricionista';
import GestionProductos from './VistaNutricionista/GestionProductos';
import VistaNutricionista from './VistaNutricionista/VistaNutricionista'
import BusquedaAsociacionPacientes from './VistaNutricionista/BusquedaAsociacionPacientes';
import GestionPlanes from './VistaNutricionista/GestionPlanes';
import SeguimientoPaciente from './VistaNutricionista/SeguimientoPaciente';

// Cliente
/* UC */

function App() {
  return (
    <div className="App">
      <Router>
          <Routes>
            <Route path='/' element = { <VistaPrincipal/> }/>

            <Route path='/vistaadministrador' element = { <VistaAdministrador/> } />
            <Route path='/reportecobro' element = { <ReporteCobro/> } />
            <Route path='/loginadministrador' element = { <LoginAdministrador/> } />

            <Route path='/vistanutricionista' element = { <VistaNutricionista/> }/>
            <Route path='/busquedaasociacionpacientes' element = { <BusquedaAsociacionPacientes/> }/>
            <Route path='/gestionplanes' element = { <GestionPlanes/> }/>
            <Route path='/seguimientopaciente' element = { <SeguimientoPaciente/> }/>
            
            <Route path= '/loginnutricionista' element={<LoginNutricionista/>}/>
            <Route path= '/gestionproductos' element={<GestionProductos/>}/>
            

            

            
          </Routes>
      </Router>
      
    </div>
  );
}

export default App;