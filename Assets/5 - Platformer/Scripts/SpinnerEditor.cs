using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Spinner))]
public class SpinnerEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Spinner spinner = (Spinner)target;
        if(GUILayout.Button("Reset rotation"))
        {
            spinner.ResetRotation();
        }
        
    }

}
