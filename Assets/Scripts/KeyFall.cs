using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyFall : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioClip;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("AAAAAAAAAAAAAAAAAAAA");
        if (collision.gameObject.tag.Equals("Floor"))
        {
            audioClip.Play();
            Debug.Log("BBBBBBBBBBBBBBBBBBBBBBBBBB");            
        }
    }
}
