package com.nutritec.nutritecpaciente;

import androidx.appcompat.app.AppCompatActivity;

import android.annotation.SuppressLint;
import android.content.Intent;
import android.os.Bundle;
import android.os.StrictMode;
import android.util.Log;
import android.view.View;
import android.widget.SearchView;
import android.widget.Toast;

import com.nutritec.nutritecpaciente.databinding.ActivityConsumableRegisterBinding;
import com.nutritec.nutritecpaciente.connection.APIhandler;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;

public class ConsumableRegisterActivity extends AppCompatActivity {
    ActivityConsumableRegisterBinding binding;
    public  ArrayList<Consumable> consumableList = new ArrayList<>();
    public  ArrayList<Consumable>  selectedConsumables = new ArrayList<>();

    public boolean selecting = true;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        binding = ActivityConsumableRegisterBinding.inflate(getLayoutInflater());
        StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder().permitAll().build();

        StrictMode.setThreadPolicy(policy);
        try {
            Thread setupdataThread = new Thread(){
                public void run(){
                    try {
                        setUpData(consumableList);
                        runOnUiThread(new Runnable() {
                            @Override
                            public void run() {
                                setUpList();
                            }
                        });

                    } catch (Exception e) {
                        throw new RuntimeException(e);
                    }
                }
            };
            setupdataThread.start();

        } catch (Exception e) {
            Toast.makeText(getApplicationContext(), "Error al obtener consumibles",Toast.LENGTH_LONG).show();
            Log.d("Error al obtener consumibles",e.toString());
        }


        setContentView(binding.getRoot());

        initSearchWidgets();


        binding.registroConsumoBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (selecting){
                    showSelection();
                } else {
                    for(Consumable consumable: selectedConsumables){
                        int respuesta = APIhandler.registerNewConsumo(consumable,getIntent().getExtras()
                                .getString("userEmail"),getIntent().getExtras().getInt("mealTime"));
                        Log.d("respuesta post", String.valueOf(respuesta));
                    }
                    Log.d("User consumible",getIntent().getExtras()
                            .getString("userEmail"));
                    Log.d("Meal Consumible", String.valueOf(getIntent().getExtras().getInt("mealTime")));
                    Toast.makeText(getApplicationContext(),"Consumo Registrado",
                            Toast.LENGTH_SHORT).show();
                    finish();
                    Intent home = new Intent(ConsumableRegisterActivity.this,
                            MainActivity.class);
                    home.putExtra("userEmail",getIntent().getExtras().getString("userEmail"));
                    home.putExtra("userName",getIntent().getExtras().getString("userName"));
                    startActivity(home);
                }
            }
        });

        binding.newRecipeBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent newRecipe = new Intent(ConsumableRegisterActivity.this, NewRecetaActivity.class);
                startActivity(newRecipe);
            }
        });

        binding.newProductBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent newProduct = new Intent(ConsumableRegisterActivity.this, NewProductActivity.class);
                startActivity(newProduct);
            }
        });
    }

    private void initSearchWidgets(){
        binding.consumableSearchbox.setOnQueryTextListener(new SearchView.OnQueryTextListener() {
            @Override
            public boolean onQueryTextSubmit(String query) {
                return false;
            }

            @Override
            public boolean onQueryTextChange(String s) {
                ArrayList<Consumable> filteredConsumables = new ArrayList<Consumable>();
                for(Consumable consumable: consumableList){
                    if (consumable.getNombre().toLowerCase().contains(s) || consumable.getBarcode().contains(s)){
                        filteredConsumables.add(consumable);
                    }
                }
                ConsumableListAdapter adapter = new ConsumableListAdapter(getApplicationContext(), filteredConsumables,selectedConsumables);
                binding.listview.setAdapter(adapter);
                return false;
            }
        });
    }

    private void setUpData(ArrayList<Consumable> consumableList) throws Exception {
        JSONArray productos = APIhandler.getProductos();
        JSONArray recetas = APIhandler.getRecetas();

        Log.d("productos obtenidos",productos.toString());
        Log.d("recetas obtenidos",recetas.toString());
        for(int index = 0;index<productos.length();index++){
            if(productos.getJSONObject(index).getString("aprobado").equals("true")){
                Consumable newConsumable = new Consumable();
                newConsumable.setNombre(productos.getJSONObject(index).getString("nombre"));
                newConsumable.setBarcode(productos.getJSONObject(index).getString("codigoBarras"));
                newConsumable.setIdentifier(productos.getJSONObject(index).getString("id"));
                consumableList.add(newConsumable);
            }
        }
        for(int index = 0;index<recetas.length();index++){

                Consumable newConsumable = new Consumable();
                newConsumable.setNombre(recetas.getJSONObject(index).getString("nombre"));
                consumableList.add(newConsumable);

        }





    }
    @SuppressLint("SetTextI18n")
    private void showSelection(){
        binding.consumableRegistrytitle.setText("Selección");
        ConsumableListAdapter adapter = new ConsumableListAdapter(getApplicationContext(), selectedConsumables);
        binding.listview.setAdapter(adapter);
        selecting = false;
        binding.registroConsumoBtn.setText("Confirmar");
    }

    private void setUpList(){
        ConsumableListAdapter adapter = new ConsumableListAdapter(getApplicationContext(),  consumableList, selectedConsumables);
        binding.listview.setAdapter(adapter);
    }

}