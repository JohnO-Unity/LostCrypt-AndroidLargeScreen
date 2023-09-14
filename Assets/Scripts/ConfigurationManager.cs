using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigurationManager : MonoBehaviour {
    public TMPro.TMP_Text labelDebug;   // Useful to see the information sent from the Android activity event

    void Awake() {
        if (null != labelDebug)
            labelDebug.text = string.Empty;
    }

    // This will be called from the OverrideForLargeScreen.java class, from the activity callback onConfigurationChanged
    public void onConfigurationChanged(string newConfig) {
        if (null != labelDebug)
            labelDebug.text = newConfig;
    }
}