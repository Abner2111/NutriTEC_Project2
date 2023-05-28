import './App.css';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'
import React from "react";

import "../node_modules/bootstrap/dist/css/bootstrap.min.css";

// Administrador
import VistaPrincipal from './VistaPrincipal';  

// Nutricionista
/* UC */

// Cliente
/* UC */

function App() {
  return (
    <div className="App">
      <Router>
          <Routes>
            <Route path='/' element={ <VistaPrincipal/> }/>

            
          </Routes>
      </Router>
      
    </div>
  );
}

export default App;