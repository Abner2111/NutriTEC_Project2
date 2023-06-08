package com.nutritec.nutritecpaciente.connection;

public class Producto {
    private String nombre;
    private String codigo_barras;
    private String tamano_porcion;
    private boolean aprobado;
    private int grasa;
    private int energia;
    private int proteina;
    private int sodio;
    private int carbohidratos;
    private int hierro;
    private int vitaminad;
    private int vitaminab6;
    private int vitaminac;
    private int vitaminak;
    private int vitaminab;
    private int vitaminab12;
    private int vitaminaa;
    private int calcio;

    public Producto(String nombre, String codigo_barras, String tamano_porcion, boolean aprobado, int grasa, int energia, int proteina, int sodio, int carbohidratos, int hierro, int vitaminad, int vitaminab6, int vitaminac, int vitaminak, int vitaminab, int vitaminab12, int vitaminaa, int calcio) {
        this.nombre = nombre;
        this.codigo_barras = codigo_barras;
        this.tamano_porcion = tamano_porcion;
        this.aprobado = aprobado;
        this.grasa = grasa;
        this.energia = energia;
        this.proteina = proteina;
        this.sodio = sodio;
        this.carbohidratos = carbohidratos;
        this.hierro = hierro;
        this.vitaminad = vitaminad;
        this.vitaminab6 = vitaminab6;
        this.vitaminac = vitaminac;
        this.vitaminak = vitaminak;
        this.vitaminab = vitaminab;
        this.vitaminab12 = vitaminab12;
        this.vitaminaa = vitaminaa;
        this.calcio = calcio;
    }

    public Producto() {
    }

    public String getNombre() {
        return nombre;
    }

    public void setNombre(String nombre) {
        this.nombre = nombre;
    }

    public String getCodigo_barras() {
        return codigo_barras;
    }

    public void setCodigo_barras(String codigo_barras) {
        this.codigo_barras = codigo_barras;
    }

    public String getTamano_porcion() {
        return tamano_porcion;
    }

    public void setTamano_porcion(String tamano_porcion) {
        this.tamano_porcion = tamano_porcion;
    }

    public boolean isAprobado() {
        return aprobado;
    }

    public void setAprobado(boolean aprobado) {
        this.aprobado = aprobado;
    }

    public int getGrasa() {
        return grasa;
    }

    public void setGrasa(int grasa) {
        this.grasa = grasa;
    }

    public int getEnergia() {
        return energia;
    }

    public void setEnergia(int energia) {
        this.energia = energia;
    }

    public int getProteina() {
        return proteina;
    }

    public void setProteina(int proteina) {
        this.proteina = proteina;
    }

    public int getSodio() {
        return sodio;
    }

    public void setSodio(int sodio) {
        this.sodio = sodio;
    }

    public int getCarbohidratos() {
        return carbohidratos;
    }

    public void setCarbohidratos(int carbohidratos) {
        this.carbohidratos = carbohidratos;
    }

    public int getHierro() {
        return hierro;
    }

    public void setHierro(int hierro) {
        this.hierro = hierro;
    }

    public int getVitaminad() {
        return vitaminad;
    }

    public void setVitaminad(int vitaminad) {
        this.vitaminad = vitaminad;
    }

    public int getVitaminab6() {
        return vitaminab6;
    }

    public void setVitaminab6(int vitaminab6) {
        this.vitaminab6 = vitaminab6;
    }

    public int getVitaminac() {
        return vitaminac;
    }

    public void setVitaminac(int vitaminac) {
        this.vitaminac = vitaminac;
    }

    public int getVitaminak() {
        return vitaminak;
    }

    public void setVitaminak(int vitaminak) {
        this.vitaminak = vitaminak;
    }

    public int getVitaminab() {
        return vitaminab;
    }

    public void setVitaminab(int vitaminab) {
        this.vitaminab = vitaminab;
    }

    public int getVitaminab12() {
        return vitaminab12;
    }

    public void setVitaminab12(int vitaminab12) {
        this.vitaminab12 = vitaminab12;
    }

    public int getVitaminaa() {
        return vitaminaa;
    }

    public void setVitaminaa(int vitaminaa) {
        this.vitaminaa = vitaminaa;
    }

    public int getCalcio() {
        return calcio;
    }

    public void setCalcio(int calcio) {
        this.calcio = calcio;
    }
}
