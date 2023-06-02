import React  , {Component} from "react";
import MealTimeSelectorItem from './MealTimeSelectorItem';
import { obtenerTiemposComida } from "../../Api";
class MealTimeSelector extends Component{
    constructor (props){
        super(props);
        this.state = {
            mealtimes : [] //mealtimes from the api
        }
        this.getMealtimes();
        
        
    }
    createMealTime = ({id, nombre}) => {
        return < MealTimeSelectorItem
                id = {id}
                nombre = {nombre}
                />
    }

    getMealtimes = async() => {
        const data = await obtenerTiemposComida();
        this.setState({ mealtimes : data});
    }

    render(){
        return(
            <div>
                <select>
                    {this.state.mealtimes.map(this.createMealTime)}
                </select>
            </div>
        )
    }
    
}

export default MealTimeSelector;