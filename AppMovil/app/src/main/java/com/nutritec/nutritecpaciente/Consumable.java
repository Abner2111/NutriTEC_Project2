package com.nutritec.nutritecpaciente;

public class Consumable {
    String calories;
    String nombre;
    String barcode;

    public Consumable( String nombre,String calories, String barcode) {
        this.calories = calories;
        this.nombre = nombre;
        this.barcode = barcode;
    }

    public String getBarcode() {
        return barcode;
    }

    public String getCalories() {
        return calories;
    }

    public String getNombre() {
        return nombre;
    }

}
