package com.sunpn.productionplan;

import android.annotation.SuppressLint;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.res.Configuration;
import android.graphics.Color;
import android.os.Handler;
import android.provider.Settings;
import android.support.annotation.Nullable;
import android.support.v4.content.ContextCompat;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.support.v7.widget.LinearLayoutManager;
import android.text.TextUtils;
import android.util.Log;
import android.view.Gravity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ImageButton;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import com.bumptech.glide.Glide;
import com.chad.library.adapter.base.BaseQuickAdapter;
import com.chad.library.adapter.base.BaseViewHolder;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

import dialog.AlterDataDialog;
import dialog.DownloadNewVersionDialog;
import dialog.EnterBaseInfoDialog;
import dialog.EnterPwdDialog;
import dialog.LoadDialog;
import entity.CheckVersionEntity;
import entity.CompanyInfoEntity;
import entity.GetPwdResultEntity;
import entity.MessageEntity;
import entity.ProductLineDataEntity;
import entity.ProductLineInfoEntity;
import interfaces.MyDataChangeListener;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import service.BackgroundUpdataService;
import util.HttpUtil;
import util.Util;
import view.AutoPlayRecyclerView;

public class MainActivity extends AppCompatActivity {
    private App app;
    private ImageView logo_iv;
    private TextView company_tv;
    private TextView terminal_tv;
    private AutoPlayRecyclerView data_rv;
    private MyRecycleAdapter data_sa;
    private List<ProductLineDataEntity.LineDataBean> lineData = new ArrayList<>();
    private TextView msg_tv;
    private LinearLayout header_ll;
    private LoadDialog loadDialog;
    private EnterBaseInfoDialog enterBaseInfoDialog;
    private Spinner showCount_sp;
    private int itemHeight;
    private int itemShowCount;
    private int textSize;
    private ImageButton chang_ib;
    private EnterPwdDialog enterPwdDialog;
    private AlterDataDialog alterDataDialog;
    private Spinner textSize_sp;
    private DownloadNewVersionDialog downloadNewVersionDialog;
    private View state_view;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        iniView();
        loadDialog.show();
        loadDialog.setMessage("加载中....");
        loadDialog.setCancelable(true);
        if(TextUtils.isEmpty(app.getUrl())  || app.getPid() == -1 || app.getTid() == -1){
            loadDialog.dismiss();
            enterBaseInfoDialog.show();
        }else{
            //开启更新service
            Intent intent = new Intent(MainActivity.this, BackgroundUpdataService.class);
            startService(intent);

        }
//        showCount_sp.setAdapter(new MyArrayAdapter(MainActivity.this,getResources().getStringArray(R.array.showCount)));
        enterBaseInfoDialog.setOnDismissListener(new DialogInterface.OnDismissListener() {
            @Override
            public void onDismiss(DialogInterface dialogInterface) {
                if(TextUtils.isEmpty(app.getUrl())  || app.getPid() == -1 || app.getTid() == -1){
                    loadDialog.dismiss();
                    enterBaseInfoDialog.show();
                }else{
                    //开启更新service
                    Intent intent = new Intent(MainActivity.this, BackgroundUpdataService.class);
                    startService(intent);
                }
            }
        });
        //全屏
        final View decorView = getWindow().getDecorView();
        decorView.setSystemUiVisibility(View.SYSTEM_UI_FLAG_HIDE_NAVIGATION | View.SYSTEM_UI_FLAG_IMMERSIVE_STICKY | View.SYSTEM_UI_FLAG_FULLSCREEN);
        getWindow().getDecorView().setOnSystemUiVisibilityChangeListener(new View.OnSystemUiVisibilityChangeListener() {
            @Override
            public void onSystemUiVisibilityChange(int i) {

                decorView.setSystemUiVisibility(View.SYSTEM_UI_FLAG_HIDE_NAVIGATION | View.SYSTEM_UI_FLAG_IMMERSIVE_STICKY | View.SYSTEM_UI_FLAG_FULLSCREEN);
            }
        });

