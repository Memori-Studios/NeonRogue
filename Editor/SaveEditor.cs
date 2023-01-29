using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof (SaveSystem))]
public class SaveEditor : Editor {

	public override void OnInspectorGUI() {
		SaveSystem saveSystem = (SaveSystem)target;
		base.OnInspectorGUI();

		if (GUILayout.Button ("Wipe Save File")) {
			saveSystem.WipeSaveData();
		}
		if (GUILayout.Button ("Add Resources")) {
			saveSystem.AddResources();
		}
		if(GUILayout.Button ("Open Save Folder")) {
			EditorUtility.RevealInFinder(Application.persistentDataPath);
		}
	}
}