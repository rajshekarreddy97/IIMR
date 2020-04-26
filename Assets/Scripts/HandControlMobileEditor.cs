using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR

[CustomEditor(typeof(HandControlMobile))]

public class HandControlMobileEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GUIStyle myStyle = GUI.skin.GetStyle("HelpBox");
        myStyle.richText = true;
        myStyle.fontSize = 11;

        EditorGUILayout.HelpBox("\nIIMR : Intangible Interactions in Mixed Reality. \n\nIIMR operates over a network and allows for mid-air interactions using just the bare-hands. The aim is to provide researchers and developers an inexpensive and accessible solution for developing and prototyping hand interactions in a mixed reality environment. The system consists of three modules, namely a Smartphone, a Google cardboard-like HMD, and a Leap Motion controller.\n",MessageType.None);

        base.OnInspectorGUI();

        HandControlMobile handControlMobile = (HandControlMobile) target;

        EditorGUILayout.HelpBox("\nStereoscopic Rendering is enabled by default. To enable Stereoscopic rendering, follow the below steps: \n\n1. Open Assets->Resources->Vuforia Configuration. Under the 'Device Type' dropdown, select 'Phone + Viewer'.\n2. Under the 'Viewer Type' dropdown, select your HMD.  \n3. Go to Unity Player Settings -> XR Settings and enable 'Virtual Reality Supported', then click on the '+' sign and choose 'Vuforia'.\n",MessageType.Info);
        EditorGUILayout.HelpBox("\nTo enable Monoscopic rendering, follow the below steps: \n\n1. Open Assets->Resources->Vuforia Configuration. Under the 'Device Type' dropdown, select 'Handheld'. \n2. Go to Unity Player Settings -> XR Settings and disable 'Virtual Reality Supported'.\n",MessageType.Info);
        EditorGUILayout.HelpBox("\nVuforia Ground Plane support is only available through Monoscopic Rendering.\n",MessageType.Info);

    }
}

#endif