        app.setMyDataChangeListener(new MyDataChangeListener() {
            @Override
            public void onNewData(ProductLineDataEntity newData) {
                if(loadDialog.isShowing())
                    loadDialog.dismiss();
                lineData.clear();
                if(newData != null){
                    lineData.addAll(newData.getLineData());
                }
                data_sa = new MyRecycleAdapter(lineData);
                data_rv.setAdapter(data_sa);
                if(lineData.size() > itemShowCount){
                    data_rv.startAutoPlay();
                }else{
                    data_rv.stopAutoPlay();
                }

                Toast.makeText(getApplicationContext(), "新数据", Toast.LENGTH_SHORT).show();
            }

            @Override
            public void onNewMessage(MessageEntity messageEntity) {
                if(loadDialog.isShowing())
                    loadDialog.dismiss();
                msg_tv.setText(messageEntity.getMessage().getContent());
                try {
                    msg_tv.setTextSize(Float.parseFloat(messageEntity.getMessage().getSize()));
                }catch (NumberFormatException e){
                    msg_tv.setTextSize(18f);
                }

                if(!TextUtils.isEmpty(messageEntity.getMessage().getColor())){
                    try {
                        msg_tv.setTextColor(Color.parseColor(messageEntity.getMessage().getColor()));
                    }catch (Exception e){
                        Toast.makeText(getApplicationContext(),"颜色代码错误",Toast.LENGTH_SHORT).show();
                        msg_tv.setTextColor(Color.RED);
                    }
                }
                Toast.makeText(getApplicationContext(), "新通知", Toast.LENGTH_SHORT).show();
            }

            @Override
            public void onNewProductionInfo(ProductLineInfoEntity productLineInfoEntity) {
                if(productLineInfoEntity.getLineInfo() == null){
                    if(enterBaseInfoDialog != null)
                        enterBaseInfoDialog.show();
                    return;
                }

                if(loadDialog.isShowing())
                    loadDialog.dismiss();
                //初始化表格表头
                header_ll.removeAllViews();
                for (ProductLineInfoEntity.LineInfoBean.FieldsBean fieldsBean : productLineInfoEntity.getLineInfo().getFields()) {
                    TextView textView = getTextView(fieldsBean.getFieldWidth());
                    textView.setTextSize(textSize);
                    textView.setText(fieldsBean.getFieldName());
                    textView.setTag(fieldsBean.getField());
                    header_ll.addView(textView);
                    if(fieldsBean.getField().equals("ProductionPlanID") || fieldsBean.getField().equals("TerminalID")){
                        textView.setVisibility(View.GONE);
                    }
                }
                //初始化终端名字
                terminal_tv.setText(productLineInfoEntity.getLineInfo().getName());
            }

            @Override
            public void onNewCompanyInfo(CompanyInfoEntity companyInfoEntity) {
                if(loadDialog.isShowing())
                    loadDialog.dismiss();
                Glide.with(getApplicationContext()).load(companyInfoEntity.getInfo().getLogoUrl()).error(R.mipmap.ic_launcher).placeholder(R.mipmap.ic_launcher_round).into(logo_iv);
                company_tv.setText(companyInfoEntity.getInfo().getCompanyName());
            }

            @Override
            public void onNewVersion(CheckVersionEntity checkVersionEntity) {
                if(downloadNewVersionDialog == null)
                    downloadNewVersionDialog = new DownloadNewVersionDialog(MainActivity.this);
                downloadNewVersionDialog.setUrl(checkVersionEntity.getDownloadUrl());
                downloadNewVersionDialog.show();
            }

            @Override
            public void onSuccess() {
                if(loadDialog.isShowing())
                    loadDialog.dismiss();
                state_view.setBackgroundResource(R.drawable.green_bg);
            }

            @Override
            public void onError(Throwable t) {
                t.printStackTrace();
                if(loadDialog.isShowing())
                    loadDialog.dismiss();
//                data.clear();
//                data_sa.notifyDataSetChanged();
//                //停止更新service
//                Intent intent = new Intent(MainActivity.this, BackgroundUpdataService.class);
//                stopService(intent);
//                enterBaseInfoDialog.show();
                state_view.setBackgroundResource(R.drawable.red_bg);
                Toast.makeText(getApplicationContext(), "错误" + t.getMessage(), Toast.LENGTH_SHORT).show();
            }
        });

