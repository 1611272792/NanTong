package com.sunpn.productionplan;

import android.annotation.SuppressLint;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.graphics.Color;
import android.os.Handler;
import android.support.annotation.Nullable;
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

import com.chad.library.adapter.base.BaseQuickAdapter;
import com.chad.library.adapter.base.BaseViewHolder;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

import dialog.DownloadNewVersionDialog;
import dialog.EnterBaseInfoDialog;
import dialog.LoadDialog;
import entity.ProductLineDataEntity;
import entity.ProductLineInfoEntity;
import interfaces.MyDataChangeListener;
import service.BackgroundUpdataService;
import util.Util;
import view.AutoPlayRecyclerView;

public class MainActivity extends AppCompatActivity {
    private App app;
    private ImageView logo_iv;
    private TextView company_tv;
    private TextView terminal_tv;
    private AutoPlayRecyclerView data_rv;
    private MyRecycleAdapter data_sa;
    private ProductLineDataEntity data = new ProductLineDataEntity();
    private List<ProductLineDataEntity.ListBean> lineData = new ArrayList<ProductLineDataEntity.ListBean>();
    //    private ProductLineDataEntity.ListBean currentShowData = new ;
    private TextView msg_tv;
    private LinearLayout header_ll;
    private LoadDialog loadDialog;
    private EnterBaseInfoDialog enterBaseInfoDialog;
    private Spinner showCount_sp;
    private int itemHeight;
    private int itemShowCount;
    private int textSize;
    private ImageButton chang_ib;
    private Spinner textSize_sp;
    private DownloadNewVersionDialog downloadNewVersionDialog;
    private View state_view;

