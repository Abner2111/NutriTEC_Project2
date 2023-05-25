package com.nutritec.nutritecpaciente;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;

import com.nutritec.nutritecpaciente.databinding.ActivityMainBinding;


public class MainActivity extends AppCompatActivity {
    ActivityMainBinding binding;
    @Override

    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        binding = ActivityMainBinding.inflate(getLayoutInflater());
        setContentView(binding.getRoot());

        binding.registrarConsumoBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent consumableRegistryActivity = new Intent(MainActivity.this, MealTimeSelectionActivity.class);
                startActivity(consumableRegistryActivity);
            }
        });

        binding.registrarMedidasBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent measurementRegistryActivity = new Intent(MainActivity.this, MeasurementRegisterActivity.class);
                startActivity(measurementRegistryActivity);
            }
        });

    }
}