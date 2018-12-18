package service;

import android.app.Service;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageInfo;
import android.content.pm.PackageManager;
import android.os.Handler;
import android.os.IBinder;
import android.provider.Settings;
import android.support.annotation.Nullable;
import android.support.design.widget.BaseTransientBottomBar;
import android.support.design.widget.Snackbar;
import android.util.Log;
import android.widget.TextView;
import android.widget.Toast;

import com.bumptech.glide.Glide;
import com.sunpn.productionplan.App;
import com.sunpn.productionplan.MainActivity;
import com.sunpn.productionplan.R;

import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.TimeUnit;

import entity.CheckVersionEntity;
import entity.CompanyInfoEntity;
import entity.MessageEntity;
import entity.ProccessEntity;
import entity.ProductLineDataEntity;
import entity.ProductLineInfoEntity;
import interfaces.IApi;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import util.HttpUtil;

/**
 * Created by guhh on 2018/3/21.
 */

public class BackgroundUpdataService extends Service {
    private App app;
    private IApi iApi;
    private String androidID;
    private ScheduledExecutorService singThreadPool = Executors.newSingleThreadScheduledExecutor();
    private Runnable getDataRun = new Runnable() {
        @Override
        public void run() {
            try {
                iApi = HttpUtil.getInstance(app.getUrl());
                //获取通知
                Call<MessageEntity> call_info = iApi.getMessage(app.getTid(),androidID);
                call_info.enqueue(new Callback<MessageEntity>() {
                    @Override
                    public void onResponse(Call<MessageEntity> call, Response<MessageEntity> response) {
                        MessageEntity messageEntity = response.body();
                        if(app.getMyDataChangeListener() != null)
                            app.getMyDataChangeListener().onSuccess();

                        if(messageEntity != null &&  messageEntity.getStateCode() == 100 &&  !messageEntity.equals(app.getMessageEntity())){
                            app.setMessageEntity(messageEntity);
                            if(app.getMyDataChangeListener() !=null)
                                app.getMyDataChangeListener().onNewMessage(messageEntity);
                        }else if(messageEntity != null && messageEntity.getStateCode() == 104){
                            if(app.getMyDataChangeListener() !=null)
                                app.getMyDataChangeListener().onError(new Throwable(messageEntity.getReason()));
                        }
                    }

                    @Override
                    public void onFailure(Call<MessageEntity> call, Throwable t) {
                        t.printStackTrace();
                        Log.i("sssddd-error","获取通知"+t.getMessage());
                        if(app.getMyDataChangeListener() !=null)
                            app.getMyDataChangeListener().onError(t);
                    }
                });

                if(app.getProductLineInfoEntity() == null){
                    //获取产线基本信息
                    Call<ProductLineInfoEntity> call_productInfo = iApi.getProductLineInfo(app.getTid(),androidID);
                    call_productInfo.enqueue(new Callback<ProductLineInfoEntity>() {
                        @Override
                        public void onResponse(Call<ProductLineInfoEntity> call, Response<ProductLineInfoEntity> response) {
                            ProductLineInfoEntity productLineInfoEntity = response.body();
                            if(productLineInfoEntity != null  && productLineInfoEntity.getStateCode() == 100){
                                //表格内容初始化
                                app.setProductLineInfoEntity(productLineInfoEntity);
                                if(app.getMyDataChangeListener() != null){
                                    app.getMyDataChangeListener().onNewProductionInfo(productLineInfoEntity);
                                }
                            }else if(productLineInfoEntity != null && productLineInfoEntity.getStateCode() == 104){
                                if(app.getMyDataChangeListener() !=null)
                                    app.getMyDataChangeListener().onError(new Throwable(productLineInfoEntity.getReason()));
                            }
                        }

                        @Override
                        public void onFailure(Call<ProductLineInfoEntity> call, Throwable t) {
                            t.printStackTrace();
                            Log.i("sssddd-error","获取产线基本信息"+t.getMessage());
                            if(app.getMyDataChangeListener() != null)
                                app.getMyDataChangeListener().onError(t);
                        }
                    });
                }else if(app.getProccessEntity() == null){
                    //获取全部工序
                    Call<ProccessEntity> call_product = iApi.getAllProcess();
                    call_product.enqueue(new Callback<ProccessEntity>() {
                        @Override
                        public void onResponse(Call<ProccessEntity> call, Response<ProccessEntity> response) {
                            ProccessEntity productEntity = response.body();
                            if(productEntity != null && productEntity.getStateCode() == 100){
                                app.setProccessEntity(productEntity);
                            }
                        }

                        @Override
                        public void onFailure(Call<ProccessEntity> call, Throwable t) {
                            t.printStackTrace();
                            Log.i("sssddd-error","获取所有工序"+t.getMessage());
                            if(app.getMyDataChangeListener() != null)
                                app.getMyDataChangeListener().onError(t);
                        }
                    });
                }else{
                    //获取产线数据
                    Call<ProductLineDataEntity> call = iApi.getProductLineData(app.getTid(),androidID);
                    call.enqueue(new Callback<ProductLineDataEntity>() {
                        @Override
                        public void onResponse(Call<ProductLineDataEntity> call, Response<ProductLineDataEntity> response) {
                            ProductLineDataEntity productLineDataEntity = response.body();
                            if(app.getMyDataChangeListener() != null)
                                app.getMyDataChangeListener().onSuccess();

                            if(productLineDataEntity != null &&  productLineDataEntity.getStateCode() == 100 && !productLineDataEntity.equals(app.getProductLineDataEntity())){
                                app.setProductLineDataEntity(productLineDataEntity);
                                if(app.getMyDataChangeListener() != null)
                                    app.getMyDataChangeListener().onNewData(productLineDataEntity);
                            }else if(productLineDataEntity != null && productLineDataEntity.getStateCode() == 104){
                                if(app.getMyDataChangeListener() != null)
                                    app.getMyDataChangeListener().onError(new Throwable(productLineDataEntity.getReaSon()));
                            }
                        }

                        @Override
                        public void onFailure(Call<ProductLineDataEntity> call, Throwable t) {
                            Log.i("sssddd-error","获取产线数据"+t.getMessage());
                            if(app.getMyDataChangeListener() != null)
                                app.getMyDataChangeListener().onError(t);
                        }
                    });
                }

                if(app.getCompanyInfoEntity() == null){
                    //获取公司基本信息
                    Call<CompanyInfoEntity> call_companyInfo = iApi.getCompanyInfo(app.getTid(),androidID);
                    call_companyInfo.enqueue(new Callback<CompanyInfoEntity>() {
                        @Override
                        public void onResponse(Call<CompanyInfoEntity> call, Response<CompanyInfoEntity> response) {
                            CompanyInfoEntity companyInfoEntity = response.body();
                            if (companyInfoEntity != null &&  companyInfoEntity.getStateCode() == 100) {
                                app.setCompanyInfoEntity(companyInfoEntity);
                                if(app.getMyDataChangeListener() != null)
                                    app.getMyDataChangeListener().onNewCompanyInfo(companyInfoEntity);
                            }else if(companyInfoEntity != null && companyInfoEntity.getStateCode() == 104){
                                if(app.getMyDataChangeListener() != null)
                                    app.getMyDataChangeListener().onError(new Throwable(companyInfoEntity.getReason()));
                            }

                        }

                        @Override
                        public void onFailure(Call<CompanyInfoEntity> call, Throwable t) {
                            t.printStackTrace();
                            Log.i("sssddd-error","获取公司基本信息"+t.getMessage());
                            if(app.getMyDataChangeListener() != null)
                                app.getMyDataChangeListener().onError(t);
                        }
                    });
                }

                if(app.getCheckVersionEntity() == null){
                    //检查更新
                    int code = 0;
                    PackageInfo info = null;
                    try {
                        info = getPackageManager().getPackageInfo(getBaseContext().getPackageName(), 0);
                    } catch (PackageManager.NameNotFoundException e) {
                        e.printStackTrace();
                    }
                    code = info.versionCode;
                    Call<CheckVersionEntity> call_checkVersion = iApi.checkVersion(0,code);
                    call_checkVersion.enqueue(new Callback<CheckVersionEntity>() {
                        @Override
                        public void onResponse(Call<CheckVersionEntity> call, Response<CheckVersionEntity> response) {
                            CheckVersionEntity checkVersionEntity = response.body();
                            if(checkVersionEntity != null ){
                                if(checkVersionEntity.getStateCode() == 100){
                                    app.setCheckVersionEntity(checkVersionEntity);
                                    if(checkVersionEntity.isNewVersion() && app.getMyDataChangeListener() != null)
                                        app.getMyDataChangeListener().onNewVersion(checkVersionEntity);
                                }else{
                                    if(app.getMyDataChangeListener() != null)
                                        app.getMyDataChangeListener().onError(new Throwable(checkVersionEntity.getReason()));
                                }

                            }else{
                                if(app.getMyDataChangeListener() != null)
                                    app.getMyDataChangeListener().onError(new Throwable("服务器返回空"));
                            }
                        }

                        @Override
                        public void onFailure(Call<CheckVersionEntity> call, Throwable t) {
                            if(app.getMyDataChangeListener() != null)
                                app.getMyDataChangeListener().onError(t);
                            Log.i("sssddd-error","检查更新"+t.getMessage());
                        }
                    });
                }
            }catch (Exception e){
                if(app.getMyDataChangeListener() != null)
                    app.getMyDataChangeListener().onError(e);
                Log.i("sssddd",e.toString());
            }
        }
    };
    @Nullable
    @Override
    public IBinder onBind(Intent intent) {
        return null;
    }

    @Override
    public void onCreate() {
        super.onCreate();
        app = (App) getApplication();
        singThreadPool.scheduleAtFixedRate(getDataRun,0,3000, TimeUnit.MILLISECONDS);
        androidID = Settings.Secure.getString(getApplicationContext().getContentResolver(), Settings.Secure.ANDROID_ID);
    }

    @Override
    public void onDestroy() {
        super.onDestroy();
        singThreadPool.shutdownNow();
    }
}
