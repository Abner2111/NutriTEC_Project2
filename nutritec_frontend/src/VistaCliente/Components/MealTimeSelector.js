import React  , {Component} from "react";
import MealTimeSelectorItem from './MealTimeSelectorItem';
import { obtenerTiemposComida } from "../../Api";
class MealTimeSelector extends Component{
    constructor (props){
        super(props);
        this.state = {
            mealtimes : [], //mealtimes from the api
            selectedValue: ''
        }
        this.getMealtimes();
        
        
    }
    createMealTime = ({id, nombre}) => {
        console.log(id);
        console.log(nombre);
        return < MealTimeSelectorItem
                id = {id}
                nombre = {nombre}
                />
    }

    getMealtimes = async() => {
        const data = await obtenerTiemposComida();
        this.setState({ mealtimes : data});
    }
    handleChange = (e) =>{
        this.setState({selectedValue:e.target.value});
        this.props.parentCallback(this.state.selectedValue);
        console.log(this.state.selectedValue)
    }
    render(){
        return(
            <div>
                <select value = {this.state.selectedValue} onChange={this.handleChange}>
                    {this.state.mealtimes.map(this.createMealTime)}
                </select>
            </div>
        )
    }
    
}

export default MealTimeSelector;