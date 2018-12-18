package view;

import android.annotation.SuppressLint;
import android.content.Context;
import android.graphics.Color;
import android.graphics.Rect;
import android.os.Handler;
import android.support.annotation.Nullable;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.support.v7.widget.RecyclerView.OnScrollListener;
import android.util.AttributeSet;
import android.util.Log;
import android.view.KeyEvent;
import android.view.MotionEvent;
import android.view.View;
import android.widget.Toast;

import com.chad.library.adapter.base.BaseQuickAdapter;
import com.sunpn.productionplan.R;

/**
 * Created by guhh on 2018/3/22.
 */


public class AutoPlayRecyclerView extends RecyclerView{
    private final int KEYCODE_DOWN = 20;
    private final int KEYCODE_UP = 19;
    private final int KEYCODE_RIGHT = 22;
    private final int KEYCODE_LEFT = 21;
    private final int KEYCODE_ENTER = 23;
    private final int KEYCODE_DEL = 67;
    private int autoPlayPosition = 0;
    private long delayTime = 2000;
    private boolean isPlay = false;
    private OnScrollCompleteListener onScrollCompleteListener;

    private Handler autoPlay_handler = new Handler();
    private Runnable autoPlay_run = new Runnable() {
        @Override
        public void run() {
            ((LinearLayoutManager)getLayoutManager()).scrollToPositionWithOffset(autoPlayPosition,0);
            autoPlayPosition++;
            if(autoPlayPosition >(getAdapter().getItemCount() - getItemShowCount()) ){
                autoPlayPosition = 0;
                if(onScrollCompleteListener != null)
                    onScrollCompleteListener.OnScrollComplete();
            }
            Log.i("sssddd-ssss",autoPlayPosition+"**");
            autoPlay_handler.postDelayed(autoPlay_run,delayTime);
        }
    };

    public AutoPlayRecyclerView(Context context) {
        super(context);
        addOnScrollListener(new OnScrollListener() {
            @Override
            public void onScrollStateChanged(RecyclerView recyclerView, int newState) {
                super.onScrollStateChanged(recyclerView, newState);
                Log.i("sssddd",newState+"--");
                if(newState == 0){
                        autoPlayPosition = ((LinearLayoutManager)recyclerView.getLayoutManager()).findFirstVisibleItemPosition();
                }
            }

            @Override
            public void onScrolled(RecyclerView recyclerView, int dx, int dy) {
                super.onScrolled(recyclerView, dx, dy);
            }
        });
    }

//    @Override
//    protected void onFocusChanged(boolean gainFocus, int direction, @Nullable Rect previouslyFocusedRect) {
//        super.onFocusChanged(gainFocus, direction, previouslyFocusedRect);
//        if(gainFocus){
//            startAutoPlay();
//        }else{
//            stopAutoPlay();
//        }
//    }

    public AutoPlayRecyclerView(Context context, @Nullable AttributeSet attrs) {
        super(context, attrs);
        addOnScrollListener(new OnScrollListener() {
            @Override
            public void onScrollStateChanged(RecyclerView recyclerView, int newState) {
                super.onScrollStateChanged(recyclerView, newState);
                if(newState == 0){
                    autoPlayPosition = ((LinearLayoutManager)recyclerView.getLayoutManager()).findFirstVisibleItemPosition();
                    startAutoPlay();
                }else{
                    stopTempAutoPlay();
                }
            }

            @Override
            public void onScrolled(RecyclerView recyclerView, int dx, int dy) {
                super.onScrolled(recyclerView, dx, dy);
            }
        });
    }

    @Override
    public void setAdapter(Adapter adapter) {
        autoPlayPosition = 1;
        super.setAdapter(adapter);
        clearFocus();
        ((BaseQuickAdapter)adapter).setOnItemClickListener(new BaseQuickAdapter.OnItemClickListener() {
            @Override
            public void onItemClick(BaseQuickAdapter adapter, View view, int position) {
                Log.i("sssddd","setOnItemClickListener");
            }
        });
    }

//    @Override
//    public boolean onTouchEvent(MotionEvent e) {
//
//        switch (e.getAction()){
//            case MotionEvent.ACTION_DOWN:
//                stopTempAutoPlay();
//                return false;
//            case MotionEvent.ACTION_UP:
//                if(isPlay)
//                    startAutoPlay();
//                performClick();
//                break;
//            case MotionEvent.ACTION_MOVE:
//                stopTempAutoPlay();
//                break;
//            case MotionEvent.ACTION_CANCEL:
//                startAutoPlay();
//                break;
//        }
//        return super.onTouchEvent(e);
//    }

//    @Override
//    public boolean onKeyDown(int keyCode, KeyEvent event) {
//        switch (keyCode){
//            case KEYCODE_DOWN:
//                Toast.makeText(getContext(),"KEYCODE_DOWN",Toast.LENGTH_SHORT).show();
//                ((BaseQuickAdapter) getAdapter()).getViewByPosition(0, R.id.rootView).setBackgroundColor(Color.RED);
//                break;
//            case KEYCODE_UP:
//
//                break;
//        }
//        return super.onKeyDown(keyCode, event);
//    }

    @Override
    public boolean performClick() {
        return super.performClick();
    }

    public void startAutoPlay(){
        Log.i("sssddd-ssss","startAutoPlay");
        //开启自动滚动
        isPlay = true;
        autoPlay_handler.removeCallbacks(autoPlay_run);
        autoPlay_handler.postDelayed(autoPlay_run,delayTime);
    }

    private int getItemShowCount(){
        LinearLayoutManager linearLayoutManager = ((LinearLayoutManager)getLayoutManager());
        return linearLayoutManager.findLastVisibleItemPosition() - linearLayoutManager.findFirstVisibleItemPosition();
    }

    public void stopAutoPlay(){
        Log.i("sssddd-ssss","stopAutoPlay");
        isPlay = false;
        autoPlay_handler.removeCallbacks(autoPlay_run);
    }

    public void stopTempAutoPlay(){
        Log.i("sssddd-ssss","stopAutoPlay");
        autoPlay_handler.removeCallbacks(autoPlay_run);
    }

    public long getDelayTime() {
        return delayTime;
    }

    public void setDelayTime(long delayTime) {
        this.delayTime = delayTime;
    }

    public OnScrollCompleteListener getOnScrollCompleteListener() {
        return onScrollCompleteListener;
    }

    public void setOnScrollCompleteListener(OnScrollCompleteListener onScrollCompleteListener) {
        this.onScrollCompleteListener = onScrollCompleteListener;
    }

    public interface OnScrollCompleteListener{
        public abstract void OnScrollComplete();
    }
}
