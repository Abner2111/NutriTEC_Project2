package com.nutritec.nutritecpaciente;

import androidx.appcompat.app.AppCompatActivity;

import android.annotation.SuppressLint;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Toast;

import com.nutritec.nutritecpaciente.connection.APIhandler;
import com.nutritec.nutritecpaciente.databinding.ActivityNuevaRecetaBinding;

import org.json.JSONArray;

import java.util.ArrayList;

public class NewRecetaActivity extends AppCompatActivity {
    ActivityNuevaRecetaBinding binding;

    public ArrayList<Consumable> productList = new ArrayList<>();
    public  ArrayList<Consumable>  selectedProducts = new ArrayList<>();

    public ArrayList<String> currentRecipes = new ArrayList<>();

    boolean selecting = true;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        binding = ActivityNuevaRecetaBinding.inflate(getLayoutInflater());

        setContentView(binding.getRoot());
        Thread setupdataThread = new Thread(){
            public void run(){
                try {
                    setUpData(productList);
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
        Thread getCurrentRecipes = new Thread(){
            public void run(){
                try {
                    getRecipeNames(currentRecipes);

                } catch (Exception e) {
                    throw new RuntimeException(e);
                }
            }
        };

        setupdataThread.start();
        getCurrentRecipes.start();



        binding.registroRecetaBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (selecting){
                    showSelection();
                } else {
                    String recipeName = String.valueOf(binding.recipeNameEntry.getText());
                    if(!recipeName.equals("") && !currentRecipes.contains(recipeName.toLowerCase())){
                        for(int i =0;i<selectedProducts.size();i++){
                            APIhandler.agregarReceta(recipeName,selectedProducts.get(i).getIdentifier());
                        }
                    }
                    Toast.makeText(getApplicationContext(),"Receta Creada",Toast.LENGTH_SHORT).show();
                    finish();
                }
            }
        });
    }

    @SuppressLint("SetTextI18n")
    private void showSelection(){
        binding.createRecipeTitle.setText("Productos Seleccionados");
        ConsumableListAdapter adapter = new ConsumableListAdapter(getApplicationContext(), selectedProducts);
        binding.listview.setAdapter(adapter);
        selecting = false;
        binding.registroRecetaBtn.setText("Confirmar");
    }
    private void setUpData(ArrayList<Consumable> productList) throws Exception {
        JSONArray productos = APIhandler.getProductos();

        Log.d("productos obtenidos",productos.toString());
        for(int index = 0;index<productos.length();index++){
            if(productos.getJSONObject(index).getString("aprobado").equals("true")){
                Consumable newConsumable = new Consumable();
                newConsumable.setNombre(productos.getJSONObject(index).getString("nombre"));
                newConsumable.setBarcode(productos.getJSONObject(index).getString("codigoBarras"));
                newConsumable.setIdentifier(productos.getJSONObject(index).getString("id"));
                productList.add(newConsumable);
            }
        }

    }
    private void setUpList(){
        ConsumableListAdapter adapter = new ConsumableListAdapter(getApplicationContext(),  productList, selectedProducts);
        binding.listview.setAdapter(adapter);
    }

    private void getRecipeNames(ArrayList<String> currentRecipes) throws Exception {
        JSONArray recetas = APIhandler.getRecetas();
        for(int index = 0;index<recetas.length();index++){

            this.currentRecipes.add(recetas.getJSONObject(index).getString("nombre").toLowerCase());

        }
    }




}