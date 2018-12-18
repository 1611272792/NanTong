package dialog;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.Context;
import android.content.Intent;
import android.icu.text.UnicodeSet;
import android.net.Uri;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.ProgressBar;
import android.widget.TextView;
import android.widget.Toast;

import com.liulishuo.filedownloader.BaseDownloadTask;
import com.liulishuo.filedownloader.FileDownloadLargeFileListener;
import com.liulishuo.filedownloader.FileDownloader;
import com.sunpn.productionplan.R;

import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.File;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.UnsupportedEncodingException;

/**
 * Created by guhh on 2018/3/23.
 */
public class DownloadNewVersionDialog extends AlertDialog {
    private Activity activity;
    private ProgressBar progressBar;
    private TextView status_tv ;
    private String url ;
    private String downPath;
    private Button retry_btn;
    private Button cancel_btn;
    public DownloadNewVersionDialog(Context context) {
        super(context);
        setCancelable(false);
        activity = (Activity) context;
    }

    protected DownloadNewVersionDialog(Context context, boolean cancelable, OnCancelListener cancelListener) {
        super(context, cancelable, cancelListener);
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.dialog_download);
        progressBar = findViewById(R.id.loading_progress);
        progressBar.setMax(100);
        status_tv = findViewById(R.id.status_tv);
        downPath = getContext().getExternalCacheDir().getPath()+(url.substring(url.lastIndexOf("/")));
        retry_btn = findViewById(R.id.retry_btn);
        cancel_btn = findViewById(R.id.cancel_btn);

        FileDownloader.getImpl().create(url)
                .setForceReDownload(true)
                .setPath(downPath)
                .setListener(new FileDownloadLargeFileListener() {
                    @Override
                    protected void pending(BaseDownloadTask task, long soFarBytes, long totalBytes) {
                        Log.i("sssddd-download","pending");
                        status_tv.setText("准备中...");
                        retry_btn.setVisibility(View.GONE);
                        cancel_btn.setVisibility(View.GONE);
                    }

                    @Override
                    protected void progress(BaseDownloadTask task, long soFarBytes, long totalBytes) {
                        retry_btn.setVisibility(View.GONE);
                        cancel_btn.setVisibility(View.GONE);
                        Log.i("sssddd-download",soFarBytes+"--"+totalBytes);
                        status_tv.setText("下载中...");
                        progressBar.setProgress((int) ((((float) soFarBytes)/totalBytes)*100));
                    }

                    @Override
                    protected void paused(BaseDownloadTask task, long soFarBytes, long totalBytes) {
                        Log.i("sssddd-download","paused");
                        status_tv.setText("暂停中...");
                    }

                    @Override
                    protected void completed(BaseDownloadTask task) {
                        Log.i("sssddd-download","completed");
                        status_tv.setText("已完成");
                        retry_btn.setVisibility(View.GONE);
                        cancel_btn.setVisibility(View.GONE);
                        dismiss();
                        install();
                    }

                    @Override
                    protected void error(BaseDownloadTask task, Throwable e) {
                        Log.i("sssddd-download","error"+e.getMessage());
                        status_tv.setText("下载出错\n"+e.getMessage()+"\n"+url);
                        retry_btn.setVisibility(View.VISIBLE);
                        cancel_btn.setVisibility(View.VISIBLE);
                    }

                    @Override
                    protected void warn(BaseDownloadTask task) {
                        Log.i("sssddd-download","warn");
                    }
                })
                .start();

        cancel_btn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                dismiss();
            }
        });

        retry_btn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {

            }
        });
    }

    private void install(){
        Intent intentInstall = new Intent(android.content.Intent.ACTION_VIEW);
        intentInstall.setDataAndType(Uri.parse("file://"+downPath), "application/vnd.android.package-archive");
        intentInstall.setFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
        activity.startActivity(intentInstall);
    }

    public String getUrl() {
        return url;
    }

    public void setUrl(String url) {
        this.url = url;
    }

    public static int installSilent(String filePath) {
        File file = new File(filePath);
        if (filePath == null || filePath.length() == 0 || file == null || file.length() <= 0 || !file.exists() || !file.isFile()) {
            return 1;
        }

        String[] args = { "pm", "install", "-r", filePath };
        ProcessBuilder processBuilder = new ProcessBuilder(args);
        Process process = null;
        BufferedReader successResult = null;
        BufferedReader errorResult = null;
        StringBuilder successMsg = new StringBuilder();
        StringBuilder errorMsg = new StringBuilder();
        int result;
        try {
            process = processBuilder.start();
            successResult = new BufferedReader(new InputStreamReader(process.getInputStream()));
            errorResult = new BufferedReader(new InputStreamReader(process.getErrorStream()));
            String s;
            while ((s = successResult.readLine()) != null) {
                successMsg.append(s);
            }
            while ((s = errorResult.readLine()) != null) {
                errorMsg.append(s);
            }
        } catch (IOException e) {
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        } finally {
            try {
                if (successResult != null) {
                    successResult.close();
                }
                if (errorResult != null) {
                    errorResult.close();
                }
            } catch (IOException e) {
                e.printStackTrace();
            }
            if (process != null) {
                process.destroy();
            }
        }

        // TODO should add memory is not enough here
        if (successMsg.toString().contains("Success") || successMsg.toString().contains("success")) {
            result = 0;
        } else {
            result = 2;
        }
        Log.d("test-test", "successMsg:" + successMsg + ", ErrorMsg:" + errorMsg);
        return result;
    }

    public static boolean runRootCommand(String command) {

        Process process = null;

        DataOutputStream os = null;

        try {

            process = Runtime.getRuntime().exec("su");

            os = new DataOutputStream(process.getOutputStream());

            os.writeBytes(command+"\n");

            os.writeBytes("exit\n");

            os.flush();

            process.waitFor();

        } catch (Exception e) {

            Log.d("*** DEBUG ***", "Unexpected error - Here is what I know: "+e.getMessage());

            return false;

        }

        finally {

            try {

                if (os != null) {

                    os.close();

                }

                process.destroy();

            } catch (Exception e) {

                // nothing

            }

        }

        return true;

    }
}
