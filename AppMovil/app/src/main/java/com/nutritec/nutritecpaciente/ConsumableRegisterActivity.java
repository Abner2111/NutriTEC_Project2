package com.nutritec.nutritecpaciente;

import androidx.appcompat.app.AppCompatActivity;

import android.annotation.SuppressLint;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.SearchView;
import android.widget.Toast;

import com.nutritec.nutritecpaciente.databinding.ActivityConsumableRegisterBinding;


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
        setUpData();
        setUpList();
        initSearchWidgets();
        setContentView(binding.getRoot());
        binding.registroConsumoBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (selecting){
                    showSelection();
                } else {
                    Toast.makeText(getApplicationContext(),"Consumo Registrado",Toast.LENGTH_SHORT).show();
                    finish();
                    Intent home = new Intent(ConsumableRegisterActivity.this, MainActivity.class);
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

    private void setUpData(){
        consumableList.add(new Consumable("Gallo Pinto","300", ""));
        consumableList.add(new Consumable("Manzana","60",  "11111111"));
        consumableList.add(new Consumable("Agua","0","22222222"));
        consumableList.add(new Consumable("Casado con carne","550",  ""));
        consumableList.add(new Consumable("Mandarina","75", ""));
        consumableList.add(new Consumable("Hamburguesa con queso","900", ""));
        consumableList.add(new Consumable("Snickers", "488","33333333"));
        consumableList.add(new Consumable("Casado con pescado", "500",""));
        consumableList.add(new Consumable("Agua gasificada","0","44444444"));
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