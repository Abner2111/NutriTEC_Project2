package com.nutritec.nutritecpaciente;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.RadioButton;
import android.widget.RadioGroup;
import android.widget.Toast;

import com.nutritec.nutritecpaciente.databinding.ActivityMealTimeSelectionBinding;

import java.util.HashMap;

public class MealTimeSelectionActivity extends AppCompatActivity {
    HashMap<String, Integer> mealTimesMap = new HashMap<>();

    Integer selectedMealTime;
    ActivityMealTimeSelectionBinding binding;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        binding = ActivityMealTimeSelectionBinding.inflate(getLayoutInflater());
        setContentView(binding.getRoot());

        //Setting dictionary values for mealtimes
        mealTimesMap.put(getResources().getString(R.string.breakfastlbl),1);
        mealTimesMap.put(getResources().getString(R.string.morningsnacklabl),2);
        mealTimesMap.put(getResources().getString(R.string.lunchlbl),3);
        mealTimesMap.put(getResources().getString(R.string.afternoonsnacklbl),4);
        mealTimesMap.put(getResources().getString(R.string.cenalbl),5);
        addListenerOnButton();
        binding.goToCconsumablesBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                int selectedMeal=0;
                String selectedMealText;
                Intent consumableSelection = new Intent(MealTimeSelectionActivity.this, ConsumableRegisterActivity.class);
                int selectedId = binding.mealTimeSelecctor.getCheckedRadioButtonId();
                RadioButton selectedButton = (RadioButton) findViewById(selectedId);
                selectedMealText=selectedButton.getText().toString();
                selectedMeal = mealTimesMap.get(selectedMealText);
                Log.d("SelectedMealTimeID", String.valueOf(selectedMeal));
                consumableSelection.putExtra("MealTime", selectedMeal);

                startActivity(consumableSelection);
                finishAffinity();
            }
        });
    }

    public void addListenerOnButton() {



        binding.mealTimeSelecctor.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View v) {

                // get selected radio button from radioGroup
                int selectedId = binding.mealTimeSelecctor.getCheckedRadioButtonId();

                // find the radiobutton by returned id
                RadioButton radioButton = (RadioButton) findViewById(selectedId);

                Toast.makeText(MealTimeSelectionActivity.this,
                        radioButton.getText(), Toast.LENGTH_SHORT).show();

            }

        });

    }
}