using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionEvent : MonoBehaviour
{
    private OneHandLocomotion oneHandLocomotion;

    void Start()
    {
        oneHandLocomotion = GetComponentInChildren<OneHandLocomotion>();
    }

    void OnCollisionEnter(Collision col)
    {
        if(oneHandLocomotion != null && col.gameObject.tag == "Floor")
        {
            oneHandLocomotion.SetCollisionState(true);
            Debug.Log("Me chocooooo");
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (oneHandLocomotion != null && col.gameObject.tag == "Floor")
        {
            oneHandLocomotion.SetCollisionState(false);
            Debug.Log("Ya no :D");
        }
    }
}
