package com.sqb.myapplication;

import androidx.appcompat.app.AppCompatActivity;

import android.annotation.TargetApi;
import android.content.Intent;
import android.graphics.Bitmap;
import android.os.Build;
import android.os.Bundle;
import android.view.KeyEvent;
import android.view.View;
import android.view.Window;
import android.view.WindowManager;
import android.webkit.WebResourceRequest;
import android.webkit.WebSettings;
import android.webkit.WebView;
import android.webkit.WebViewClient;
//打包密码 123456789
public class MainActivity extends AppCompatActivity {

    private WebView webView;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        webView = (WebView) findViewById(R.id.wv);
        WebSettings webSetting = webView.getSettings();
        webSetting.setJavaScriptEnabled(true);//js交互允许
        //自适应屏幕
        webSetting.setUseWideViewPort(true);//设置此属性，可任意比例缩放
        webSetting.setLoadWithOverviewMode(true);//缩放至屏幕的大小
        webView.requestFocusFromTouch();
        webSetting.setAllowFileAccess(true);
        webSetting.setJavaScriptCanOpenWindowsAutomatically(true);
        webSetting.setLoadsImagesAutomatically(true);  //支持自动加载图片
        //支持内容重新布局
        webSetting.setLayoutAlgorithm(WebSettings.LayoutAlgorithm.NARROW_COLUMNS);
        webSetting.setAppCacheEnabled(true);
        webSetting.setCacheMode(WebSettings.LOAD_DEFAULT);
        webSetting.setDomStorageEnabled(true);//当网页需要保存数时据
        webSetting.setGeolocationEnabled(true);//启用还H5的地理定位服务
        webSetting.setAllowFileAccessFromFileURLs(true);
        webSetting.setLoadWithOverviewMode(true);
        if(Build.VERSION.SDK_INT>=Build.VERSION_CODES.LOLLIPOP){//防止5.0以上HTTPS中含有http的链接无法显示
            webSetting.setMixedContentMode(WebSettings.MIXED_CONTENT_ALWAYS_ALLOW);
        }
        webView.setWebViewClient(new WebViewClient() {
            @Override
            public boolean shouldOverrideUrlLoading(WebView view, String url) {
                startPay(url);
                return super.shouldOverrideUrlLoading(view, url);
            }

            @TargetApi(Build.VERSION_CODES.LOLLIPOP)
            @Override
            public boolean shouldOverrideUrlLoading(WebView view, WebResourceRequest request) {
                return shouldOverrideUrlLoading(view, request.getUrl().toString());
            }
            @Override
            public void onPageStarted(WebView view, String url, Bitmap favicon) {
                super.onPageStarted(view, url, favicon);
            }
            @Override
            public void onPageFinished(WebView view, String url) {
                super.onPageFinished(view, url);
            }
        });
        webView.loadUrl("http://jsq.lawss360.com/sqb/home");
        //webView.loadUrl("https://app.huobaowang.com/meeting/expo/35/company");

    }

    @Override
    public boolean onKeyDown(int keyCode, KeyEvent event) {
        if((keyCode == KeyEvent.KEYCODE_BACK) && webView.canGoBack() ){
            webView.goBack();
            return  true;
        }
        return  false;
    }

    private boolean startPay(String url) {
        try {
           if (parseScheme(url)) {
                Intent intent;
                intent = Intent.parseUri(url, Intent.URI_INTENT_SCHEME);
                intent.addCategory("android.intent.category.BROWSABLE");
                intent.setComponent(null);
                startActivity(intent);
                return true;
            }
        } catch (Exception e) {

        }
        return false;
    }

    public boolean parseScheme(String url) {
        if (url.contains("platformapi/startApp") || url.contains("platformapi/startapp")) {
            return true;
        } else if ((Build.VERSION.SDK_INT > Build.VERSION_CODES.M) && (url.contains("platformapi") && (url.contains("startApp") || url.contains("startpp")))) {
            return true;
        } else {
            return false;
        }
    }
}