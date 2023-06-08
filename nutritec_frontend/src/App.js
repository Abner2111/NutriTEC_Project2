import './App.css';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'
import React from "react";

import "../node_modules/bootstrap/dist/css/bootstrap.min.css";


import VistaPrincipal from './VistaPrincipal';

// Administrador
import LoginAdministrador from './VistaAdministrador/LoginAdministrador';
import VistaAdministrador from './VistaAdministrador/VistaAdministrador';
import ReporteCobro from './VistaAdministrador/ReporteCobro';
import AprobarProductos from './VistaAdministrador/AprobarProductos';


// Nutricionista
import LoginNutricionista from './VistaNutricionista/LoginNutricionista';
import VistaNutricionista from './VistaNutricionista/VistaNutricionista'
import GestionProductos from './VistaNutricionista/GestionProductos';
import BusquedaAsociacionPacientes from './VistaNutricionista/BusquedaAsociacionPacientes';
import GestionPlanes from './VistaNutricionista/GestionPlanes';
import AsignacionPlanPaciente from './VistaNutricionista/AsignacionPlanPaciente';
import SeguimientoPaciente from './VistaNutricionista/SeguimientoPaciente';

// Cliente
import VistaCliente  from './VistaCliente/VistaCliente';
import NuevoConsumoCliente from './VistaCliente/NuevoConsumoCliente';
import LoginCliente from './VistaCliente/LoginCliente';
import NuevoMedidas from './VistaCliente/NuevoMedidas';
import VisualizacionPlanes from './VistaCliente/VisualizacionPlanes';
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

            <Route path='/loginnutricionista' element={<LoginNutricionista/>}/>
            <Route path='/vistanutricionista' element = { <VistaNutricionista/> }/>
            <Route path='/gestionproductos' element={<GestionProductos/>}/>
            <Route path='/busquedaasociacionpacientes' element = { <BusquedaAsociacionPacientes/> }/>
            <Route path='/gestionplanes' element = { <GestionPlanes/> }/>
            <Route path='/asignacionplanpaciente' element = { <AsignacionPlanPaciente/> }/>
            <Route path='/seguimientopaciente' element = { <SeguimientoPaciente/> }/>
            
            
            <Route path='/vistacliente' element = { <VistaCliente/> }/>
            <Route path='/nuevoconsumo' element = {<NuevoConsumoCliente/>}/>
            <Route path='/logincliente' element = { <LoginCliente/> }/>
            <Route path='/nuevoMedidas' element = { <NuevoMedidas/> }/>
            <Route path='/planescliente' element = { <VisualizacionPlanes/> }/>
            <Route path='/aprobacionproductos' element = { <AprobarProductos/> }/>

          </Routes> 
      </Router>
      
    </div>
  );
}

export default App;