package com.nutritec.nutritecpaciente;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.os.StrictMode;
import android.util.Log;
import android.view.View;
import android.widget.Toast;

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

        binding.fatPercentagePicker.setMinValue(0);
        binding.fatPercentagePicker.setMaxValue(100);
        binding.fatPercentagePicker.setWrapSelectorWheel(true);

        binding.musclePercentagePicker.setMinValue(0);
        binding.musclePercentagePicker.setMaxValue(100);
        binding.musclePercentagePicker.setWrapSelectorWheel(true);

        binding.IngresarMedidasbtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                String waist = binding.waistMeasureinpt.getText().toString();
                Log.d("waist",waist);
                String fat = String.valueOf(binding.fatPercentagePicker.getValue());
                Log.d("fat",fat);
                String muscle = String.valueOf(binding.musclePercentagePicker.getValue());
                Log.d("muscle",muscle);
                String hip = String.valueOf(binding.hipMeasureinpt.getText());
                String neck = binding.neckMeasureinpt.getText().toString();
                String email = getIntent().getExtras().getString("userEmail");
                Log.d("Email",email);
                int result = APIhandler.registrarMedidas(waist,fat,muscle,hip ,neck, email);
                if(result>=0){
                    Toast.makeText(getApplicationContext(),"Medida registrada con éxito",Toast.LENGTH_LONG).show();
                    binding.waistMeasureinpt.setText("");
                    binding.fatPercentagePicker.setValue(0);
                    binding.musclePercentagePicker.setValue(0);
                    binding.hipMeasureinpt.setText("");
                    binding.neckMeasureinpt.setText("");

                }
            }
        });
    }

}