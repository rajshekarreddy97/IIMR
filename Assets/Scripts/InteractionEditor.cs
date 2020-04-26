using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

[CustomEditor(typeof(Interaction))]
public class InteractionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GUIStyle myStyle = GUI.skin.GetStyle("HelpBox");
        myStyle.richText = true;
        myStyle.fontSize = 11;
        
        EditorGUILayout.HelpBox("\nIIMR : Intangible Interactions in Mixed Reality. \n\nIIMR operates over a network and allows for mid-air interactions using just the bare-hands. \nThe aim is to provide researchers and developers an inexpensive and accessible solution \nfor developing and prototyping hand interactions in a mixed reality environment. \nThe system consists of three modules, namely a Smartphone, a Google cardboard-like HMD, and a Leap Motion controller.\n",MessageType.Info);

        base.OnInspectorGUI();

        Interaction interaction = (Interaction) target;
        
    }
}

#endif