package com.nutritec.nutritecpaciente.connection;

public class ConsumoProducto {
    private final String inptcorreo;
    private final String inptfecha;
    private final int inpttiempocomidaid;
    private final int inptproductoid;


    public ConsumoProducto(String inptcorreo, String inptfecha, int inpttiempocomidaid, int inptproductoid) {
        this.inptcorreo = inptcorreo;
        this.inptfecha = inptfecha;
        this.inpttiempocomidaid = inpttiempocomidaid;
        this.inptproductoid = inptproductoid;
    }

    public String getInpcorreo() {
        return inptcorreo;
    }

    public String getInpfecha() {
        return inptfecha;
    }

    public int getInpttiempocomidaid() {
        return inpttiempocomidaid;
    }

    public int getInptproductoid() {
        return inptproductoid;
    }
}
