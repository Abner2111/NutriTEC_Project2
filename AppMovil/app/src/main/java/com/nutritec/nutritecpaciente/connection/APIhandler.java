package com.nutritec.nutritecpaciente.connection;
import android.annotation.SuppressLint;
import android.content.Intent;
import android.util.Log;
import android.widget.Toast;

import androidx.annotation.NonNull;

import com.nutritec.nutritecpaciente.Consumable;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;
import java.security.SecureRandom;
import java.security.cert.CertificateException;
import java.security.cert.X509Certificate;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;

import javax.net.ssl.HostnameVerifier;
import javax.net.ssl.HttpsURLConnection;
import javax.net.ssl.SSLContext;
import javax.net.ssl.SSLSession;
import javax.net.ssl.SSLSocketFactory;
import javax.net.ssl.TrustManager;
import javax.net.ssl.X509TrustManager;

import okhttp3.OkHttpClient;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class APIhandler {
    private static Retrofit retrofit;
    private static String BASE_URL = "https://nutritecrestapi.azurewebsites.net/";

    public APIhandler(){
    }
    public static void setBaseUrl(String url){
        BASE_URL = url;
    }

    public static Retrofit getRetrofitInstance(){
        if(retrofit==null){
            retrofit = new Retrofit.Builder().baseUrl(BASE_URL).addConverterFactory(GsonConverterFactory.create()).client(getUnsafeOkHttpClient()).build();
        }
        return retrofit;
    }
    private static OkHttpClient getUnsafeOkHttpClient() {
        try {
            // Create a trust manager that does not validate certificate chains
            final TrustManager[] trustAllCerts = new TrustManager[] {
                    new X509TrustManager() {
                        @Override
                        public void checkClientTrusted(java.security.cert.X509Certificate[] chain, String authType) throws CertificateException {
                        }

                        @Override
                        public void checkServerTrusted(java.security.cert.X509Certificate[] chain, String authType) throws CertificateException {
                        }

                        @Override
                        public java.security.cert.X509Certificate[] getAcceptedIssuers() {
                            return new java.security.cert.X509Certificate[]{};
                        }
                    }
            };

            // Install the all-trusting trust manager
            final SSLContext sslContext = SSLContext.getInstance("SSL");
            sslContext.init(null, trustAllCerts, new java.security.SecureRandom());
            // Create an ssl socket factory with our all-trusting manager
            final SSLSocketFactory sslSocketFactory = sslContext.getSocketFactory();

            OkHttpClient.Builder builder = new OkHttpClient.Builder();
            builder.sslSocketFactory(sslSocketFactory);
            builder.hostnameVerifier(new HostnameVerifier() {
                @Override
                public boolean verify(String hostname, SSLSession session) {
                    return true;
                }
            });

            OkHttpClient okHttpClient = builder.build();
            return okHttpClient;
        } catch (Exception e) {
            throw new RuntimeException(e);
        }
    }
    public static void trustAllCertificates() {
        try {
            TrustManager[] trustAllCerts = new TrustManager[]{
                    new X509TrustManager() {
                        public X509Certificate[] getAcceptedIssuers() {
                            X509Certificate[] myTrustedAnchors = new X509Certificate[0];
                            return myTrustedAnchors;
                        }

                        @Override
                        public void checkClientTrusted(X509Certificate[] certs, String authType) {
                        }

                        @Override
                        public void checkServerTrusted(X509Certificate[] certs, String authType) {
                        }
                    }
            };

            SSLContext sc = SSLContext.getInstance("SSL");
            sc.init(null, trustAllCerts, new SecureRandom());
            HttpsURLConnection.setDefaultSSLSocketFactory(sc.getSocketFactory());
            HttpsURLConnection.setDefaultHostnameVerifier(new HostnameVerifier() {
                @Override
                public boolean verify(String arg0, SSLSession arg1) {
                    return true;
                }
            });
        } catch (Exception e) {
        }
    }
    public static String sendGet(String url) throws Exception{
        trustAllCertificates();
        URL obj = new URL(url);
        HttpURLConnection connection = (HttpURLConnection) obj.openConnection();
        connection.setConnectTimeout(8000);
        connection.setReadTimeout(8000);
        int responseCode = connection.getResponseCode();
        Log.d("Rsponse Code", String.valueOf(responseCode));
        BufferedReader in = new BufferedReader(
                new InputStreamReader(connection.getInputStream())
        );
        String inputLine;
        StringBuilder response = new StringBuilder();
        while((inputLine=in.readLine())!=null){
            response.append(inputLine);
        }
        in.close();
        return response.toString();
    }

    /**
     *
     * @param correo client email
     * @param nombre clients name
     * @param apellido1 clients first surname
     * @param apellido2 clients second surname
     * @param contrasena clients pwd
     * @param pais clients country of origin
     * @param fecha_registro clients registry date
     * @param fecha_nacimiento clients birthdate
     * @param estatura clients height
     * @param peso clients weight
     * @return an integer if the value is 1, post was successful, if it is 0, api rejected it,
     * if it is -1, the post could not be sent
     */
    public static int registerNewClient(String correo, String nombre, String apellido1, String apellido2, String contrasena, String pais, String fecha_registro, String fecha_nacimiento, int estatura, int peso){
        final int[] responsestatus = new int[1];
        Methods methods = getRetrofitInstance().create(Methods.class);
        Call<Cliente> call = methods.getClientData(new Cliente(correo,nombre,apellido1,apellido2,contrasena,pais,fecha_registro,fecha_nacimiento,estatura,peso));
        call.enqueue(new Callback<Cliente>() {
            @Override
            public void onResponse(@NonNull Call<Cliente> call, @NonNull Response<Cliente> response) {
                if(response.isSuccessful()){
                    responsestatus[0] = 1;
                } else {
                    responsestatus[0] = 0;
                    try {
                        Log.d("cliente no registrado", response.errorBody().string());
                    } catch (IOException e) {
                        throw new RuntimeException(e);
                    }

                }
            }

            @Override
            public void onFailure(@NonNull Call<Cliente> call, Throwable t) {
                responsestatus[0] = -1;
            }
        });
        return responsestatus[0];
    }

    /**
     * register consumables in the api
     * @param consumible consumible object(product or recipe)
     * @param correo user email
     * @param tiempocomida mealtime id
     * @return an integer if the value is 1, post was successful, if it is 0, api rejected it,
     *      * if it is -1, the post could not be sent
     */
    public static int registerNewConsumo(Consumable consumible, String correo, int tiempocomida){
        final int[] responsestatus = new int[1];
        Calendar c = Calendar.getInstance();
        SimpleDateFormat SDFormat = new SimpleDateFormat("yyyy-MM-dd");
        String currentDateString = SDFormat.format(c.getTime());

        Methods methods = getRetrofitInstance().create(Methods.class);

        if (!consumible.getIdentifier().equals("")){
            Call<ConsumoProducto> call = methods.getConsumoProductoData(new ConsumoProducto(correo,currentDateString,tiempocomida,Integer.parseInt(consumible.getIdentifier())));
            Log.d("Producto a registrar",correo +" "+currentDateString+" "+tiempocomida+" "+consumible.getIdentifier());
            Log.d("call",call.toString());
            call.enqueue(new Callback<ConsumoProducto>() {
                @Override
                public void onResponse(Call<ConsumoProducto> call,  Response<ConsumoProducto> response) {
                    if(response.isSuccessful()){
                        responsestatus[0] = 1;
                    } else {
                        responsestatus[0] = 0;

                        try {
                            Log.d("error consumo", response.errorBody().string());
                        } catch (IOException e) {
                            Log.d("error",e.toString());
                        }

                    }
                }

                @Override
                public void onFailure(Call<ConsumoProducto> call, Throwable t) {
                    Log.d("error registro","no se pudo enviar");
                    responsestatus[0] = -1;
                }
            });
        } else {
            Call<ConsumoReceta> call = methods.getConsumoRecetaData(new ConsumoReceta(correo, currentDateString,tiempocomida, consumible.getNombre()));

            Log.d("Receta a registrar",correo +" "+currentDateString+" "+tiempocomida+" "+consumible.getNombre());
            call.enqueue(new Callback<ConsumoReceta>() {
                @Override
                public void onResponse(Call<ConsumoReceta> call,Response<ConsumoReceta> response) {
                    if(response.isSuccessful()){
                        responsestatus[0] = 1;
                    } else {
                        responsestatus[0] = 0;
                        try {
                            Log.d("error consumo", response.errorBody().string());
                        } catch (IOException e) {
                            throw new RuntimeException(e);
                        }

                    }
                }

                @Override
                public void onFailure(@NonNull Call<ConsumoReceta> call, Throwable t) {
                    responsestatus[0] = -1;
                }
            });
        }

        return responsestatus[0];
    }

    /**
     *
     * @return JSON array with products from the API
     * @throws Exception if connection is not achieved
     */
    public static JSONArray getProductos() throws Exception {
        String productsString = sendGet(BASE_URL+"/api/producto");
        JSONArray productsJSON = new JSONArray(productsString);
        return productsJSON;
    }

    /**
     *
     * @return JSON arrray with recipes from the API
     * @throws Exception if connection is not achieved
     */
    public static JSONArray getRecetas() throws Exception{
        String recetasString = sendGet(BASE_URL+"/api/Receta");
        JSONArray recetasJSON = new JSONArray(recetasString);
        return recetasJSON;
    }

    public static JSONArray getMealTimes() throws Exception{
        String mealTimeString = sendGet(BASE_URL+"/api/TiempoComida");
        JSONArray mealTimeJSON = new JSONArray(mealTimeString);
        return mealTimeJSON;
    }

    public static JSONArray validarCliente(String correo, String contrasena) throws Exception {
        String sendString = BASE_URL+"/api/Cliente/Login/"+correo+"/"+contrasena;
        Log.d("API request", sendString);
        String clientString = sendGet(sendString);
        JSONArray clientJSON = new JSONArray(clientString);
        Log.d("Cliente Verificado",clientString);
        return clientJSON;
    }

    public static int registrarMedidas(String cintura, String grasa, String musculo, String cadera, String cuello, String correo){
        final int[] responsestatus = new int[1];
        Calendar c = Calendar.getInstance();
        @SuppressLint("SimpleDateFormat") SimpleDateFormat SDFormat = new SimpleDateFormat("yyyy-MM-dd");
        String currentDateString = SDFormat.format(c.getTime());
        Methods methods = getRetrofitInstance().create(Methods.class);
        if (cintura.equals("")){
            cintura = "0";
        }
        if (grasa.equals("")){
            grasa = "0";
        }
        if (musculo.equals("")){
            musculo = "0";
        }
        if (cadera.equals("")){
            cadera = "0";
        }
        if (cuello.equals("")){
            cuello = "0";
        }

        Call<Medida> call = methods.getMedidaData(new Medida(currentDateString, cintura,grasa,musculo,cadera,cuello,correo));
        call.enqueue(new Callback<Medida>() {
            @Override
            public void onResponse(@NonNull Call<Medida> call, @NonNull Response<Medida> response) {
                if(response.isSuccessful()){
                    responsestatus[0] = 1;
                } else {
                    responsestatus[0] = 0;
                    Log.d("Error Medida", response.toString());

                }
            }

            @Override
            public void onFailure(Call<Medida> call, Throwable t) {
                responsestatus[0] = -1;
            }
        });
        return responsestatus[0];
    }
    public static int agregarReceta(String name, String product_id){
        final int[] responsestatus = new int[1];
        Methods methods = getRetrofitInstance().create(Methods.class);
        Call<ProductoReceta> call = methods.getProductoRecetaData(new ProductoReceta(name, product_id));
        call.enqueue(new Callback<ProductoReceta>() {
            @Override
            public void onResponse(@NonNull Call<ProductoReceta> call, @NonNull Response<ProductoReceta> response) {
                if(response.isSuccessful()){
                    responsestatus[0] = 1;
                } else {
                    responsestatus[0] = 0;
                    Log.d("Error receta", response.toString());

                }
            }

            @Override
            public void onFailure(Call<ProductoReceta> call, Throwable t) {
                responsestatus[0] = -1;
                Log.d("Error receta", "no se pudo");
            }
        });
        return responsestatus[0];
    }
}
