#if UNITY_ANDROID
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.IO;

public class PostBuildProcessor : MonoBehaviour {
	[PostProcessBuild]
	public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject) {
		// Only execute if target is Android
		if (target == BuildTarget.Android) {
			// Path to the generated AndroidManifest.xml
			string manifestPath = pathToBuiltProject + "/src/main/AndroidManifest.xml";

			// Load the manifest into memory
			string manifestContent = File.ReadAllText(manifestPath);

			// Modify the AndroidManifest.xml file content here
			// ...

			// Write the new manifest content
			File.WriteAllText(manifestPath, manifestContent);
		}
	}
}
#endif