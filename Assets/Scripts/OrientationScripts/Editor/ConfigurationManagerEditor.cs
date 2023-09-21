using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(ConfigurationManager))]
[CanEditMultipleObjects]
public class ConfigurationManagerEditor : Editor {
	SerializedProperty labelDebug;

	void OnEnable() {
		labelDebug = serializedObject.FindProperty("labelDebug");
	}

	public override void OnInspectorGUI() {
		serializedObject.Update();
		EditorGUILayout.PropertyField(labelDebug);
		serializedObject.ApplyModifiedProperties();
		if (Application.isPlaying) {
			if (EditorGUILayout.LinkButton("Force Refresh")) {
				(serializedObject.targetObject as ConfigurationManager).onConfigurationChanged(string.Empty);
			}
		}
	}
}