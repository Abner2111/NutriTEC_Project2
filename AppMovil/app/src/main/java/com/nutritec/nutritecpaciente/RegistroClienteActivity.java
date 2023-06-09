package com.nutritec.nutritecpaciente;

import android.app.DatePickerDialog;
import android.content.Intent;
import android.os.Build;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.DatePicker;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import androidx.annotation.RequiresApi;
import androidx.appcompat.app.AppCompatActivity;
import androidx.fragment.app.DialogFragment;


import com.nutritec.nutritecpaciente.databinding.ActivityRegistroClienteBinding;

import java.io.IOException;
import java.text.SimpleDateFormat;
import java.time.LocalDate;
import java.time.Period;
import java.util.Calendar;

import com.nutritec.nutritecpaciente.connection.APIhandler;
import com.nutritec.nutritecpaciente.login.LoginActivity;

public class RegistroClienteActivity extends AppCompatActivity implements DatePickerDialog.OnDateSetListener {
    ActivityRegistroClienteBinding binding;


    String fecha_numerica = "";

    @Override
    protected void onCreate(Bundle savedInstanceState) {

        super.onCreate(savedInstanceState);
        binding = ActivityRegistroClienteBinding.inflate(getLayoutInflater());

        setContentView(binding.getRoot());




        //Click listener que abre datePicker
        binding.userFechaNacimientoBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                DialogFragment datePicker = new DatePickerFragment();
                datePicker.show(getSupportFragmentManager(), "Seleccione Fecha de Nacimiento");
            }
        });

        //Click listener de boton de registro
        binding.userRegistrarseBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                String username = String.valueOf(binding.userNameEdittext.getText());
                String apellido1 = String.valueOf(binding.userApellido1Edittext.getText());
                String apellido2 = String.valueOf(binding.userApellido2Edittext.getText());
                String correo_e = String.valueOf(binding.userEmailEdittext.getText());
                String pais = String.valueOf(binding.userPaisEdittext.getText());
                String pwd = String.valueOf(binding.userPasswordregistryEdittext.getText());
                String bdate = String.valueOf(binding.userDateText.getText());
                String weight = String.valueOf(binding.userWeightEdittext.getText());
                String altura = String.valueOf(binding.userHeightEdittext.getText());
                Calendar c = Calendar.getInstance();
                SimpleDateFormat SDFormat = new SimpleDateFormat("yyyy-MM-dd");
                String currentDateString = SDFormat.format(c.getTime());

                if(username.equals("")
                        ||apellido1.equals("")
                        ||correo_e.equals("")
                        ||pais.equals("")
                        ||pwd.equals("")
                        ||bdate.equals("")
                        ||weight.equals("")
                        ||altura.equals("")
                ){
                    Toast.makeText(RegistroClienteActivity.this, "Rellene todos los campos que tienen un *", Toast.LENGTH_LONG).show();
                }
                APIhandler.registerNewClient(correo_e,username,apellido1,apellido2,pwd,
                        pais,currentDateString,bdate,Integer.parseInt(altura),Integer.parseInt(weight));
                Intent login = new Intent(RegistroClienteActivity.this, LoginActivity.class);
                startActivity(login);
                finish();


            }
        });
    }

    //Metodo para seleccion de fecha
    @RequiresApi(api = Build.VERSION_CODES.O)
    @Override
    public void onDateSet(DatePicker datePicker, int year, int month, int day) {
        Calendar c =  Calendar.getInstance();
        c.set(Calendar.YEAR, year);
        c.set(Calendar.MONTH, month);
        c.set(Calendar.DAY_OF_MONTH, day);

        SimpleDateFormat SDFormat = new SimpleDateFormat("yyyy-MM-dd");
        fecha_numerica = String.valueOf(year)+String.valueOf(month)+String.valueOf(day);
        String currentDateString = SDFormat.format(c.getTime());


        binding.userDateText.setText(currentDateString);

        String curr_age = "";
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.O) {
            curr_age = String.valueOf(Period.between(LocalDate.of(year, month, day), LocalDate.now()).getYears());
        }


        binding.userEdadEdittext.setText(curr_age);
        binding.userEdadEdittext.setEnabled(false);


    }



}