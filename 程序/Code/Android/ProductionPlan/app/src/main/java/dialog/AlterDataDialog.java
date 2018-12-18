package dialog;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.Context;
import android.opengl.Visibility;
import android.os.Bundle;
import android.provider.Settings;
import android.util.Log;
import android.view.KeyEvent;
import android.view.LayoutInflater;
import android.view.View;
import android.view.WindowManager;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.SimpleAdapter;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import com.google.gson.Gson;
import com.sunpn.productionplan.App;
import com.sunpn.productionplan.R;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.HashMap;
import java.util.List;

import entity.BaseResultBean;
import entity.CheckProcessEntity;
import entity.ProccessEntity;
import entity.ProductLineDataEntity;
import entity.ProductLineInfoEntity;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import util.HttpUtil;

/**
 * Created by guhh on 2018/3/26.
 */
public class AlterDataDialog extends AlertDialog {
    private TextView title_tv;
    private ProductLineDataEntity.LineDataBean lineDataBean;
    private ArrayList<View> dataViews = new ArrayList<>();
    private Activity activity;
    private App app;
    private LinearLayout rootView;
    private Button confirm_btn;
    private Button cancel_btn;
    private String androidID;
    private Spinner state_sp;
    private List<HashMap<String,String>> state_data;

    private Spinner process_sp;
    private View process_view;
    private List<HashMap<String,String>> process_data;

    public AlterDataDialog(Context context) {
        super(context);
        activity = (Activity) context;
        androidID = Settings.Secure.getString(getContext().getContentResolver(), Settings.Secure.ANDROID_ID);

    }

