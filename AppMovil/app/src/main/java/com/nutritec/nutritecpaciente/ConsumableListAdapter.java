package com.nutritec.nutritecpaciente;

import android.annotation.SuppressLint;
import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;

import org.w3c.dom.Text;

import java.util.ArrayList;

public class ConsumableListAdapter extends ArrayAdapter<Consumable> {
    public ConsumableListAdapter(Context context, ArrayList<Consumable> consumableArrayList){
        super(context, R.layout.consumable_list_item, consumableArrayList);
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

        return convertView;
    }
}


