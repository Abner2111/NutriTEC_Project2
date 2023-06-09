package com.nutritec.nutritecpaciente.connection;

public class ProductoReceta {
    private String receta_name;
    private String producto;

    public ProductoReceta(String receta_name, String producto) {
        this.receta_name = receta_name;
        this.producto = producto;
    }

    public String getReceta_name() {
        return receta_name;
    }

    public String getProducto() {
        return producto;
    }
}
