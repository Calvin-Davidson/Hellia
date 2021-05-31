using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DynamicPostProcessingFade))]
public class DynamicPostProcessingFadeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        DynamicPostProcessingFade myTarget = (DynamicPostProcessingFade)target;
        if (GUILayout.Button("Reset Post Processing"))
        {
            myTarget.ResetPostProcessing();
        }
    }
}
