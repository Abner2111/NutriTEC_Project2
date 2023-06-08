package com.nutritec.nutritecpaciente.connection;

public class Medida {
    private String fecha;
    private String medidacintura;
    private String porcentajegrasa;
    private String porcentajemusculo;
    private String medidacadera;
    private String medidacuello;
    private String correocliente;

    public Medida(String fecha, String medidacintura, String porcentajegrasa, String porcentajemusculo, String medidacadera, String medidacuello, String correocliente) {
        this.fecha = fecha;
        this.medidacintura = medidacintura;
        this.porcentajegrasa = porcentajegrasa;
        this.porcentajemusculo = porcentajemusculo;
        this.medidacadera = medidacadera;
        this.medidacuello = medidacuello;
        this.correocliente = correocliente;
    }

    public String getFecha() {
        return fecha;
    }

    public String getMedidacintura() {
        return medidacintura;
    }

    public String getPorcentajegrasa() {
        return porcentajegrasa;
    }

    public String getPorcentajemusculo() {
        return porcentajemusculo;
    }

    public String getMedidacadera() {
        return medidacadera;
    }

    public String getMedidacuello() {
        return medidacuello;
    }

    public String getCorreocliente() {
        return correocliente;
    }
}
