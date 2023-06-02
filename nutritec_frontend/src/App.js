import './App.css';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'
import React from "react";

import "../node_modules/bootstrap/dist/css/bootstrap.min.css";

// Administrador
import VistaPrincipal from './VistaPrincipal';

// Nutricionista
import LoginNutricionista from './VistaNutricionista/LoginNutricionista';
import GestionProductos from './VistaNutricionista/GestionProductos';

/* UC */

// Cliente
/* UC */

function App() {
  return (
    <div className="App">
      <Router>
          <Routes>
            <Route path='/1' element={ <VistaPrincipal/> }/>
            <Route path='/2' element={<LoginNutricionista/>}/>
            <Route path='/' element={<GestionProductos/>}/>


            
          </Routes>
      </Router>
      
    </div>
  );
}

export default App;