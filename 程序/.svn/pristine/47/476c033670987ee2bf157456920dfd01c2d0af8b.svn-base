package com.sunpn.productionplan;

import android.app.Application;

import com.liulishuo.filedownloader.FileDownloader;

import java.util.HashMap;
import java.util.List;

import cn.dreamtobe.filedownloader.OkHttp3Connection;
import entity.CheckVersionEntity;
import entity.CompanyInfoEntity;
import entity.MessageEntity;
import entity.ProccessEntity;
import entity.ProductLineDataEntity;
import entity.ProductLineInfoEntity;
import interfaces.MyDataChangeListener;
import util.Util;

/**
 * Created by guhh on 2018/3/21.
 */

public class App extends Application {
    private MyDataChangeListener myDataChangeListener;
private ProductLineDataEntity productLineDataEntity;
private ProductLineInfoEntity productLineInfoEntity;
private MessageEntity messageEntity;
private CompanyInfoEntity companyInfoEntity;
private CheckVersionEntity checkVersionEntity;
private ProccessEntity proccessEntity;
    private String url;

    public MyDataChangeListener getMyDataChangeListener() {
        return myDataChangeListener;
    }

    public CompanyInfoEntity getCompanyInfoEntity() {
        return companyInfoEntity;
    }

    public ProccessEntity getProccessEntity() {
        return proccessEntity;
    }


    public void setProccessEntity(ProccessEntity proccessEntity) {
        this.proccessEntity = proccessEntity;
    }

    public void setCompanyInfoEntity(CompanyInfoEntity companyInfoEntity) {
        this.companyInfoEntity = companyInfoEntity;
    }

    public void setMyDataChangeListener(MyDataChangeListener myDataChangeListener) {
        this.myDataChangeListener = myDataChangeListener;
    }

    public ProductLineDataEntity getProductLineDataEntity() {
        return productLineDataEntity;
    }

    public void setProductLineDataEntity(ProductLineDataEntity productLineDataEntity) {
        this.productLineDataEntity = productLineDataEntity;
    }

    public ProductLineInfoEntity getProductLineInfoEntity() {
        return productLineInfoEntity;
    }

    public void setProductLineInfoEntity(ProductLineInfoEntity productLineInfoEntity) {
        this.productLineInfoEntity = productLineInfoEntity;
    }

    public MessageEntity getMessageEntity() {
        return messageEntity;
    }

    public void setMessageEntity(MessageEntity messageEntity) {
        this.messageEntity = messageEntity;
    }

    public CheckVersionEntity getCheckVersionEntity() {
        return checkVersionEntity;
    }

    public void setCheckVersionEntity(CheckVersionEntity checkVersionEntity) {
        this.checkVersionEntity = checkVersionEntity;
    }


    @Override
    public void onCreate() {
        super.onCreate();
        HashMap<String,String> h = Util.getData(getApplicationContext());
        url = h.get("url");
        FileDownloader.setupOnApplicationOnCreate(this).connectionCreator(new OkHttp3Connection.Creator());
    }



    public void clear(){
        myDataChangeListener = null;
        url = "";
        productLineDataEntity = null;
        productLineInfoEntity = null;
        messageEntity = null;
        companyInfoEntity = null;
        checkVersionEntity = null;
    }

    public String getUrl() {
        return url;
    }

    public void setUrl(String url ){
        this.url = url;
    }
}
