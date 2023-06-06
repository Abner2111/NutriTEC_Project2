import React from 'react';

const SelectedConsumableRow = ({id, nombre, codigoBarras,childToParent}) => {
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
                <button style={{ color: '#FFF', backgroundColor: '#FF0000', borderRadius: '12px', padding: '5px', border: '2px solid #008CBA' }} primary onClick={() => childToParent({id, nombre, codigoBarras})}>Eliminar</button>
            </div>
        </div>
    );
}

export default SelectedConsumableRow;