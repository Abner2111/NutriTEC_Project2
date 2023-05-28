package com.nutritec.nutritecpaciente;

import androidx.appcompat.app.AppCompatActivity;

import android.annotation.SuppressLint;
import android.os.Bundle;
import android.view.View;
import android.widget.Toast;

import com.nutritec.nutritecpaciente.databinding.ActivityNuevaRecetaBinding;

import java.util.ArrayList;

public class NuevaRecetaActivity extends AppCompatActivity {
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
        setUpData();
        setUpList();
        binding.registroRecetaBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (selecting){
                    showSelection();
                } else {
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
    private void setUpData(){
        productList.add(new Consumable("Sal","300", ""));
        productList.add(new Consumable("Arroz","60",  "11111111"));
        productList.add(new Consumable("Frijoles","0","22222222"));
        productList.add(new Consumable("Aceite","550",  ""));
        productList.add(new Consumable("Salsa Lizano","75", ""));
        productList.add(new Consumable("Cebolla","900", ""));
        productList.add(new Consumable("Chile Dulce", "488","33333333"));
        productList.add(new Consumable("Carne de Res", "500",""));
        productList.add(new Consumable("Huevo","0","44444444"));
    }
    private void setUpList(){
        ConsumableListAdapter adapter = new ConsumableListAdapter(getApplicationContext(),  productList, selectedProducts);
        binding.listview.setAdapter(adapter);
    }


}