        showCount_sp.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {

                itemShowCount = Integer.parseInt(getResources().getStringArray(R.array.showCount)[i]);
                Util.saveShowCountSpinnerSelectPosition(i,getApplicationContext());//保存显示条数
                int height = data_rv.getHeight()==0?data_rv.getMeasuredHeight():data_rv.getHeight();
                itemHeight = height/itemShowCount;
                data_sa = new MyRecycleAdapter(lineData);
                data_rv.setAdapter(data_sa);
                if(lineData.size() > itemShowCount){
                    data_rv.startAutoPlay();
                }else{
                    data_rv.stopAutoPlay();
                }
                Log.i("sssddd---",data_rv.getHeight()+"-"+data_rv.getMeasuredHeight());
            }

            @Override
            public void onNothingSelected(AdapterView<?> adapterView) {

            }
        });
        textSize_sp.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {
                textSize = Integer.parseInt(getResources().getStringArray(R.array.textSize)[i]);
                Util.saveTextSizeSpinnerSelectPosition(i,getApplicationContext());//保存显示条数
                data_sa = new MyRecycleAdapter(lineData);
                data_rv.setAdapter(data_sa);
                if(lineData.size() > itemShowCount){
                    data_rv.startAutoPlay();
                }else{
                    data_rv.stopAutoPlay();
                }


                header_ll.removeAllViews();
                ProductLineInfoEntity productLineInfoEntity = app.getProductLineInfoEntity();
                if(productLineInfoEntity == null)
                    return;
                for (ProductLineInfoEntity.LineInfoBean.FieldsBean fieldsBean : productLineInfoEntity.getLineInfo().getFields()) {
                    TextView textView = getTextView(fieldsBean.getFieldWidth());
                    textView.setTextSize(textSize);
                    textView.setText(fieldsBean.getFieldName());
                    textView.setTag(fieldsBean.getField());
                    header_ll.addView(textView);
                    if(fieldsBean.getField().equals("ProductionPlanID") || fieldsBean.getField().equals("TerminalID")){
                        textView.setVisibility(View.GONE);
                    }
                }
            }

            @Override
            public void onNothingSelected(AdapterView<?> adapterView) {

            }
        });

        enterPwdDialog.setOnDismissListener(new DialogInterface.OnDismissListener() {
            @Override
            public void onDismiss(DialogInterface dialogInterface) {
                if(enterPwdDialog.isTruePwd()){
                    alterDataDialog.setLineDataBean(enterPwdDialog.getLineDataBean());
                    alterDataDialog.show();
                    data_rv.startAutoPlay();
                }
            }
        });

        alterDataDialog.setOnDismissListener(new DialogInterface.OnDismissListener() {
            @Override
            public void onDismiss(DialogInterface dialogInterface) {
                data_rv.startAutoPlay();

            }
        });
        header_ll.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                data_rv.setSelected(false);
            }
        });
        chang_ib.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                //停止更新service
                Intent intent = new Intent(MainActivity.this, BackgroundUpdataService.class);
                stopService(intent);

                lineData.clear();
                data_sa.notifyDataSetChanged();
                enterBaseInfoDialog.show();
            }
        });
        enterBaseInfoDialog.setOnShowListener(new DialogInterface.OnShowListener() {
            @Override
            public void onShow(DialogInterface dialogInterface) {
                Intent intent = new Intent(MainActivity.this,BackgroundUpdataService.class);
                stopService(intent);
            }
        });
        enterBaseInfoDialog.setOnDismissListener(new DialogInterface.OnDismissListener() {
            @Override
            public void onDismiss(DialogInterface dialogInterface) {
                Intent intent = new Intent(MainActivity.this,BackgroundUpdataService.class);
                startService(intent);
            }
        });

    }

    @Override
    protected void onPause() {
        super.onPause();
        Log.i("sssddd","onPause");
    }

    @Override
    protected void onDestroy() {
        super.onDestroy();
        System.exit(0);
        Log.i("sssddd","onDestroy");
    }

    @Override
    protected void onResume() {
        super.onResume();
    }

    @Override
    public void onBackPressed() {
        if(alterDataDialog.isShowing()){
            alterDataDialog.dismiss();
        }else{
            super.onBackPressed();
        }
    }

    @Override
    public void onConfigurationChanged(Configuration newConfig) {
        super.onConfigurationChanged(newConfig);

    }

    private void iniView() {
        app = (App) getApplication();
        state_view = findViewById(R.id.state_view);
        chang_ib=  findViewById(R.id.chang_ib);
        textSize_sp = findViewById(R.id.textSize_sp);
        enterPwdDialog = new EnterPwdDialog(this);
        alterDataDialog = new AlterDataDialog(this);
        showCount_sp = findViewById(R.id.showCount_sp);
        showCount_sp.setSelection(Util.getShowCountSpinnerSelectPosition(getApplicationContext()));
        textSize_sp.setSelection(Util.getTextSizeSpinnerSelectPosition(getApplicationContext()));
        loadDialog = new LoadDialog(this);
        enterBaseInfoDialog = new EnterBaseInfoDialog(this);
        header_ll = findViewById(R.id.tableHeader_ll);
        logo_iv = findViewById(R.id.logo_iv);
        company_tv = findViewById(R.id.companyName_tv);
        terminal_tv = findViewById(R.id.terminalName_tv);
        data_rv = findViewById(R.id.data_rv);
        data_rv.setDelayTime(app.getPageTime() * 1000);
        msg_tv = findViewById(R.id.msg_tv);
        LinearLayoutManager linearLayoutManager = new LinearLayoutManager(this);
        linearLayoutManager.setOrientation(LinearLayoutManager.VERTICAL);
        data_rv.setLayoutManager(linearLayoutManager);
        data_sa = new MyRecycleAdapter(lineData);
        data_rv.setAdapter(data_sa);
    }

    private TextView getTextView(int weight) {
        TextView textView = (TextView) LayoutInflater.from(getApplicationContext()).inflate(R.layout.textview, null);
        LinearLayout.LayoutParams layoutParams = new LinearLayout.LayoutParams(0, ViewGroup.LayoutParams.MATCH_PARENT, weight);
        textView.setLayoutParams(layoutParams);
        textView.setGravity(Gravity.CENTER);
        textView.setFocusable(false);
        textView.setClickable(false);
        textView.setFocusableInTouchMode(false);
        return textView;
    }

    class MyRecycleAdapter extends BaseQuickAdapter<ProductLineDataEntity.LineDataBean, MyRecycleAdapter.MyViewHolder> {
        private int couint = 0;
        private Handler handler = new Handler();
        private DelayIniViewRun delayIniRun = new DelayIniViewRun();

        public MyRecycleAdapter(@Nullable List<ProductLineDataEntity.LineDataBean> data) {
            super(data);
        }

        @Override
        public MyViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
            Log.i("sssddd", "onCreateViewHolder");
            LinearLayout view = (LinearLayout) LayoutInflater.from(getApplicationContext()).inflate(R.layout.item_data, null);
            return new MyViewHolder(view);
        }

        @SuppressLint("ClickableViewAccessibility")
        @Override
        protected void convert(MyViewHolder helper, ProductLineDataEntity.LineDataBean item) {
            Log.i("sssddd", "convert");
            helper.clear();
//            服务端返回数据没有排序的情况下
            setData(item, helper.textViews);
            helper.rootView.setTag(item);
            if(helper.getAdapterPosition() %2 ==0){
                helper.rootView.setBackgroundResource(R.drawable.radius_bg4);
            }else {
                helper.rootView.setBackgroundResource(R.drawable.radius_bg5);
            }
//            //服务端返回数据已经排序的情况下
//            for(int i=0;i<item.size();i++){
//                if(i<helper.textViews.size())
//                helper.textViews.get(i).setText(item.get(i).getData());
//            }

        }
        private void setData(ProductLineDataEntity.LineDataBean item, HashMap<String, TextView> textViews) {
            for(String key: textViews.keySet() ){
                textViews.get(key).setTextSize(textSize);
                if(key.equals("ProductionPlanID")){
                    textViews.get(key).setText(item.getProductionPlanID());
                    textViews.get(key).setVisibility(View.GONE);
                }else if(key.equals("Customer")){
                    textViews.get(key).setText(item.getCustomer());
                }else if(key.equals("ProjectNo")){
                    textViews.get(key).setText(item.getProjectNo());
                }else if(key.equals("ProductionPlanName")){
                    textViews.get(key).setText(item.getProductionPlanName());
                }else if(key.equals("ProductionPlanVersion")){
                    textViews.get(key).setText(item.getProductionPlanVersion());
                }else if(key.equals("PlanNum")){
                    textViews.get(key).setText(item.getPlanNum());
                }else if(key.equals("RealNum")){
                    textViews.get(key).setText(item.getRealNum());
                }else if(key.equals("StartTime")){
                    textViews.get(key).setText(item.getStartTime());
                }else if(key.equals("EndTime")){
                    textViews.get(key).setText(item.getEndTime());
                }else if(key.equals("State")){
                    textViews.get(key).setText(item.getState());
//                    setState(textViews.get(key),item.getState());
                }else if(key.equals("TerminalID")){
                    textViews.get(key).setText(item.getTerminalID());
                    textViews.get(key).setVisibility(View.GONE);
                }else if(key.equals("c1")){
//                    textViews.get(key).setText(item.getC1());
                    setState(textViews.get(key),item.getC1());
                }else if(key.equals("c2")){
//                    textViews.get(key).setText(item.getC2());
                    setState(textViews.get(key),item.getC2());
                }else if(key.equals("c3")){
//                    textViews.get(key).setText(item.getC3());
                    setState(textViews.get(key),item.getC3());
                }else if(key.equals("c4")){
//                    textViews.get(key).setText(item.getC4());
                    setState(textViews.get(key),item.getC4());
                }else if(key.equals("c5")){
//                    textViews.get(key).setText(item.getC5());
                    setState(textViews.get(key),item.getC5());
                }else if(key.equals("c6")){
//                    textViews.get(key).setText(item.getC6());
                    setState(textViews.get(key),item.getC6());
                }else if(key.equals("c7")){
//                    textViews.get(key).setText(item.getC7());
                    setState(textViews.get(key),item.getC7());
                }else if(key.equals("c8")){
//                    textViews.get(key).setText(item.getC8());
                    setState(textViews.get(key),item.getC8());
                }else if(key.equals("c9")){
//                    textViews.get(key).setText(item.getC9());
                    setState(textViews.get(key),item.getC9());
                }else if(key.equals("c10")){
//                    textViews.get(key).setText(item.getC10());
                    setState(textViews.get(key),item.getC10());
                }else if(key.equals("c11")){
//                    textViews.get(key).setText(item.getC11());
                    setState(textViews.get(key),item.getC11());
                }else if(key.equals("c12")){
//                    textViews.get(key).setText(item.getC12());
                    setState(textViews.get(key),item.getC12());
                }else if(key.equals("c13")){
//                    textViews.get(key).setText(item.getC13());
                    setState(textViews.get(key),item.getC13());
                }else if(key.equals("c14")){
//                    textViews.get(key).setText(item.getC14());
                    setState(textViews.get(key),item.getC14());
                }else if(key.equals("c15")){
//                    textViews.get(key).setText(item.getC15());
                    setState(textViews.get(key),item.getC15());
                }else if(key.equals("c16")){
//                    textViews.get(key).setText(item.getC16());
                    setState(textViews.get(key),item.getC16());
                }else if(key.equals("c17")){
//                    textViews.get(key).setText(item.getC17());
                    setState(textViews.get(key),item.getC17());
                }else if(key.equals("c18")){
//                    textViews.get(key).setText(item.getC18());
                    setState(textViews.get(key),item.getC18());
                }else if(key.equals("c19")){
//                    textViews.get(key).setText(item.getC19());
                    setState(textViews.get(key),item.getC19());
                }else if(key.equals("c20")){
//                    textViews.get(key).setText(item.getC20());
                    setState(textViews.get(key),item.getC20());
                }else if(key.equals("c21")){
//                    textViews.get(key).setText(item.getC21());
                    setState(textViews.get(key),item.getC21());
                }else if(key.equals("c22")){
//                    textViews.get(key).setText(item.getC22());
                    setState(textViews.get(key),item.getC22());
                }else if(key.equals("c23")){
//                    textViews.get(key).setText(item.getC23());
                    setState(textViews.get(key),item.getC23());
                }else if(key.equals("c24")){
//                    textViews.get(key).setText(item.getC24());
                    setState(textViews.get(key),item.getC24());
                }else if(key.equals("c25")){
//                    textViews.get(key).setText(item.getC25());
                    setState(textViews.get(key),item.getC25());
                }else if(key.equals("c26")){
//                    textViews.get(key).setText(item.getC26());
                    setState(textViews.get(key),item.getC26());
                }else if(key.equals("c27")){
//                    textViews.get(key).setText(item.getC27());
                    setState(textViews.get(key),item.getC27());
                }else if(key.equals("c28")){
//                    textViews.get(key).setText(item.getC28());
                    setState(textViews.get(key),item.getC28());
                }else if(key.equals("c29")){
//                    textViews.get(key).setText(item.getC29());
                    setState(textViews.get(key),item.getC29());
                }else if(key.equals("c30")){
//                    textViews.get(key).setText(item.getC30());
                    setState(textViews.get(key),item.getC30());
                }else if(key.equals("c31")){
//                    textViews.get(key).setText(item.getC31());
                    setState(textViews.get(key),item.getC31());
                }else if(key.equals("c32")){
//                    textViews.get(key).setText(item.getC32());
                    setState(textViews.get(key),item.getC32());
                }else if(key.equals("c33")){
//                    textViews.get(key).setText(item.getC33());
                    setState(textViews.get(key),item.getC33());
                }else if(key.equals("c34")){
//                    textViews.get(key).setText(item.getC34());
                    setState(textViews.get(key),item.getC34());
                }else if(key.equals("c35")){
//                    textViews.get(key).setText(item.getC35());
                    setState(textViews.get(key),item.getC35());
                }else if(key.equals("c36")){
//                    textViews.get(key).setText(item.getC36());
                    setState(textViews.get(key),item.getC36());
                }else if(key.equals("c37")){
//                    textViews.get(key).setText(item.getC37());
                    setState(textViews.get(key),item.getC37());
                }else if(key.equals("c38")){
//                    textViews.get(key).setText(item.getC38());
                    setState(textViews.get(key),item.getC38());
                }else if(key.equals("c39")){
//                    textViews.get(key).setText(item.getC39());
                    setState(textViews.get(key),item.getC39());
                }else if(key.equals("c40")){
//                    textViews.get(key).setText(item.getC40());
                    setState(textViews.get(key),item.getC40());
                }else if(key.equals("c41")){
//                    textViews.get(key).setText(item.getC41());
                    setState(textViews.get(key),item.getC41());
                }else if(key.equals("c42")){
//                    textViews.get(key).setText(item.getC42());
                    setState(textViews.get(key),item.getC42());
                }else if(key.equals("c43")){
//                    textViews.get(key).setText(item.getC43());
                    setState(textViews.get(key),item.getC43());
                }else if(key.equals("c44")){
//                    textViews.get(key).setText(item.getC44());
                    setState(textViews.get(key),item.getC44());
                }else if(key.equals("c45")){
//                    textViews.get(key).setText(item.getC45());
                    setState(textViews.get(key),item.getC45());
                }else if(key.equals("c46")){
//                    textViews.get(key).setText(item.getC46());
                    setState(textViews.get(key),item.getC46());
                }else if(key.equals("c47")){
//                    textViews.get(key).setText(item.getC47());
                    setState(textViews.get(key),item.getC47());
                }else if(key.equals("c48")){
//                    textViews.get(key).setText(item.getC48());
                    setState(textViews.get(key),item.getC48());
                }else if(key.equals("c49")){
//                    textViews.get(key).setText(item.getC49());
                    setState(textViews.get(key),item.getC49());
                }else if(key.equals("c50")){
//                    textViews.get(key).setText(item.getC50());
                    setState(textViews.get(key),item.getC50());
                }
            }
        }

        private void setState(TextView textView, String state) {
            if(textView != null && state != null && !state.isEmpty()){
                String[] strings = state.split("&");
                if(strings.length>0){
                    textView.setText(strings[0]);
                    if(strings.length>1){
                        try {
                            textView.setTextColor(Color.parseColor(strings[1]));
                        }catch (Exception e){
                            e.printStackTrace();
                        }
                    }
                }
            }
        }

        class MyViewHolder extends BaseViewHolder {
            private HashMap<String, TextView> textViews = new HashMap<>();
            private LinearLayout rootView;
            public MyViewHolder(View view) {
                super(view);
                int height = data_rv.getHeight()==0?data_rv.getMeasuredHeight():data_rv.getHeight();
                itemHeight = height/itemShowCount;
                rootView = (LinearLayout) view;
                if(itemHeight == 0){
                    rootView.setLayoutParams(new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MATCH_PARENT, ViewGroup.LayoutParams.MATCH_PARENT));
                }else{
                    rootView.setLayoutParams(new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MATCH_PARENT, itemHeight));
                }
                ProductLineInfoEntity productLineInfoEntity = app.getProductLineInfoEntity();
                if (productLineInfoEntity == null) {
                    return;
                }
                List<ProductLineInfoEntity.LineInfoBean.FieldsBean> dd = productLineInfoEntity.getLineInfo().getFields();
                for (ProductLineInfoEntity.LineInfoBean.FieldsBean fieldsBean : dd) {
                    TextView textView = getTextView(fieldsBean.getFieldWidth());
                    rootView.addView(textView);
                    textViews.put(fieldsBean.getField(), textView);
                }

