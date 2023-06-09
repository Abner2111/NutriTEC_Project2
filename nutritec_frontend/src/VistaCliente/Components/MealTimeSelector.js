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
    /* `createMealTime` is a function that takes an object with `id` and `nombre` properties as its
    argument. It then returns a `MealTimeSelectorItem` component with `id` and `nombre` props set to
    the values of the corresponding properties in the argument object. This function is used in the
    `render` method of the `MealTimeSelector` component to map over the `mealtimes` array in the
    component's state and create a `MealTimeSelectorItem` component for each item in the array. */
    createMealTime = ({id, nombre}) => {
        return < MealTimeSelectorItem
                id = {id}
                nombre = {nombre}
                />
    }

    /* `getMealtimes` is an asynchronous function that uses the `await` keyword to wait for the
    `obtenerTiemposComida` function to return data from an API call. Once the data is returned, it
    is stored in the component's state using the `setState` method, with the `mealtimes` property
    set to the returned data. This function is called in the constructor of the `MealTimeSelector`
    component to initialize the `mealtimes` state property with data from the API. */
    getMealtimes = async() => {
        const data = await obtenerTiemposComida();
        this.setState({ mealtimes : data});
        setTimeout(()=>{
            this.selectedValue = data[0].id;
            
        },1000);
        setTimeout(()=>{
            
            this.props.parentCallback(this.selectedValue);
        },1000);
    }

    /* `handleChange` is a function that is called when the value of the `select` element in the
    `render` method of the `MealTimeSelector` component changes. It takes an event object `e` as its
    argument. */
    handleChange = (e) =>{
        this.setState({selectedValue:e.target.value});
        this.props.parentCallback(e.target.value);
        e.preventDefault();
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