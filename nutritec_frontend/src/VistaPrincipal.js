import React from "react"
import Card from './Templates/Card'

import AdminIcon from './Imgs/AdminIcon.png'
import ClientIcon from './Imgs/ClientIcon.png'
import NutricionistIcon from './Imgs/NutricionistIcon.png'

const VistaPrincipal = () => {
    return (
        <div className='container-fluid'>
            <div className='row'>

                <div className='col'></div>

                <div className='col'>
                    <Card 
                        title='Administrador'
                        imageUrl={AdminIcon}
                        body='Entrar como administrador'
                        url='/loginadmin'
                    />
                </div>

                <div className='col'>
                    <Card 
                        title='Cliente'
                        imageUrl={ClientIcon}
                        body='Entrar como cliente'
                        url='/logincliente'
                    />
                </div>

                <div className='col'>
                    <Card 
                        title='Nutricionista'
                        imageUrl={NutricionistIcon}
                        body='Entrar como nutricionista'
                        url='/loginnutricionist'
                    />
                </div>

                <div className='col'></div>

            </div>
        </div>
    )
}

export default VistaPrincipal