import React from "react";
import ConsumableList from "./Components/ConsumableList";
import { NavbarCliente } from "../Templates/NavbarCliente";

const NuevoConsumoCliente = () =>{
    
    return (
        
        <div className = "container">
            <NavbarCliente/>
            <header className="header">
                <h1>Registra tu consumo</h1>
            </header>
            
            <ConsumableList/>
        </div>
    )
}
export default NuevoConsumoCliente;