package com.nutritec.nutritecpaciente.connection;

public class ProductoReceta {
    private String receta_name;
    private int producto_id;

    public ProductoReceta(String receta_name, int producto_id) {
        this.receta_name = receta_name;
        this.producto_id = producto_id;
    }

    public String getReceta_name() {
        return receta_name;
    }

    public int getProducto_id() {
        return producto_id;
    }
}
