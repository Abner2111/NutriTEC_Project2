package com.nutritec.nutritecpaciente;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.os.StrictMode;
import android.util.Log;
import android.view.View;

import com.nutritec.nutritecpaciente.databinding.ActivityMeasurementRegisterBinding;
import com.nutritec.nutritecpaciente.connection.APIhandler;
public class MeasurementRegisterActivity extends AppCompatActivity {
    ActivityMeasurementRegisterBinding binding;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        binding = ActivityMeasurementRegisterBinding.inflate(getLayoutInflater());
        setContentView(binding.getRoot());

        StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder().permitAll().build();
        StrictMode.setThreadPolicy(policy);

        binding.fatPercentagePicker.setMinValue(1);
        binding.fatPercentagePicker.setMaxValue(90);
        binding.fatPercentagePicker.setWrapSelectorWheel(true);

        binding.musclePercentagePicker.setMinValue(1);
        binding.musclePercentagePicker.setMaxValue(90);
        binding.musclePercentagePicker.setWrapSelectorWheel(true);

        binding.IngresarMedidasbtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                String waist = binding.waistMeasureinpt.getText().toString();
                String fat = String.valueOf(binding.fatPercentagePicker.getValue());
                String muscle = String.valueOf(binding.musclePercentagePicker.getValue());
                String hip = String.valueOf(binding.hipMeasureinpt.getText());
                String neck = binding.neckMeasureinpt.getText().toString();
                String email = getIntent().getExtras().getString("userEmail");
                int result = APIhandler.registrarMedidas(waist,fat,muscle,hip ,neck, email);
                Log.d("Medidas post result", String.valueOf(result));
            }
        });
    }

}