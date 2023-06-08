package com.nutritec.nutritecpaciente.connection;

public class Cliente {
    private String correo;
    private String nombre;
    private String apellido1;
    private String apellido2;
    private String contrasena;
    private String pais;
    private String fecha_registro;
    private String fecha_nacimiento;
    private int estatura;
    private int peso;

    public Cliente() {
    }

    public Cliente(String correo, String nombre, String apellido1, String apellido2, String contrasena, String pais, String fecha_registro, String fecha_nacimiento, int estatura, int peso) {
        this.correo = correo;
        this.nombre = nombre;
        this.apellido1 = apellido1;
        this.apellido2 = apellido2;
        this.contrasena = contrasena;
        this.pais = pais;
        this.fecha_registro = fecha_registro;
        this.fecha_nacimiento = fecha_nacimiento;
        this.estatura = estatura;
        this.peso = peso;
    }

    public String getCorreo() {
        return correo;
    }

    public void setCorreo(String correo) {
        this.correo = correo;
    }

    public String getNombre() {
        return nombre;
    }

    public void setNombre(String nombre) {
        this.nombre = nombre;
    }

    public String getApellido1() {
        return apellido1;
    }

    public void setApellido1(String apellido1) {
        this.apellido1 = apellido1;
    }

    public String getApellido2() {
        return apellido2;
    }

    public void setApellido2(String apellido2) {
        this.apellido2 = apellido2;
    }

    public String getContrasena() {
        return contrasena;
    }

    public void setContrasena(String contrasena) {
        this.contrasena = contrasena;
    }

    public String getPais() {
        return pais;
    }

    public void setPais(String pais) {
        this.pais = pais;
    }

    public String getFecha_registro() {
        return fecha_registro;
    }

    public void setFecha_registro(String fecha_registro) {
        this.fecha_registro = fecha_registro;
    }

    public String getFecha_nacimiento() {
        return fecha_nacimiento;
    }

    public void setFecha_nacimiento(String fecha_nacimiento) {
        this.fecha_nacimiento = fecha_nacimiento;
    }

    public int getEstatura() {
        return estatura;
    }

    public void setEstatura(int estatura) {
        this.estatura = estatura;
    }

    public int getPeso() {
        return peso;
    }

    public void setPeso(int peso) {
        this.peso = peso;
    }
}

