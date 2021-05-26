using System.Collections;
using System.Collections.Generic;
using Runtime.LightSystem;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LightReceiver), true)]
public class LightReceiverEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        ILightReceiver myTarget = (ILightReceiver)target;
        if (GUILayout.Button("Respawn crystals"))
        {
            myTarget.InstantiateBeams();
        }
    }
}
