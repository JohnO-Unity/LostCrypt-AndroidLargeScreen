using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraAspectLock : MonoBehaviour
{
	[Range(0.5f, 2.2f)]
	public float maintainAspect = 1.0f;
	private Camera _camera;

    private void Awake() {
		_camera = GetComponent<Camera>();
    }

	private void Start() {
	}

	private void Update() {
        // In-editor testing can test and simulate orientation changes
	#if UNITY_EDITOR
		StartCoroutine(MaintainAspectRatio());
	#endif
    }


    public void OnConfigurationChanged()
	{
        StartCoroutine(MaintainAspectRatio());
    }

    private IEnumerator MaintainAspectRatio()
    {

        float scaleScreen = Screen.width / Screen.height;
		Rect rect = _camera.pixelRect;
		if (scaleScreen > maintainAspect) {
			// use height, lock width to max at aspect scale
			rect.width = Screen.height * maintainAspect;
			rect.height = Screen.height;
		} else {
			// use width, lock height to max at aspect scale
			rect.width = Screen.width;
			rect.height = Screen.width / maintainAspect;
		}
		/*
		var widthFactor = Mathf.Floor(Screen.width / minWidth);
		var heightFactor = Mathf.Floor(Screen.height / minHeight);

		// Use the smaller one
		var factorToUse = widthFactor > heightFactor ? heightFactor : widthFactor;

		Rect rect = _camera.pixelRect;
		rect.width = minWidth * factorToUse;
		rect.height = minHeight * factorToUse;
		*/

		rect.x = Mathf.Floor((Screen.width - rect.width) * 0.5f);
		rect.y = Mathf.Floor((Screen.height - rect.height) * 0.5f);

        yield return null;

        _camera.pixelRect = rect;        
	}
}