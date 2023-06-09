package com.nutritec.nutritecpaciente;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.os.StrictMode;
import android.util.Log;
import android.view.View;
import android.widget.RadioButton;
import android.widget.RadioGroup;
import android.widget.Toast;

import com.nutritec.nutritecpaciente.databinding.ActivityMealTimeSelectionBinding;
import com.nutritec.nutritecpaciente.connection.APIhandler;

import org.json.JSONArray;

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
        StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder().permitAll().build();

        StrictMode.setThreadPolicy(policy);
        try {
            setUpMealTimes();
        } catch (Exception e) {
            throw new RuntimeException(e);
        }


        addListenerOnButton();
        binding.goToCconsumablesBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                int selectedMeal=mealTimesMap.entrySet().iterator().next().getValue();
                String selectedMealText;
                Intent consumableSelection = new Intent(MealTimeSelectionActivity.this, ConsumableRegisterActivity.class);
                int selectedId = binding.mealTimeSelecctor.getCheckedRadioButtonId();
                RadioButton selectedButton = (RadioButton) findViewById(selectedId);
                selectedMealText=selectedButton.getText().toString();
                selectedMeal = mealTimesMap.get(selectedMealText);
                Log.d("SelectedMealTimeID", String.valueOf(selectedMeal));
                consumableSelection.putExtra("mealTime", selectedMeal);
                consumableSelection.putExtra("userEmail",getIntent().getExtras().getString("userEmail"));
                consumableSelection.putExtra("userName",getIntent().getExtras().getString("userName"));

                startActivity(consumableSelection);
                finish();
            }
        });
    }

    public void addMealOptions(String mealTimeName){
        RadioButton mealTimeBtn = new RadioButton(binding.mealTimeSelecctor.getContext());
        mealTimeBtn.setId(View.generateViewId());
        mealTimeBtn.setText(mealTimeName);
        mealTimeBtn.setTextSize(25);
        if(binding.mealTimeSelecctor.getChildCount()<=0){
            mealTimeBtn.setChecked(true);
        }
        binding.mealTimeSelecctor.addView(mealTimeBtn);
    }

    public void setUpMealTimes() throws Exception {
        JSONArray mealtimes = APIhandler.getMealTimes();
        for(int index = 0;index<mealtimes.length();index++){
            String name = mealtimes.getJSONObject(index).getString("nombre");
            int id = Integer.parseInt(mealtimes.getJSONObject(index).getString("id"));
            addMealOptions(name);
            mealTimesMap.put(name,id);
        }
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