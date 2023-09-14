package com.unity.lostcryptlargescreenexample;

import com.unity3d.player.UnityPlayerActivity;
import android.app.Activity;
import android.os.Bundle;
import android.util.Log;
import android.content.res.Configuration;
import android.view.View;
import android.view.Window;
import android.view.WindowManager;

public class OverrideForLargeScreen extends UnityPlayerActivity {
    // This ensures the layout will be correct.
    @Override public void onConfigurationChanged(Configuration newConfig) {
        super.onConfigurationChanged(newConfig);
		
        // This will appear in the Android logcat
        Log.d("UnityActivity.ConfigurationManager", newConfig.toString());

		// This will be sent to the C# layer in Unity, and can be received by game objects
        mUnityPlayer.UnitySendMessage("ConfigurationManager", "onConfigurationChanged", newConfig.toString());
    }
}