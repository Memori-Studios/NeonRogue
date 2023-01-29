using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor (typeof (WaveFunctionCollapse))]
public class WFCEditor : Editor {

	public override void OnInspectorGUI() {
		WaveFunctionCollapse waveFunctionCollapse = (WaveFunctionCollapse)target;
		base.OnInspectorGUI();

		if (GUILayout.Button ("Generate Prototypes")) {
			waveFunctionCollapse.InitializeWaveFunction();
		}
		if (GUILayout.Button ("Clear")) {
			waveFunctionCollapse.ClearAll();
		}
		if (GUILayout.Button ("Collapse")) {
			waveFunctionCollapse.StartCollapse();
		}
	}
}