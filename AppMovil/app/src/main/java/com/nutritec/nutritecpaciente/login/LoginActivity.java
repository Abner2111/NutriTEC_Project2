package com.nutritec.nutritecpaciente.login;

import android.content.Intent;
import android.os.Bundle;
import android.os.StrictMode;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;

import com.nutritec.nutritecpaciente.MainActivity;
import com.nutritec.nutritecpaciente.R;
import com.nutritec.nutritecpaciente.RegistroClienteActivity;
import com.nutritec.nutritecpaciente.databinding.ActivityLoginBinding;

import java.util.regex.Pattern;
import com.nutritec.nutritecpaciente.connection.APIhandler;

import org.json.JSONArray;

public class LoginActivity extends AppCompatActivity {
    ActivityLoginBinding binding;

    @Override

    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        binding = ActivityLoginBinding.inflate(getLayoutInflater());
        setContentView(binding.getRoot());
        StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder().permitAll().build();

        StrictMode.setThreadPolicy(policy);



        binding.loginRegisterBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                String email_input = String.valueOf(binding.emaiInpt.getText());
                String password_input = String.valueOf(binding.passwordLoginInpt.getText());
                //Regular Expression for email
                String email_regex = "^[A-Za-z0-9+_.-]+@(.+)$";
                //Compile regular expression to get the pattern
                Pattern pattern = Pattern.compile(email_regex);

                if (!pattern.matcher(email_input).matches()){
                    Toast invalid_user = Toast.makeText(getApplicationContext(), R.string.invalid_id, Toast.LENGTH_LONG);
                    invalid_user.show();
                } else {
                    try {
                        JSONArray userJSONArray = APIhandler.validarCliente(email_input,password_input);
                        if(userJSONArray.length()<1){
                            Toast.makeText(getApplicationContext(),"Usuario o contraseña incorrectos", Toast.LENGTH_LONG).show();

                        } else {
                            Intent home = new Intent(LoginActivity.this,MainActivity.class);
                            home.putExtra("userName", userJSONArray.getJSONObject(0).getString("nombre"));
                            home.putExtra("userEmail", userJSONArray.getJSONObject(0).getString("correo"));
                            startActivity(home);
                            finish();
                        }
                    } catch (Exception e) {
                        Log.d("Error en validacion", e.toString());
                        Toast.makeText(getApplicationContext(),"No se pudo validar el usuario", Toast.LENGTH_LONG).show();
                    }

                }



            }
        });

        binding.registerBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent registerView = new Intent(LoginActivity.this, RegistroClienteActivity.class);
                startActivity(registerView);
                finish();
            }
        });

}
}