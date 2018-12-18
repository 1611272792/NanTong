package util;

import android.app.Application;
import android.content.Context;
import android.text.TextUtils;
import android.util.Log;

import com.sunpn.productionplan.App;

import java.util.concurrent.TimeUnit;

import interfaces.IApi;
import okhttp3.OkHttpClient;
import okhttp3.logging.HttpLoggingInterceptor;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

/**
 * Created by guhh on 2018/2/27.
 */

public class HttpUtil {
    private static IApi iApi;
//    public static void get
    public static IApi getInstance(String url){

            //初始化api
            OkHttpClient okHttpClient = new OkHttpClient.Builder()
                    .readTimeout(3, TimeUnit.SECONDS)//设置读取超时时间
                    .writeTimeout(3, TimeUnit.SECONDS)//设置写的超时时间
                    .connectTimeout(3, TimeUnit.SECONDS)//设置连接超时时间
                    .addInterceptor(new HttpLoggingInterceptor().setLevel(HttpLoggingInterceptor.Level.BODY)).build();
            Retrofit retrofit = new Retrofit.Builder()
                    .baseUrl("http://"+url+"/api/")
//                    .baseUrl("http://"+app.getIp()+":"+app.getPort()+"/ProductionPlanServer/")
                    .addConverterFactory(GsonConverterFactory.create())
                    .client(okHttpClient)
                    .build();
        iApi = retrofit.create(IApi.class);
        return iApi;
    }
}
