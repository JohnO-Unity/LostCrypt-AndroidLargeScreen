using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OnScreenControls : MonoBehaviour
{
    public Canvas controlsCanvas;
    public Button backButton;

    void Awake()
    {
        bool isMobile = SystemInfo.deviceType == DeviceType.Handheld;

#if SIMULATE_MOBILE
        isMobile = true;
#endif

        if (controlsCanvas != null)
            controlsCanvas.gameObject.SetActive(isMobile);

        if (isMobile)
            Application.targetFrameRate = 60;

        if (backButton)
        {
            backButton.onClick.AddListener(() =>
            {
                SceneManager.LoadSceneAsync("Mainmenu");
            });
        }
    }
}