//                rootView.setOnLongClickListener(new View.OnLongClickListener() {
//                    @Override
//                    public boolean onLongClick(final View view) {
//                        data_rv.stopAutoPlay();
//                        String androidID = Settings.Secure.getString(getApplicationContext().getContentResolver(), Settings.Secure.ANDROID_ID);
//                        Call<GetPwdResultEntity> call = HttpUtil.getInstance(app.getUrl()).getPwd(app.getPid(),androidID);
//                        call.enqueue(new Callback<GetPwdResultEntity>() {
//                            @Override
//                            public void onResponse(Call<GetPwdResultEntity> call, Response<GetPwdResultEntity> response) {
//                                GetPwdResultEntity getPwdResultEntity = response.body();
//                                if(getPwdResultEntity != null){
//                                    if(getPwdResultEntity.getStateCode() == 100){
//                                        if(TextUtils.isEmpty(getPwdResultEntity.getPwd())){
//                                            alterDataDialog.setLineDataBean((ProductLineDataEntity.LineDataBean)view.getTag());
//                                            alterDataDialog.show();
//                                        }else{
//                                            enterPwdDialog.setLineDataBeans((ProductLineDataEntity.LineDataBean)view.getTag());
//                                            enterPwdDialog.setPwd(getPwdResultEntity.getPwd());
//                                            enterPwdDialog.show();
//                                        }
//
//                                    }else{
//                                        Toast.makeText(MainActivity.this,getPwdResultEntity.getReason(),Toast.LENGTH_SHORT).show();
//                                    }
//                                }
//                            }
//
//                            @Override
//                            public void onFailure(Call<GetPwdResultEntity> call, Throwable t) {
//                                Toast.makeText(MainActivity.this,"发送获取密码请求失败",Toast.LENGTH_SHORT).show();
//                            }
//                        });
//                        return true;
//                    }
//                });
            }
            public void clear(){
                for(String key:textViews.keySet()){
                    textViews.get(key).setText("");
                }
            }
        }
    }


    class DelayIniViewRun implements Runnable {
        private List<ProductLineDataEntity.LineDataBean> item;
        private List<TextView> textViews;

        public void setItem(List<ProductLineDataEntity.LineDataBean> item) {
            this.item = item;
        }

        public void setTextViews(List<TextView> textViews) {
            this.textViews = textViews;
        }

        @Override
        public void run() {

        }
    }
    private class MyArrayAdapter extends ArrayAdapter<String> {
        private Context mContext;
        private String [] mStringArray;
        public MyArrayAdapter(Context context, String[] stringArray) {
            super(context, android.R.layout.simple_spinner_item, stringArray);
            mContext = context;
            mStringArray=stringArray;
        }

        @Override
        public View getDropDownView(int position, View convertView, ViewGroup parent) {
//            //修改Spinner展开后的字体颜色
            if (convertView == null) {
                LayoutInflater inflater = LayoutInflater.from(mContext);
                convertView = inflater.inflate(android.R.layout.simple_spinner_dropdown_item, parent,false);
//                convertView.setBackgroundColor(Color.BLACK);
            }

            //此处text1是Spinner默认的用来显示文字的TextView
            TextView tv = (TextView) convertView.findViewById(android.R.id.text1);
            tv.setGravity(Gravity.CENTER);
            tv.setText(mStringArray[position]);
            tv.setTextColor(Color.WHITE);

            return convertView;

        }

        @Override
        public View getView(int position, View convertView, ViewGroup parent) {
            // 修改Spinner选择后结果的字体颜色
            if (convertView == null) {
                LayoutInflater inflater = LayoutInflater.from(mContext);
                convertView = inflater.inflate(android.R.layout.simple_spinner_item, parent, false);
            }
//
            //此处text1是Spinner默认的用来显示文字的TextView
            TextView tv = (TextView) convertView.findViewById(android.R.id.text1);
            tv.setGravity(Gravity.CENTER);
            tv.setText(mStringArray[position]);
            tv.setTextColor(Color.WHITE);
            return convertView;
        }

    }
}
