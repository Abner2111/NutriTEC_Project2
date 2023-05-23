package com.nutritec.nutritecpaciente;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.widget.SearchView;

import com.nutritec.nutritecpaciente.databinding.ActivityConsumableRegisterBinding;


import java.util.ArrayList;

public class ConsumableRegisterActivity extends AppCompatActivity {
    ActivityConsumableRegisterBinding binding;
    public static ArrayList<Consumable> consumableList = new ArrayList<>();
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        binding = ActivityConsumableRegisterBinding.inflate(getLayoutInflater());
        setUpData();
        setUpList();
        initSearchWidgets();
        setContentView(binding.getRoot());
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
                ConsumableListAdapter adapter = new ConsumableListAdapter(getApplicationContext(), filteredConsumables);
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
    private void setUpList(){
        ConsumableListAdapter adapter = new ConsumableListAdapter(getApplicationContext(),  consumableList);
        binding.listview.setAdapter(adapter);
    }

}