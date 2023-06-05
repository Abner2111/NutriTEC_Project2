import React from "react"

// Templates
import { NavbarAdministrador } from "../Templates/NavbarAdministrador"
import MiniCard from "../Templates/MiniCard"

// Icons
import AprobacionProductoIcon from '../Imgs/AprobacionProductoIcon.png'
import ReporteCobroIcon from '../Imgs/ReporteCobroIcon.png'


const VistaAdministrador = () => {
    return (
      <div>
        <NavbarAdministrador/>
      
        <div className='container-fluid'>
            <div className='row'>

                <div className='col'></div>

                <div className='col'>
                <MiniCard
                    title='Aprobación de productos'
                    imageUrl={AprobacionProductoIcon}
                    body='Aprobar productos'
                    url='/aprobacionproductos'
                />
                </div>

                <div className='col'>
                <MiniCard
                    title='Reportes de cobro'
                    imageUrl={ReporteCobroIcon}
                    body='Gestionar cobro'
                    url='/reportecobro'
                />
                </div>

                <div className='col'></div>
            
            </div>
          </div>
      </div>
    )
}

export default VistaAdministrador