    protected AlterDataDialog(Context context, boolean cancelable, OnCancelListener cancelListener) {
        super(context, cancelable, cancelListener);

    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.dialog_alter_data);
        getWindow().clearFlags(WindowManager.LayoutParams.FLAG_ALT_FOCUSABLE_IM);
        getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_ADJUST_RESIZE |
                WindowManager.LayoutParams.SOFT_INPUT_STATE_HIDDEN);
        setCancelable(false);
        app = (App) activity.getApplication();
        rootView = findViewById(R.id.rootView_ll);
        title_tv = findViewById(R.id.title_tv);
        confirm_btn = findViewById(R.id.confirm_btn);
        cancel_btn = findViewById(R.id.cancel_btn);
        cancel_btn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                dismiss();
            }
        });
        confirm_btn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View eventView) {
                int pid = app.getTid();
                ProductLineDataEntity.LineDataBean newDataBean = new ProductLineDataEntity.LineDataBean();
                for (View view:dataViews){
                    if(view instanceof EditText){
                        EditText editText = ((EditText) view);
                        if(editText.getTag().equals("ProductionPlanID")){
                            newDataBean.setProductionPlanID(editText.getText().toString());
                        }else if(editText.getTag().equals("ProductionPlanName")){
                            newDataBean.setProductionPlanName(editText.getText().toString());
                        }else if(editText.getTag().equals("ProductionPlanVersion")){
                            newDataBean.setProductionPlanVersion(editText.getText().toString());
                        }else if(editText.getTag().equals("PlanNum")){
                            newDataBean.setPlanNum(editText.getText().toString());
                        }else if(editText.getTag().equals("RealNum")){
                            newDataBean.setRealNum(editText.getText().toString());
                        }else if(editText.getTag().equals("StartTime")){
                            newDataBean.setStartTime(editText.getText().toString());
                        }else if(editText.getTag().equals("EndTime")){
                            newDataBean.setEndTime(editText.getText().toString());
                        }else if(editText.getTag().equals("TerminalID")){
                            newDataBean.setTerminalID(editText.getText().toString());
                        }else if(editText.getTag().equals("c1")){
                            newDataBean.setC1(editText.getText().toString());
                        }else if(editText.getTag().equals("c2")){
                            newDataBean.setC2(editText.getText().toString());
                        }else if(editText.getTag().equals("c3")){
                            newDataBean.setC3(editText.getText().toString());
                        }else if(editText.getTag().equals("c4")){
                            newDataBean.setC4(editText.getText().toString());
                        }else if(editText.getTag().equals("c5")){
                            newDataBean.setC5(editText.getText().toString());
                        }else if(editText.getTag().equals("c6")){
                            newDataBean.setC6(editText.getText().toString());
                        }else if(editText.getTag().equals("c7")){
                            newDataBean.setC7(editText.getText().toString());
                        }else if(editText.getTag().equals("c8")){
                            newDataBean.setC8(editText.getText().toString());
                        }else if(editText.getTag().equals("c9")){
                            newDataBean.setC9(editText.getText().toString());
                        }else if(editText.getTag().equals("c10")){
                            newDataBean.setC10(editText.getText().toString());
                        }else if(editText.getTag().equals("c11")){
                            newDataBean.setC11(editText.getText().toString());;
                        }else if(editText.getTag().equals("c12")){
                            newDataBean.setC12(editText.getText().toString());
                        }else if(editText.getTag().equals("c13")){
                            newDataBean.setC13(editText.getText().toString());
                        }else if(editText.getTag().equals("c14")){
                            newDataBean.setC14(editText.getText().toString());
                        }else if(editText.getTag().equals("c15")){
                            newDataBean.setC15(editText.getText().toString());
                        }else if(editText.getTag().equals("c16")){
                            newDataBean.setC16(editText.getText().toString());;
                        }else if(editText.getTag().equals("c17")){
                            newDataBean.setC17(editText.getText().toString());
                        }else if(editText.getTag().equals("c18")){
                            newDataBean.setC18(editText.getText().toString());
                        }else if(editText.getTag().equals("c19")){
                            newDataBean.setC19(editText.getText().toString());
                        }else if(editText.getTag().equals("c20")){
                            newDataBean.setC20(editText.getText().toString());
                        }
                    }else{
                        if(view.getTag().equals("State")){
                            Spinner state_sp = ((Spinner) view);
                            newDataBean.setState(Integer.parseInt(state_data.get(state_sp.getSelectedItemPosition()).get("sid")));
                        }
                    }
                }

                newDataBean.setTerminalID(String.valueOf(app.getTid()));
                newDataBean.setProcessId(app.getPid());
                newDataBean.setNextProcessId("");
                newDataBean.setNextTerminalId("");
                newDataBean.setNextStateId("");
                if(process_view != null && process_view.getVisibility() == View.VISIBLE){
                    String nextProcess = process_data.get(process_sp.getSelectedItemPosition()).get("pid");
                    String nextTerminal = process_data.get(process_sp.getSelectedItemPosition()).get("tid");
                    String nextStateId  = process_data.get(process_sp.getSelectedItemPosition()).get("sid");
                    newDataBean.setNextProcessId(nextProcess);
                    newDataBean.setNextTerminalId(nextTerminal);
                    newDataBean.setNextStateId(nextStateId);
                }
                Call<BaseResultBean> call = HttpUtil.getInstance(app.getUrl()).alterProductionData(pid,androidID,new Gson().toJson(newDataBean));
                call.enqueue(new Callback<BaseResultBean>() {
                    @Override
                    public void onResponse(Call<BaseResultBean> call, Response<BaseResultBean> response) {
                        BaseResultBean baseResultBean = response.body();
                        if(baseResultBean != null && baseResultBean.getStateCode() == 100){
                            Toast.makeText(getContext(),"修改成功",Toast.LENGTH_SHORT).show();
                            dismiss();
                        }else{
                            Toast.makeText(getContext(),"修改失败",Toast.LENGTH_SHORT).show();
                        }
                    }

                    @Override
                    public void onFailure(Call<BaseResultBean> call, Throwable t) {
                        Toast.makeText(getContext(),"修改出错"+t.getMessage(),Toast.LENGTH_SHORT).show();
                    }
                });
            }
        });
    }

    @Override
    public boolean onKeyDown(int keyCode, KeyEvent event) {
//        if(keyCode == 67){
//            try {
//                state_sp.setSelection(1);
//                confirm_btn.performClick();
//            }catch (Exception e){
//                Toast.makeText(getContext(),e.getMessage(),Toast.LENGTH_SHORT).show();
//            }
//        }
        return super.onKeyDown(keyCode, event);
    }

    @Override
    public void show() {
        super.show();
        rootView.removeAllViews();
        //初始化输入框
        List<ProductLineInfoEntity.LineInfoBean.FieldsBean> fieldsBean = app.getProductLineInfoEntity().getLineInfo().getFields();
        for(ProductLineInfoEntity.LineInfoBean.FieldsBean fieldBean : fieldsBean){
            View view = null;
            if(fieldBean.getField().equals("State")){
                view = LayoutInflater.from(getContext()).inflate(R.layout.spinner,null);
                TextView textView = view.findViewById(R.id.fieldName_tv);
                state_sp = view.findViewById(R.id.state_sp);
                textView.setText(fieldBean.getFieldName());
                state_sp.setTag(fieldBean.getField());
                state_sp.requestFocus();
                state_sp.setSelection(lineDataBean.getState());
                state_data = initSpData(app.getProductLineDataEntity().getState());
                SimpleAdapter simpleAdapter = new SimpleAdapter(getContext(),state_data,R.layout.textview2,new String[]{"name"},new int[]{R.id.name_tv});
                state_sp.setAdapter(simpleAdapter);
                state_sp.requestFocus();
                dataViews.add(state_sp);
                state_sp.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
                    @Override
                    public void onItemSelected(AdapterView<?> adapterView, View view1, int i, long l) {
                        confirm_btn.setEnabled(false);
                        confirm_btn.setText("加载中...");
                        getNextProccess(state_data.get(state_sp.getSelectedItemPosition()).get("sid"));
                    }

                    @Override
                    public void onNothingSelected(AdapterView<?> adapterView) {

                    }
                });
            }else{
                view = LayoutInflater.from(getContext()).inflate(R.layout.item_alert_data,null);
                TextView textView = view.findViewById(R.id.fieldName_tv);
                EditText editText = view.findViewById(R.id.fieldData_et);
                textView.setText(fieldBean.getFieldName());
                editText.setTag(fieldBean.getField());
                if(fieldBean.getField().equals("ProductionPlanID")){
                    editText.setText(lineDataBean.getProductionPlanID());
                    textView.setVisibility(View.GONE);
                    editText.setVisibility(View.GONE);
                }else if(fieldBean.getField().equals("ProjectNo")){
                    editText.setText(lineDataBean.getProjectNo());
                }else if(fieldBean.getField().equals("Customer")){
                    editText.setText(lineDataBean.getCustomer());
                }else if(fieldBean.getField().equals("ProductionPlanName")){
                    editText.setText(lineDataBean.getProductionPlanName());
                }else if(fieldBean.getField().equals("ProductionPlanVersion")){
                    editText.setText(lineDataBean.getProductionPlanVersion());
                }else if(fieldBean.getField().equals("PlanNum")){
                    editText.setText(lineDataBean.getPlanNum());
                }else if(fieldBean.getField().equals("RealNum")){
                    editText.setText(lineDataBean.getRealNum());
                }else if(fieldBean.getField().equals("StartTime")){
                    editText.setEnabled(false);
                    editText.setText(lineDataBean.getStartTime());
                }else if(fieldBean.getField().equals("EndTime")){
                    editText.setEnabled(false);
                    editText.setText(lineDataBean.getEndTime());
                }else if(fieldBean.getField().equals("TerminalID")){
                    editText.setText(lineDataBean.getTerminalID());
                    textView.setVisibility(View.GONE);
                    editText.setVisibility(View.GONE);
                }else if(fieldBean.getField().equals("c1")){
                    editText.setText(lineDataBean.getC1());
                }else if(fieldBean.getField().equals("c2")){
                    editText.setText(lineDataBean.getC2());
                }else if(fieldBean.getField().equals("c3")){
                    editText.setText(lineDataBean.getC3());
                }else if(fieldBean.getField().equals("c4")){
                    editText.setText(lineDataBean.getC4());
                }else if(fieldBean.getField().equals("c5")){
                    editText.setText(lineDataBean.getC5());
                }else if(fieldBean.getField().equals("c6")){
                    editText.setText(lineDataBean.getC6());
                }else if(fieldBean.getField().equals("c7")){
                    editText.setText(lineDataBean.getC7());
                }else if(fieldBean.getField().equals("c8")){
                    editText.setText(lineDataBean.getC8());
                }else if(fieldBean.getField().equals("c9")){
                    editText.setText(lineDataBean.getC9());
                }else if(fieldBean.getField().equals("c10")){
                    editText.setText(lineDataBean.getC10());
                }else if(fieldBean.getField().equals("c11")){
                    editText.setText(lineDataBean.getC11());
                }else if(fieldBean.getField().equals("c12")){
                    editText.setText(lineDataBean.getC12());
                }else if(fieldBean.getField().equals("c13")){
                    editText.setText(lineDataBean.getC13());
                }else if(fieldBean.getField().equals("c14")){
                    editText.setText(lineDataBean.getC14());
                }else if(fieldBean.getField().equals("c15")){
                    editText.setText(lineDataBean.getC15());
                }else if(fieldBean.getField().equals("c16")){
                    editText.setText(lineDataBean.getC16());
                }else if(fieldBean.getField().equals("c17")){
                    editText.setText(lineDataBean.getC17());
                }else if(fieldBean.getField().equals("c18")){
                    editText.setText(lineDataBean.getC18());
                }else if(fieldBean.getField().equals("c19")){
                    editText.setText(lineDataBean.getC19());
                }else if(fieldBean.getField().equals("c20")){
                    editText.setText(lineDataBean.getC20());
                }
                editText.setClickable(false);
                editText.setFocusable(false);
                editText.setEnabled(false);
                dataViews.add(editText);
            }
            rootView.addView(view);
        }

        if(process_view != null){
            rootView.addView(process_view);

        }
        //初始化弹窗标题
        title_tv.setText(lineDataBean.getProductionPlanName());

    }

    private void getNextProccess(String sid) {
        Call<CheckProcessEntity> call = HttpUtil.getInstance(app.getUrl()).checkProcess(app.getPid(),sid);
        call.enqueue(new Callback<CheckProcessEntity>() {
            @Override
            public void onResponse(Call<CheckProcessEntity> call, Response<CheckProcessEntity> response) {
                confirm_btn.setEnabled(true);
                confirm_btn.setText("确定");

                CheckProcessEntity checkProcessEntity = response.body();
                if(app.getMyDataChangeListener() != null)
                    app.getMyDataChangeListener().onSuccess();

                if(checkProcessEntity != null &&  checkProcessEntity.getStateCode() == 100){

                    if(checkProcessEntity.getNextProcessId() > 0){
                        if(process_view == null){
                            process_view = LayoutInflater.from(getContext()).inflate(R.layout.spinner,null);
                            rootView.addView(process_view);
                        }
                        process_view.setVisibility(View.VISIBLE);
                        TextView textView = process_view.findViewById(R.id.fieldName_tv);
                        process_sp = process_view.findViewById(R.id.state_sp);
                        textView.setText("选择下一终端");

                        for (ProccessEntity.ProcessListBean pe:app.getProccessEntity().getProcessList()) {
                            if(pe.getProcessId() == checkProcessEntity.getNextProcessId()){
                                process_data = initSpDataGx(String.valueOf(checkProcessEntity.getNextStateId()),pe.getProcessName(),pe.getProcessId(),pe.getTerminalList());
                                SimpleAdapter simpleAdapter = new SimpleAdapter(getContext(),process_data,R.layout.textview2,new String[]{"name"},new int[]{R.id.name_tv});
                                process_sp.setAdapter(simpleAdapter);
                            }

                        }
                    }else{
                        if(process_view != null){
                            process_view.setVisibility(View.GONE);
                        }
                    }

                }else if(checkProcessEntity != null){
                    if(process_view != null){
                        process_view.setVisibility(View.GONE);
                    }
                    Toast.makeText(getContext(),"获取下一工序错误"+checkProcessEntity.getReaSon(),Toast.LENGTH_SHORT).show();
                }else{
                    if(process_view != null){
                        process_view.setVisibility(View.GONE);
                    }
                    Toast.makeText(getContext(),"获取下一工序错误,服务器返回为空",Toast.LENGTH_SHORT).show();
                }
            }

            @Override
            public void onFailure(Call<CheckProcessEntity> call, Throwable t) {
                confirm_btn.setEnabled(true);
                confirm_btn.setText("确定");
                Log.i("sssddd-error","获取工序错误"+t.getMessage());
                if(app.getMyDataChangeListener() != null)
                    app.getMyDataChangeListener().onError(t);
            }
        });
    }

    private List<HashMap<String,String>> initSpDataGx(String nextStateId,String processName,int porcessId,List<ProccessEntity.ProcessListBean.TerminalListBean> terminalListBeans){
        List<HashMap<String,String>> data = new ArrayList<>();
        for (ProccessEntity.ProcessListBean.TerminalListBean terminal : terminalListBeans){
            HashMap<String,String> hashMap = new HashMap<>();
            hashMap.put("name",processName+" - "+terminal.getTerminalName());
            hashMap.put("pid",String.valueOf(porcessId));
            hashMap.put("tid", String.valueOf(terminal.getTerminalID()));
            hashMap.put("sid", String.valueOf(nextStateId));
            data.add(hashMap);
        }
        return data;
    }

    private List<HashMap<String,String>> initSpData(List<ProductLineDataEntity.StateBean> productLinesBeans){
        List<HashMap<String,String>> data = new ArrayList<>();
        for (ProductLineDataEntity.StateBean state : productLinesBeans){
            HashMap<String,String> hashMap = new HashMap<>();
            hashMap.put("name",state.getStateName());
            hashMap.put("sid",String.valueOf(state.getStateId()));
            data.add(hashMap);
        }
        return data;
    }

    public ProductLineDataEntity.LineDataBean getLineDataBean() {
        return lineDataBean;
    }

    public void setLineDataBean(ProductLineDataEntity.LineDataBean lineDataBean) {
        this.lineDataBean = lineDataBean;
    }
}
