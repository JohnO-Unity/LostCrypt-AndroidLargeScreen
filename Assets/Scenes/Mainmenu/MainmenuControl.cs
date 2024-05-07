using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.RectTransform;

public class MainmenuControlOnFold : MonoBehaviour
{
    private ConfigurationManager configurationManager;

    public GameObject portraitCanvas;
    public Button originalP;
    public Button anchoringP;
    public Button hingeP;
    public Button confirmButtonP;
    public Button cancelButtonP;
    public UnityEngine.UI.ScrollRect textViewP;

    public Canvas landscapeCanvas;
    public Button originalL;
    public Button anchoringL;
    public Button hingeL;
    public Button confirmButtonL;
    public Button cancelButtonL;
    public UnityEngine.UI.ScrollRect textViewL;

    public Button originalH;
    public Button anchoringH;
    public Button hingeH;
    public Button confirmButtonH;
    public Button cancelButtonH;
    public UnityEngine.UI.ScrollRect textViewH;

    private static readonly String ORIGINAL_TEXT = "To start, it's essential to implement support for " +
        "various aspect ratios in your Android Large Screen application. This ensures your " +
        "game or application appears correctly across different screen sizes. To do this, " +
        "you should use the full screen resizable setting. You might consider adjusting the " +
        "camera or canvas aspect ratio to better fit different screens. Settings in this project " +
        "can be viewed in the Build Settings, and in the Plugins/Android/AndroidManifest.xml file. " +
        "You can experience the full screen resizable feature in the scene named Original";

    private static readonly String ANCHORING_TEXT = "After ensuring that your game can adapt to " +
        "different aspect ratios, the next step is to ensure your game can utilize the " +
        "entire screen. This makes gameplay more immersive and can enhance the user experience. " +
        "To achieve this, you should update your game UI anchoring and camera settings to " +
        "automatically adjust to the screen size. This allows UI elements to maintain their " +
        "positions relative to the screen size.\n\nThe Anchoring scene uses CameraAspectLock " +
        "script to respond to the device configuration changes by using an extended Activity " +
        "you can find at Assets/Plugins/Android/LargeScreenPlayableActivity.java. The usage " +
        "of Unity's safeArea API was also demonstrated in the SafeZoneAPI script which binds " +
        "to the SafeZone object inside Anchoring scene.";

    private static readonly String HINGE_TEXT = "The last scene HingeAware contains a ConfigurationManager " +
        "object responding to the different folding states of the target device through Jetpack library " +
        "by using an extended Activity you can find at Assets/Plugins/Android/LargeScreenPlayableActivity.java. " +
        "The scene then use the PanelOnFold script to control its UI based on the fold status of " +
        "the device such as showing the bottom controller panel when device is on tabletop mode and adjusting the camera.";

    private enum TARGET_SCENE
    {
        ORIGINAL,
        ANCHORING,
        HINGE
    }

    private bool isConfirmState;
    private TARGET_SCENE targetScene;

    private void Awake()
    {
        SetButtonsListeners();
        configurationManager = (ConfigurationManager)GameObject.Find("ConfigurationManager")
            .GetComponent(typeof(ConfigurationManager));
        configurationManager.ActionOnOrientationChange += OnOrientationChange;        
        isConfirmState = false;
        UpdateButtonOnStateChange();
    }

    private void SetButtonsListeners()
    {
        originalP.onClick.AddListener(() => ChangeToConfirmState(TARGET_SCENE.ORIGINAL));
        originalL.onClick.AddListener(() => ChangeToConfirmState(TARGET_SCENE.ORIGINAL));

        anchoringP.onClick.AddListener(() => ChangeToConfirmState(TARGET_SCENE.ANCHORING));
        anchoringL.onClick.AddListener(() => ChangeToConfirmState(TARGET_SCENE.ANCHORING));

        hingeP.onClick.AddListener(() => ChangeToConfirmState(TARGET_SCENE.HINGE));
        hingeL.onClick.AddListener(() => ChangeToConfirmState(TARGET_SCENE.HINGE));

        confirmButtonP.onClick.AddListener(ConfirmGotoScene);
        confirmButtonL.onClick.AddListener(ConfirmGotoScene);

        cancelButtonP.onClick.AddListener(CancelGotoScene);
        cancelButtonL.onClick.AddListener(CancelGotoScene);
    }

    private void CancelGotoScene()
    {
        isConfirmState = false;
        UpdateButtonOnStateChange();
    }

    private void ConfirmGotoScene()
    {
        switch (targetScene)
        {
            case TARGET_SCENE.ORIGINAL:
                SceneManager.LoadSceneAsync("Original");
                break;
            case TARGET_SCENE.ANCHORING:
                SceneManager.LoadSceneAsync("Anchoring");
                break;
            case TARGET_SCENE.HINGE:
                SceneManager.LoadSceneAsync("HingeAware");
                break;
            default:
                break;
        }
    }

    private void ChangeToConfirmState(TARGET_SCENE targetScene)
    {
        this.targetScene = targetScene;
        isConfirmState = true;

        Debug.Log("Set text: " + targetScene.ToString());
        switch (targetScene)
        {
            case TARGET_SCENE.ORIGINAL:
                textViewP.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(ORIGINAL_TEXT);
                textViewL.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(ORIGINAL_TEXT);
                break;
            case TARGET_SCENE.ANCHORING:
                textViewP.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(ANCHORING_TEXT);
                textViewL.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(ANCHORING_TEXT);
                break;
            case TARGET_SCENE.HINGE:
                textViewP.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(HINGE_TEXT);
                textViewL.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(HINGE_TEXT);
                break;
            default:
                break;
        }
        UpdateButtonOnStateChange();
    }

    private void ResetButtonsPosition(string orientation)
    {
        Debug.Log("reset button: " + orientation);
        if (orientation.Equals("ORIENTATION_PORTRAIT"))
        {
            portraitCanvas.gameObject.SetActive(true);
            landscapeCanvas.gameObject.SetActive(false);
        }
        else
        {
            portraitCanvas.gameObject.SetActive(false);
            landscapeCanvas.gameObject.SetActive(true);
        }
    }

    private void UpdateButtonOnStateChange()
    {
        confirmButtonP.gameObject.SetActive(isConfirmState);
        textViewP.gameObject.SetActive(isConfirmState);
        cancelButtonP.gameObject.SetActive(isConfirmState);

        confirmButtonL.gameObject.SetActive(isConfirmState);        
        textViewL.gameObject.SetActive(isConfirmState);        
        cancelButtonL.gameObject.SetActive(isConfirmState);

        originalP.gameObject.SetActive(!isConfirmState);
        anchoringP.gameObject.SetActive(!isConfirmState);
        hingeP.gameObject.SetActive(!isConfirmState);

        originalL.gameObject.SetActive(!isConfirmState);
        anchoringL.gameObject.SetActive(!isConfirmState);
        hingeL.gameObject.SetActive(!isConfirmState);
    }

    private void OnOrientationChange(ConfigurationManager.OrientationInfo info)
    {
        ResetButtonsPosition(info.orientation);
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetButtonsPosition(Screen.orientation == 0 ? "ORIENTATION_PORTRAIT" : "ORIENTATION_LANDSCAPE");
    }

    // Update is called once per frame
    void Update()
    {
    }
}
