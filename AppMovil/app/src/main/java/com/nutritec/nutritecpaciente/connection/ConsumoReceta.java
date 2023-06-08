package com.nutritec.nutritecpaciente.connection;

public class ConsumoReceta {
    private String inptcorreo;
    private String inptfecha;
    private int inpttiempocomidaid;
    private String inptrecetaname;

    public ConsumoReceta(String inptcorreo, String inptfecha, int inpttiempocomidaid, String inptrecetaname) {
        this.inptcorreo = inptcorreo;
        this.inptfecha = inptfecha;
        this.inpttiempocomidaid = inpttiempocomidaid;
        this.inptrecetaname = inptrecetaname;
    }

    public String getInptcorreo() {
        return inptcorreo;
    }

    public String getInptfecha() {
        return inptfecha;
    }

    public int getInpttiempocomidaid() {
        return inpttiempocomidaid;
    }

    public String getInptrecetaname() {
        return inptrecetaname;
    }
}
