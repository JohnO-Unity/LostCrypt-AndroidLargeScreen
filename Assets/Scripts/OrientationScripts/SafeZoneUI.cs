using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

[RequireComponent(typeof(RectTransform))]
public class SafeZoneUI : MonoBehaviour {
	private RectTransform rectTransform;
	public bool debugSafeZone = false;

	private void Start() {
		rectTransform = GetComponent<RectTransform>();
		if (null == rectTransform) {
			enabled = false;
			Debug.LogWarningFormat(gameObject, "SafeZoneUI needs a Panel to resize properly.  None found on object {0}, so this component will be disabled.", name);
			return;
		}

		ConfigurationManager.OnOrientationChange += ApplySafeZone;
		ApplySafeZone();
	}

	public void ApplySafeZone() {
		Rect safeArea = Screen.safeArea;
		Vector2 anchorMin = safeArea.position;
		Vector2 anchorMax = safeArea.position + safeArea.size;

		if (Screen.width > 0 && Screen.height > 0) {
			anchorMin.x /= Screen.width;
			anchorMin.y /= Screen.height;
			anchorMax.x /= Screen.width;
			anchorMax.y /= Screen.height;

			if (anchorMin.x >= 0 && anchorMin.y >= 0 && anchorMax.x >= 0 && anchorMax.y >= 0) {
				rectTransform.anchorMin = anchorMin;
				rectTransform.anchorMax = anchorMax;
			}
		}

#if DEBUG
		Debug.LogFormat("SafeZoneUI updated {0} with screen width/height at {1}/{2} and anchorMin/Max at {3}/{4}, safe area width/height {5}/{6}, original safeArea {7}", gameObject.name, safeArea.width, safeArea.height, anchorMin, anchorMax, Screen.width, Screen.height, safeArea);
#endif
	}
}