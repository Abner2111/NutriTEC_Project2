package com.nutritec.nutritecpaciente.connection;

public class ConsumoProducto {
    private String inpcorreo;
    private String inpfecha;
    private int inpttiempocomidaid;
    private int inptproductoid;

    public ConsumoProducto(String inpcorreo, String inpfecha, int inpttiempocomidaid, int inptproductoid) {
        this.inpcorreo = inpcorreo;
        this.inpfecha = inpfecha;
        this.inpttiempocomidaid = inpttiempocomidaid;
        this.inptproductoid = inptproductoid;
    }

    public String getInpcorreo() {
        return inpcorreo;
    }

    public String getInpfecha() {
        return inpfecha;
    }

    public int getInpttiempocomidaid() {
        return inpttiempocomidaid;
    }

    public int getInptproductoid() {
        return inptproductoid;
    }
}
