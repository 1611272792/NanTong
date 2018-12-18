package dialog;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.Context;
import android.os.Bundle;
import android.text.TextUtils;
import android.view.View;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import com.sunpn.productionplan.App;
import com.sunpn.productionplan.R;

import java.util.List;

import entity.ProductLineDataEntity;

/**
 * Created by guhh on 2018/3/27.
 */

public class EnterPwdDialog extends AlertDialog {
    private ProductLineDataEntity.LineDataBean lineDataBean;
    private String pwd;
    private boolean isTruePwd = false;
    private Activity activity;
    private App app ;
    private Button confirm_btn;
    private Button cancel_btn;
    private EditText pwd_et;
    public EnterPwdDialog(Context context) {
        super(context);
        activity = (Activity) context;
    }

    protected EnterPwdDialog(Context context, boolean cancelable, OnCancelListener cancelListener) {
        super(context, cancelable, cancelListener);
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.dialog_enter_pwd);
        getWindow().clearFlags(WindowManager.LayoutParams.FLAG_ALT_FOCUSABLE_IM);
        getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_ADJUST_RESIZE |
                WindowManager.LayoutParams.SOFT_INPUT_STATE_HIDDEN);
        setCancelable(false);
        app = (App) activity.getApplication();
        confirm_btn = findViewById(R.id.confirm_btn);
        pwd_et = findViewById(R.id.pwd_et);
        confirm_btn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                String pwd = pwd_et.getText().toString().trim();
                if(!TextUtils.isEmpty(pwd)){
                    if(pwd.equals(EnterPwdDialog.this.pwd)){
                        isTruePwd = true;
                        Toast.makeText(getContext(),"验证密码成功",Toast.LENGTH_SHORT).show();
                        dismiss();
                    }else{
                        isTruePwd = false;
                        pwd_et.setError("密码不正确");
                    }
                }else{
                    pwd_et.setError("密码不能为空");
                }
            }
        });
        cancel_btn = findViewById(R.id.cancel_btn);
        cancel_btn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                dismiss();
            }
        });

    }

    @Override
    public void show() {
        super.show();
        isTruePwd = false;
    }

    @Override
    public void dismiss() {
        super.dismiss();
        pwd_et.setText("");
        pwd_et.requestFocus();
    }

    public boolean isTruePwd() {
        return isTruePwd;
    }

    public ProductLineDataEntity.LineDataBean getLineDataBean() {
        return lineDataBean;
    }

    public void setLineDataBeans(ProductLineDataEntity.LineDataBean lineDataBean) {
        this.lineDataBean = lineDataBean;
    }

    public String getPwd() {
        return pwd;
    }

    public void setPwd(String pwd) {
        this.pwd = pwd;
    }
}
