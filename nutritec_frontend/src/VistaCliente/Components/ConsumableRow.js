import React from 'react';

const ConsumableRow = ({id, nombre, codigoBarras, childToParent}) => {
    return (
        <div className = "row consumable">
            <div className = "col-md-1 consumable-add">
                <p>{id}</p>
            </div>
            <div className= "col-md-8 consumable-detail">
                <h4>{nombre}</h4>
                <p>{codigoBarras}</p>
            </div>
            <div className = "col-md-2 consumable-add">
                <button style={{ color: '#FFF', backgroundColor: '#039487', borderRadius: '12px', padding: '5px', border: '2px solid #008CBA' }} primary onClick={() => childToParent({id, nombre, codigoBarras})}>Agregar</button>
            </div>
        </div>
    );
}

export default ConsumableRow;