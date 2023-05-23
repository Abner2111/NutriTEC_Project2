package com.nutritec.nutritecpaciente;

import androidx.appcompat.app.AppCompatActivity;
import android.os.Bundle;

import com.nutritec.nutritecpaciente.databinding.ActivityMainBinding;


public class MainActivity extends AppCompatActivity {
    ActivityMainBinding binding;
    @Override

    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        binding = ActivityMainBinding.inflate(getLayoutInflater());
        setContentView(binding.getRoot());


    }
}