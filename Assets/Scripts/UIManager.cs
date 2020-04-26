using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public GameObject HandController;
    private HandControl handcontrol;

    public GameObject messageText;
    private Text text;
    
    void Start()
    {
        text = messageText.GetComponent<Text>();
        handcontrol = HandController.GetComponent<HandControl>();
    }

    void Update()
    {
        text.text = "Message: " + handcontrol.strMessage;
    }
}
