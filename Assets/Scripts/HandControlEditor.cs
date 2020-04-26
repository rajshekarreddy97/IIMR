using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR

[CustomEditor(typeof(HandControl))]

public class HandControlEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GUIStyle myStyle = GUI.skin.GetStyle("HelpBox");
        myStyle.richText = true;
        myStyle.fontSize = 11;
        
        EditorGUILayout.HelpBox("\nIIMR : Intangible Interactions in Mixed Reality. \n\nIIMR operates over a network and allows for mid-air interactions using just the bare-hands. \nThe aim is to provide researchers and developers an inexpensive and accessible solution \nfor developing and prototyping hand interactions in a mixed reality environment. \nThe system consists of three modules, namely a Smartphone, a Google cardboard-like HMD, \nand a Leap Motion controller.\n",MessageType.Info);

        base.OnInspectorGUI();

        HandControl handControl = (HandControl) target;

        GUILayout.Label("Position of Leap Motion");

        GUILayout.BeginHorizontal();

            if(GUILayout.Button("Head Mounted"))
            {
                handControl.SetHeadMounted();
            }

            if(GUILayout.Button("Flat Surface"))
            {
                handControl.SetFlatSurface();
            }

        GUILayout.EndHorizontal();

        EditorGUILayout.HelpBox("\nCallibration data are automatically \n saved and loaded.\n",MessageType.Info);

        if(GUILayout.Button("Clear Callibration Data"))
        {
            handControl.ClearCallibrationData();
        }
    }
}

#endif