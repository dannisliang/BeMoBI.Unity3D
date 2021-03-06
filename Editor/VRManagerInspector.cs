﻿using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.UI;

[CustomEditor(typeof(VirtualRealityManager))]
public class VRManagerInspector : Editor
{
    private bool showHUDElementReferences = false;

    private VirtualRealityManager vrcontroller;

    public override void OnInspectorGUI()
    {
        vrcontroller = (VirtualRealityManager)target;

        base.OnInspectorGUI();

        EditorGUILayout.BeginVertical();

        EditorGUILayout.EndVertical();

    }

    public void OnSceneGUI()
    { 
        Handles.BeginGUI();

        GUILayout.Space(25);

        GUILayout.BeginVertical(GUILayout.MaxWidth(75));

        GUILayout.Label("Choose Environment", EditorStyles.whiteLabel);
        GUILayout.Space(10);
        foreach (var item in vrcontroller.AvailableEnvironments)
        {
            GUILayout.BeginHorizontal();
            
            item.enabled = GUILayout.Toggle(item.enabled, "");

            if (GUILayout.Button(item.Title, GUILayout.Width(75)))
            {
                vrcontroller.ChangeWorld(item.Title);
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();


        Handles.EndGUI();
    }

}
