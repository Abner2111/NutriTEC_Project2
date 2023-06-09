package com.nutritec.nutritecpaciente.connection;

import android.annotation.TargetApi;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.FormUrlEncoded;
import retrofit2.http.Headers;
import retrofit2.http.POST;
import retrofit2.http.Url;

public interface Methods {

    @Headers("Content-Type: application/json")
    @POST("/api/Cliente")
    Call<Cliente> getClientData(@Body Cliente cliente);

    @Headers("Content-Type: application/json")
    @POST("/api/Consumo/producto")
    Call<ConsumoProducto> getConsumoProductoData(@Body ConsumoProducto consumoProducto);


    @Headers("Content-Type: application/json")
    @POST("/api/Consumo/receta")
    Call<ConsumoReceta> getConsumoRecetaData(@Body ConsumoReceta consumoReceta);


    @Headers("Content-Type: application/json")
    @POST("/api/Medida")
    Call<Medida> getMedidaData(@Body Medida medida);

}
