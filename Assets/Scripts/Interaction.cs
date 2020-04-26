using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (BoxCollider))]

public class Interaction : MonoBehaviour
{
    public Rigidbody rigidBody;
    public Color normalColor; 
    public Color feedbackColor;

    private void Start() 
    {
        rigidBody = GetComponent<Rigidbody>(); 
        rigidBody.constraints = RigidbodyConstraints.FreezeAll;

        normalColor = GetComponent<Renderer>().material.color;
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.tag == "Hand")
        {
            GetComponent<Renderer>().material.color = feedbackColor;

           // Interaction details goes here for when hand enters the object
        }  
    }

    private void OnCollisionStay(Collision other) 
    {
        // Interaction details goes here for when hand stays inside the object
    }

    private void OnCollisionExit(Collision other) 
    {
        GetComponent<Renderer>().material.color = normalColor;
       
        // Interaction details goes here for when hand exits the object 
    }
}
