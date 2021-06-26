using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(QuestionManager))]
public class QuestionnaireToggleEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        QuestionManager trg = (QuestionManager)target;
        if (GUILayout.Button("Create"))
            trg.Create();
        if (GUILayout.Button("Set"))
            trg.Set();
    }
}
