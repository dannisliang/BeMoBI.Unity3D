﻿using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.UI;

[CustomEditor(typeof(HUDInstruction))]
public class InstructionEditor : Editor {

    private bool showHUDElementReferences = false;

    public override void OnInspectorGUI()
    {
        HUDInstruction instructions = (HUDInstruction)target;

        EditorGUILayout.BeginVertical();

        showHUDElementReferences = EditorGUILayout.Foldout(showHUDElementReferences, "HUD Elements");
        
        if (showHUDElementReferences) { 
            instructions.InstructionTextBesideImage = (Text) EditorGUILayout.ObjectField(new GUIContent("Text renderer"), instructions.InstructionTextBesideImage, typeof(Text), true);
            instructions.InstructionTextWide = (Text) EditorGUILayout.ObjectField(new GUIContent("Text w/o Image"), instructions.InstructionTextWide, typeof(Text), true);
            instructions.InstructionImage = (RawImage) EditorGUILayout.ObjectField(new GUIContent("Image renderer"), instructions.InstructionImage, typeof(RawImage), true);
            
        }
        
        instructions.KeyOnForward = (KeyCode)EditorGUILayout.EnumMaskField("Next instruction", instructions.KeyOnForward);
        instructions.KeyOnBackward = (KeyCode)EditorGUILayout.EnumMaskField("Last instruction", instructions.KeyOnBackward);
        instructions.KeyOnRestart = (KeyCode)EditorGUILayout.EnumMaskField("Replay instruction", instructions.KeyOnRestart);
        
        if (GUILayout.Button("Play Instructions"))
        {
            if (instructions.currentInstructionSet == null)
            {
                instructions.currentInstructionSet = InstructionEditorWindow.CreateExampleInstructionSet();
            }

            instructions.StartDisplaying("NameOfInstructionSet");
        }

        if (GUILayout.Button("Edit Instructions")) 
        {
            var window = (InstructionEditorWindow) InstructionEditorWindow.GetWindow(typeof(InstructionEditorWindow), true, "Instructions Editor");
            window.HudController = instructions;
            window.Show();
        }

        EditorGUILayout.EndVertical();

    }

}
