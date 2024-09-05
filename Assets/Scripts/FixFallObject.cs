using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixFallObject : MonoBehaviour
{
    [SerializeField]
    private Transform player;


    private void OnTriggerEnter(Collider other)
    {
        other.transform.position = player.transform.position;
    }
}
