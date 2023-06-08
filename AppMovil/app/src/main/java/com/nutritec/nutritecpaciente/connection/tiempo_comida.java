package com.nutritec.nutritecpaciente.connection;

public class tiempo_comida {
    private int id;
    private String nombre;

    public tiempo_comida(int id, String nombre) {
        this.id = id;
        this.nombre = nombre;
    }

    public int getId() {
        return id;
    }

    public String getNombre() {
        return nombre;
    }
}
