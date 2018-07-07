using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Stage))]
public class MapModelCreator : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Create Map Model"))
        {
            Stage stage = target as Stage;
            stage.CreateMap();
        }
    }

}
