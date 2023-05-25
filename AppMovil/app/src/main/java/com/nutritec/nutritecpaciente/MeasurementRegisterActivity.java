package com.nutritec.nutritecpaciente;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;

import com.nutritec.nutritecpaciente.databinding.ActivityMeasurementRegisterBinding;

public class MeasurementRegisterActivity extends AppCompatActivity {
    ActivityMeasurementRegisterBinding binding;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        binding = ActivityMeasurementRegisterBinding.inflate(getLayoutInflater());
        setContentView(binding.getRoot());
        binding.fatPercentagePicker.setMinValue(1);
        binding.fatPercentagePicker.setMaxValue(90);
        binding.fatPercentagePicker.setWrapSelectorWheel(true);

        binding.musclePercentagePicker.setMinValue(1);
        binding.musclePercentagePicker.setMaxValue(90);
        binding.musclePercentagePicker.setWrapSelectorWheel(true);
    }

}