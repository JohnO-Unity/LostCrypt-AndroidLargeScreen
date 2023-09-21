using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigurationManager : MonoBehaviour {
    public TMPro.TMP_Text labelDebug;   // Useful to see the information sent from the Android activity event
    public static Action OnOrientationChange;

    void Awake() {
#if DEBUG
        if (null != labelDebug)
            labelDebug.text = string.Empty;
#endif
    }

    // This will be called from the OverrideForLargeScreen.java class, from the activity callback onConfigurationChanged
    public void onConfigurationChanged(string newConfig) {
#if DEBUG
		if (null != labelDebug)
            labelDebug.text = newConfig;
#endif
        // Always call the refresh from the main Unity thread, since this is where the UI updates occur
        StartCoroutine(ExecuteOnMainUnityThread());
    }

	IEnumerator ExecuteOnMainUnityThread() {
        yield return null;  // Will be called from main thread on next update
		OnOrientationChange?.Invoke();
	}
}