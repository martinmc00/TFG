using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandDistantSelectionInteractable : MonoBehaviour
{
    [SerializeField]
    public bool isSelectable = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool isInteractable()
    {
        return isSelectable;
    }

    public void OnSelected()
    {
        GetComponent<Renderer>().material.color = Color.green;
    }
    
    public void OnDeselected()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }
}