    private int currentIndex = 0;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        iniView();
        loadDialog.show();
        loadDialog.setMessage("加载中....");
        loadDialog.setCancelable(true);
        if(TextUtils.isEmpty(app.getUrl())){
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
                if(TextUtils.isEmpty(app.getUrl())){
                    loadDialog.dismiss();
                    enterBaseInfoDialog.show();
                }else{
                    //开启更新service
                    Intent intent = new Intent(MainActivity.this, BackgroundUpdataService.class);
                    startService(intent);
                }
            }
        });

        data_rv.setOnScrollCompleteListener(new AutoPlayRecyclerView.OnScrollCompleteListener() {
            @Override
            public void OnScrollComplete() {
                if(lineData == null || lineData.size() <= 0)
                    return;
                currentIndex ++;
                if(currentIndex > lineData.size()-1){
                    currentIndex = 0;
                }
                //设置数据
                data_sa = new MyRecycleAdapter(getListData());
                data_rv.setAdapter(data_sa);
                //设置标题
                terminal_tv.setText(lineData.get(currentIndex).getTerminalName());
                data_rv.setDelayTime(lineData.get(currentIndex).getPageTime() * 1000);
                msg_tv.setText(lineData.get(currentIndex).getTerinalInform());
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
                data = newData;
                lineData.clear();
                if(newData == null){
                    lineData.clear();
                }else{
                    lineData.addAll(newData.getList());
                }

                if(lineData.size() > 0){
                    if (data_sa != null)
                        data_sa.notifyDataSetChanged();
                    data_rv.startAutoPlay();
                }else{
                    data_rv.stopAutoPlay();
                }

                Toast.makeText(getApplicationContext(), "新数据", Toast.LENGTH_SHORT).show();
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
                data_sa = new MyRecycleAdapter(getListData());
                data_rv.setAdapter(data_sa);
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
                data_sa = new MyRecycleAdapter(getListData());
                data_rv.setAdapter(data_sa);

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

    private List<ProductLineDataEntity.ListBean.LineDataBean> getListData() {
        if(currentIndex < lineData.size())
            return lineData.get(currentIndex).getLineData();
        else
            return  new ArrayList<ProductLineDataEntity.ListBean.LineDataBean>();
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
        super.onBackPressed();
    }

    private void iniView() {
        app = (App) getApplication();
        state_view = findViewById(R.id.state_view);
        chang_ib=  findViewById(R.id.chang_ib);
        textSize_sp = findViewById(R.id.textSize_sp);
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
        msg_tv = findViewById(R.id.msg_tv);
        LinearLayoutManager linearLayoutManager = new LinearLayoutManager(this);
        linearLayoutManager.setOrientation(LinearLayoutManager.VERTICAL);
        data_rv.setLayoutManager(linearLayoutManager);
        data_sa = new MyRecycleAdapter(getListData());
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

    class MyRecycleAdapter extends BaseQuickAdapter<ProductLineDataEntity.ListBean.LineDataBean, MyRecycleAdapter.MyViewHolder> {
        private int couint = 0;
        private Handler handler = new Handler();
//        private DelayIniViewRun delayIniRun = new DelayIniViewRun();

        public MyRecycleAdapter(@Nullable List<ProductLineDataEntity.ListBean.LineDataBean> data) {
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
        protected void convert(MyViewHolder helper, ProductLineDataEntity.ListBean.LineDataBean item) {
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
        private void setData(ProductLineDataEntity.ListBean.LineDataBean item, HashMap<String, TextView> textViews) {
            for(String key: textViews.keySet() ){
                textViews.get(key).setTextSize(textSize);
                if(key.equals("ProductionPlanID")){
                    textViews.get(key).setText(item.getProductionPlanID());
                    textViews.get(key).setVisibility(View.GONE);
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
//                    textViews.get(key).setText(String.valueOf(item.getState()));
                    setState(textViews.get(key),item.getState());
                }else if(key.equals("TerminalID")){
                    textViews.get(key).setText(item.getTerminalID());
                    textViews.get(key).setVisibility(View.GONE);
                }else if(key.equals("c1")){
                    textViews.get(key).setText(item.getC1());
                }else if(key.equals("c2")){
                    textViews.get(key).setText(item.getC2());
                }else if(key.equals("c3")){
                    textViews.get(key).setText(item.getC3());
                }else if(key.equals("c4")){
                    textViews.get(key).setText(item.getC4());
                }else if(key.equals("c5")){
                    textViews.get(key).setText(item.getC5());
                }else if(key.equals("c6")){
                    textViews.get(key).setText(item.getC6());
                }else if(key.equals("c7")){
                    textViews.get(key).setText(item.getC7());
                }else if(key.equals("c8")){
                    textViews.get(key).setText(item.getC8());
                }else if(key.equals("c9")){
                    textViews.get(key).setText(item.getC9());
                }else if(key.equals("c10")){
                    textViews.get(key).setText(item.getC10());
                }else if(key.equals("c11")){
                    textViews.get(key).setText(item.getC11());
                }else if(key.equals("c12")){
                    textViews.get(key).setText(item.getC12());
                }else if(key.equals("c13")){
                    textViews.get(key).setText(item.getC13());
                }else if(key.equals("c14")){
                    textViews.get(key).setText(item.getC14());
                }else if(key.equals("c15")){
                    textViews.get(key).setText(item.getC15());
                }else if(key.equals("c16")){
                    textViews.get(key).setText(item.getC16());
                }else if(key.equals("c17")){
                    textViews.get(key).setText(item.getC17());
                }else if(key.equals("c18")){
                    textViews.get(key).setText(item.getC18());
                }else if(key.equals("c19")){
                    textViews.get(key).setText(item.getC19());
                }else if(key.equals("c20")){
                    textViews.get(key).setText(item.getC20());
                }
            }
        }

        private void setState(TextView textView, ProductLineDataEntity.ListBean.LineDataBean.StateBean state) {
            if(textView != null && state != null){
                textView.setText(state.getStateName());
                try {
                    textView.setTextColor(Color.parseColor(state.getStateColor()));
                }catch (Exception e){
                    e.printStackTrace();
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
            }
            public void clear(){
                for(String key:textViews.keySet()){
                    textViews.get(key).setText("");
                }
            }
        }
    }


//    class DelayIniViewRun implements Runnable {
//        private List<ProductLineDataEntity.LineDataBean> item;
//        private List<TextView> textViews;
//
//        public void setItem(List<ProductLineDataEntity.LineDataBean> item) {
//            this.item = item;
//        }
//
//        public void setTextViews(List<TextView> textViews) {
//            this.textViews = textViews;
//        }
//
//        @Override
//        public void run() {
//
//        }
//    }
}
