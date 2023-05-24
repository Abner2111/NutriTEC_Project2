package com.nutritec.nutritecpaciente;

public class Consumable {
    String calories;
    String nombre;
    String barcode;

    String identifier;

    public Consumable( String nombre,String calories, String barcode) {
        this.calories = calories;
        this.nombre = nombre;
        this.barcode = barcode;
        this.identifier = "";
    }


    public Consumable() {
        this.calories = "";
        this.nombre="";
        this.barcode="";
        this.identifier="";
    }

    public void setCalories(String calories) {
        this.calories = calories;
    }

    public void setNombre(String nombre) {
        this.nombre = nombre;
    }

    public String getIdentifier() {
        return identifier;
    }

    public void setIdentifier(String identifier) {
        this.identifier = identifier;
    }

    public void setBarcode(String barcode) {
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
