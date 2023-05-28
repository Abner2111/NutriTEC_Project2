import React from "react"

// Templates
import { NavbarNutricionista } from "../Templates/NavbarNutricionista"
import MiniCard from "../Templates/MiniCard"

// Icons
import ProductoIcon from '../Imgs/ProductoIcon.png'
import BusquedaAsociacionIcon from "../Imgs/BusquedaAsociacionIcon.png"
import PlanIcon from '../Imgs/PlanIcon.png'
import AsignacionPlanIcon from '../Imgs/AsignacionPlanIcon.png'


const VistaNutricionista = () => {
    return (
      <div>
        <NavbarNutricionista/>
      
        <div className='container-fluid'>
        <div className='row'>

            <div className='col'>
              <MiniCard
                title='Productos/Platillos'
                imageUrl={ProductoIcon}
                body='Gestionar productos'
                url='/gestionproductos'
              />
            </div>

            <div className='col'>
              <MiniCard
                title='Búsqueda y asociación'
                imageUrl={BusquedaAsociacionIcon}
                body='Asociar pacientes'
                url='/busquedaasociacionpacientes'
              />
            </div>

            <div className='col'>
              <MiniCard
                title='Planes'
                imageUrl={PlanIcon}
                body='Gestionar planes'
                url='/gestionplanes'
              />
            </div>

            <div className='col'>
              <MiniCard
                title='Asignación'
                imageUrl={AsignacionPlanIcon}
                body='Asignar plan a pacientes'
                url='/asignacionplanpaciente'
              />
            </div>
            
          </div>
          </div>
      </div>
    )
}

export default VistaNutricionista