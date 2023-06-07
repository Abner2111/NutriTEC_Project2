package com.nutritec.nutritecpaciente.login;

import android.content.Intent;
import android.os.Bundle;
import android.os.StrictMode;
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

public class LoginActivity extends AppCompatActivity {
    ActivityLoginBinding binding;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder().permitAll().build();
        binding = ActivityLoginBinding.inflate(getLayoutInflater());
        StrictMode.setThreadPolicy(policy);
        super.onCreate(savedInstanceState);
        setContentView(binding.getRoot());




        binding.loginRegisterBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                String email_input = String.valueOf(binding.emaiInpt.getText());
                String password_input = String.valueOf(binding.passwordLoginInpt.getText());
                //Regular Expression for email
                String email_regex = "^[A-Za-z0-9+_.-]+@(.+)$";
                //Compile regular expression to get the pattern
                Pattern pattern = Pattern.compile(email_regex);

                if (pattern.matcher(email_regex).matches()){
                    Toast invalid_user = Toast.makeText(getApplicationContext(), R.string.invalid_id, Toast.LENGTH_LONG);
                    invalid_user.show();
                }



            }
    });

}
}