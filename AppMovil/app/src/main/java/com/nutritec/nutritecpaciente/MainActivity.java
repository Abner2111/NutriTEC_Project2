package com.nutritec.nutritecpaciente;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;

import com.nutritec.nutritecpaciente.databinding.ActivityMainBinding;
import com.nutritec.nutritecpaciente.login.LoginActivity;


public class MainActivity extends AppCompatActivity {
    ActivityMainBinding binding;
    @Override

    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        binding = ActivityMainBinding.inflate(getLayoutInflater());
        setContentView(binding.getRoot());


        Bundle extras = getIntent().getExtras();

        if(getIntent().hasExtra("userName") && !extras.getString("userName").equals("")){
            binding.userNameShowText.setText(extras.getString("userName"));
        } else {
            Intent login = new Intent(MainActivity.this, LoginActivity.class);
            startActivity(login);
            finish();
        }
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
                measurementRegistryActivity.putExtra("useEmail", extras.getString("userEmail"));
                startActivity(measurementRegistryActivity);
            }
        });

    }
}