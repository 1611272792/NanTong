package util;

import android.content.Context;
import android.content.SharedPreferences;

import java.util.HashMap;

/**
 * Created by guhh on 2018/3/22.
 */

public class Util {
    public static void saveData(String tid,String pid,String pageTime, String url, Context context){
        SharedPreferences sharedPreferences = context.getSharedPreferences("data",Context.MODE_PRIVATE);
        SharedPreferences.Editor editor = sharedPreferences.edit();
        editor.putString("tid",tid);
        editor.putString("pid",pid);
        editor.putString("pageTime",pageTime);
        editor.putString("url",url);
        editor.apply();
    }



    public static HashMap<String,String> getData(Context context){
        HashMap<String,String> hashMap = new HashMap<>();
        SharedPreferences sharedPreferences = context.getSharedPreferences("data",Context.MODE_PRIVATE);
        hashMap.put("tid",sharedPreferences.getString("tid","-1"));
        hashMap.put("pid",sharedPreferences.getString("pid","-1"));
        hashMap.put("pageTime",sharedPreferences.getString("pageTime","2"));
        hashMap.put("url",sharedPreferences.getString("url",""));
        return hashMap;
    }

    public static void saveShowCountSpinnerSelectPosition(int showCountSpinnerSelectPosition, Context context){
        SharedPreferences sharedPreferences = context.getSharedPreferences("data",Context.MODE_PRIVATE);
        SharedPreferences.Editor editor = sharedPreferences.edit();
        editor.putInt("showCount",showCountSpinnerSelectPosition);
        editor.apply();
    }
    public static int getShowCountSpinnerSelectPosition(Context context){
        SharedPreferences sharedPreferences = context.getSharedPreferences("data",Context.MODE_PRIVATE);
        return sharedPreferences.getInt("showCount",5);
    }

    public static void saveTextSizeSpinnerSelectPosition(int showCountSpinnerSelectPosition, Context context){
        SharedPreferences sharedPreferences = context.getSharedPreferences("data",Context.MODE_PRIVATE);
        SharedPreferences.Editor editor = sharedPreferences.edit();
        editor.putInt("textSize",showCountSpinnerSelectPosition);
        editor.apply();
    }
    public static int getTextSizeSpinnerSelectPosition(Context context){
        SharedPreferences sharedPreferences = context.getSharedPreferences("data",Context.MODE_PRIVATE);
        return sharedPreferences.getInt("textSize",2);
    }

}
