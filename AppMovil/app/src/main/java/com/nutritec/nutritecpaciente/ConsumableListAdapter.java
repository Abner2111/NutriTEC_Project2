package com.nutritec.nutritecpaciente;

import android.annotation.SuppressLint;
import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;

import org.w3c.dom.Text;

import java.util.ArrayList;

public class ConsumableListAdapter extends ArrayAdapter<Consumable> {
    ArrayList<Consumable> selectedArrayList;
    ArrayList<Consumable> consumableArrayList;
    public ConsumableListAdapter(Context context, ArrayList<Consumable> consumableArrayList){
        super(context, R.layout.consumable_list_item, consumableArrayList);
        this.consumableArrayList = consumableArrayList;
        this.selectedArrayList = null;
    }
    public ConsumableListAdapter(Context context, ArrayList<Consumable> consumableArrayList, ArrayList<Consumable> selectedArrayList){
        super(context, R.layout.consumable_list_item, consumableArrayList);
        this.consumableArrayList = consumableArrayList;
        this.selectedArrayList = selectedArrayList;
    }
    @SuppressLint("SetTextI18n")
    @NonNull
    @Override
    public View getView(int position, @Nullable View convertView, @NonNull ViewGroup parent) {

        Consumable consumable = getItem(position);

        if(convertView == null){
            convertView = LayoutInflater.from(getContext()).inflate(R.layout.consumable_list_item, parent, false);
        }

        TextView nombre = convertView.findViewById(R.id.nombre_consumible_lbl);

        TextView barcode = convertView.findViewById(R.id.barcode_lbl);

        nombre.setText(consumable.getNombre());

        barcode.setText(consumable.getBarcode());

        Button agregar_btn = convertView.findViewById(R.id.addConsumable_btn);

        agregar_btn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if(ConsumableListAdapter.this.selectedArrayList!=null){
                    Consumable selectedConsumable = new Consumable();
                    selectedConsumable.setBarcode(ConsumableListAdapter.this.consumableArrayList.get(position).getBarcode());
                    selectedConsumable.setNombre(ConsumableListAdapter.this.consumableArrayList.get(position).getNombre());
                    selectedConsumable.setCalories(ConsumableListAdapter.this.consumableArrayList.get(position).getCalories());
                    selectedConsumable.setIdentifier(ConsumableListAdapter.this.consumableArrayList.get(position).getIdentifier());
                    ConsumableListAdapter.this.selectedArrayList.add(selectedConsumable);
                    Toast.makeText(getContext(),ConsumableListAdapter.this.consumableArrayList.get(position).getNombre()+" agregado",Toast.LENGTH_SHORT).show();
                }
            }
        });

        return convertView;
    }
}


