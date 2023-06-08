import React from "react";
import ConsumableList from "./Components/ConsumableList";
import { NavbarCliente } from "../Templates/NavbarCliente";
import { ClientContext } from "./LoginCliente";
const NuevoConsumoCliente = () =>{
    return (
        <div>
            <NavbarCliente/>
            <header className="header">
                <h1>Registra tu consumo</h1>
            </header>
            
            <ConsumableList/>
        </div>
    )
}
export default NuevoConsumoCliente;