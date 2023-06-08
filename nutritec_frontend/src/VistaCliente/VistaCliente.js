import React from "react"

// Templates
import { NavbarCliente } from "../Templates/NavbarCliente"
import MiniCard from "../Templates/MiniCard"

// Icons
import MedidasIcon from "../Imgs/MeasurementsIcon.png"
import MealIcon from '../Imgs/MealIcon.png'


const VistaCliente = () => {
    return (
      <div>
        <NavbarCliente/>
      
        <div className='container-fluid'>
        <div className='row'>

            <div className='col'>
              <MiniCard
                title='Medidas'
                imageUrl={MedidasIcon}
                body='Registrar Nueva Medida'
                url='/nuevomedidas'
              />
            </div>

            <div className='col'>
              <MiniCard
                title='Consumo'
                imageUrl={MealIcon}
                body='Registrar Nuevo Consumo'
                url='/nuevoconsumo'
              />
            </div>
            
          </div>
          </div>
      </div>
    )
}

export default VistaCliente