package dialog;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.Context;
import android.os.Bundle;
import android.support.design.widget.BaseTransientBottomBar;
import android.support.design.widget.Snackbar;
import android.text.Editable;
import android.text.TextUtils;
import android.text.TextWatcher;
import android.view.View;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.EditText;
import android.widget.SimpleAdapter;
import android.widget.Spinner;

import com.sunpn.productionplan.App;
import com.sunpn.productionplan.R;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

import entity.ProccessEntity;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import util.HttpUtil;
import util.Util;

/**
 * Created by guhh on 2018/3/19.
 */

public class EnterBaseInfoDialog extends AlertDialog {
    private Activity activity;
    private App app;
    private EditText ip_et;
    private Button confirm_btn;
    private Button cancel_btn;
    private Button getProductLine_btn;
    private Spinner productLine_sp;
    private List<HashMap<String,String>> data = new ArrayList<>();
    private SimpleAdapter simpleAdapter;


    public EnterBaseInfoDialog(Context context) {
        super(context);
        activity = (Activity) context;
        app = (App) activity.getApplication();
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.dialog_enter_ccount);
        getWindow().clearFlags(WindowManager.LayoutParams.FLAG_ALT_FOCUSABLE_IM);
        setCancelable(false);
        initView();

        confirm_btn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if(checkInput()){
                    String url = ip_et.getText().toString().trim();
                    app.setUrl(url);
                    Util.saveData(url,getContext());
                    dismiss();
                }
            }
        });


        cancel_btn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                dismiss();
            }
        });
    }

    @Override
    public void show() {
        super.show();
        HashMap<String,String> h = Util.getData(getContext());
        ip_et.setText(h.get("url"));
        if(!TextUtils.isEmpty(h.get("url"))){
            getProductLine_btn.callOnClick();
        }

        ip_et.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence charSequence, int i, int i1, int i2) {

            }

            @Override
            public void onTextChanged(CharSequence charSequence, int i, int i1, int i2) {

            }

            @Override
            public void afterTextChanged(Editable editable) {
                data.clear();
                simpleAdapter.notifyDataSetChanged();
            }
        });
    }


    private void initView() {
        ip_et =  findViewById(R.id.ip_et);
        productLine_sp = findViewById(R.id.productLine_sp);
        HashMap<String,String> hashMap = new HashMap<>();
        hashMap.put("name","请选择工序终端");
        hashMap.put("id", "-1");
        data.add(0,hashMap);
        simpleAdapter = new SimpleAdapter(getContext(),data,R.layout.textview1,new String[]{"name"},new int[]{R.id.name_tv});
        productLine_sp.setAdapter(simpleAdapter);
        confirm_btn =  findViewById(R.id.confirm_btn);
        cancel_btn = findViewById(R.id.cancel_btn);
        getProductLine_btn = findViewById(R.id.getProductLine_btn);
    }

    private boolean checkInput(){
        String ip = ip_et.getText().toString().trim();
        if(TextUtils.isEmpty(ip)){
            ip_et.setError("网址不能为空");
            ip_et.requestFocus();
            return  false;
        }else{
            return true;
        }
    }

}
