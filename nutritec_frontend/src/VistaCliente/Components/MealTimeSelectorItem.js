import React from "react";

const MealTimeSelectorItem = ({id, nombre}) =>{
    let idText = {id}.toString();
    return (
    <option value = {idText}>{nombre}</option>
    );
}

export default MealTimeSelectorItem;