package dialog;

import android.app.ProgressDialog;
import android.content.Context;
import android.os.Bundle;
import android.view.Window;
import android.view.WindowManager;
import android.widget.TextView;

import com.sunpn.productionplan.R;

/**
 * Created by guhh on 2018/3/1.
 */

public class LoadDialog extends ProgressDialog {
    private TextView msg_tv;

    public LoadDialog(Context context) {
        super(context);
    }

    public LoadDialog(Context context, int theme) {
        super(context, theme);
    }

    @Override
    public void setMessage(CharSequence message) {
        msg_tv.setText(message);
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.dialog_load);
        Window window = getWindow();
        if(window !=null){
            window.setLayout(WindowManager.LayoutParams.WRAP_CONTENT, WindowManager.LayoutParams.WRAP_CONTENT);
            window.setBackgroundDrawableResource(R.drawable.dialog_bg_trans);
        }
        msg_tv = (TextView) findViewById(R.id.msg_tv);
    }
}
