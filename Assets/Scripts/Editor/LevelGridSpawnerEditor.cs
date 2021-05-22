using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelGridSpawner))]
public class LevelGridSpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        LevelGridSpawner myTarget = (LevelGridSpawner)target;
        if (GUILayout.Button("Generate Floor Grid"))
        {
            myTarget.Generate();
        }
        if (GUILayout.Button("Place Block"))
        {
            myTarget.PlaceWall();
        }
    }
}
