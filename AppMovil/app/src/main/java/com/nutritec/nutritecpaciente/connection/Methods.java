package com.nutritec.nutritecpaciente.connection;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.Header;
import retrofit2.http.Headers;
import retrofit2.http.POST;

public interface Methods {

    @Headers("Content-Type: application/json")
    @POST("/Cliente")
    Call<Cliente> getClientData(@Body Cliente cliente);

    @Headers("Content-Type: application/json")
    @POST("/Consumo/producto")
    Call<ConsumoProducto> getConsumoProductoData(@Body ConsumoProducto c_p);

    @Headers("Content-Type: application/json")
    @POST("/Consumo/receta")
    Call<ConsumoReceta> getConsumoRecetaData(@Body ConsumoReceta c_r);

    @Headers("Content-Type: application/json")
    @POST("/Medida")
    Call<Medida> getMedidaData(@Body Medida medida);

}
