import React from "react";

const MealTimeSelectorItem = ({id, nombre}) =>{
    
    return (
    <option value = {id}>{nombre}</option>
    );
}

export default MealTimeSelectorItem;