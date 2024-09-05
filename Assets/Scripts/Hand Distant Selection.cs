using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using UnityEngine;

public class HandDistantSelection : MonoBehaviour
{

    [SerializeField]
    private Transform handTransform;
    [SerializeField]
    private Transform palmTransform;

    public LineRenderer lineRenderer;
    public float selectionTime = 2.0f;

    private Transform selectedObject = null;
    private float timer = 0.0f;
    private bool isSelecting = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isSelecting)
        {
            lineRenderer.SetPosition(0, palmTransform.position);
            lineRenderer.SetPosition(1, handTransform.position + handTransform.up * 5.0f);
            lineRenderer.enabled = true;
        }
        else
        {
            lineRenderer.enabled = false;
        }
        RaycastHit hit;
        if(Physics.Raycast(handTransform.position, handTransform.up, out hit, Mathf.Infinity))
        {
            HandDistantSelectionInteractable interactable = hit.transform.GetComponent<HandDistantSelectionInteractable>();
            if(interactable != null && interactable.isInteractable())
            {
                lineRenderer.SetPosition(1, hit.point);
                if(selectedObject != hit.transform)
                {
                    if(selectedObject != null)
                    {
                        selectedObject.GetComponent<HandDistantSelectionInteractable>().OnDeselected();
                    }

                    selectedObject = hit.transform;
                    interactable.OnSelected();
                    timer = 0.0f;
                }
                else
                {
                    timer += Time.deltaTime;
                    if(timer >= selectionTime)
                    {
                        isSelecting = true;
                        StartCoroutine(MoveObjectToHand(selectedObject));
                    }
                }
            }
        }
        else
        {
            lineRenderer.enabled = false;
            if (selectedObject != null)
            {
                selectedObject.GetComponent<HandDistantSelectionInteractable>().OnDeselected();
                selectedObject = null;
            }
            timer = 0.0f;
            isSelecting = false;
        }
        
    }

    private IEnumerator MoveObjectToHand(Transform obj)
    {
        HandDistantSelectionInteractable interactable = obj.GetComponent<HandDistantSelectionInteractable>();
        interactable.isSelectable = false; // Desactivar la selección mientras se mueve el objeto
        while (Vector3.Distance(obj.position, handTransform.position) > 0.01f)
        {
            obj.position = Vector3.Lerp(obj.position, handTransform.position, Time.deltaTime * 5.0f);
            yield return null;
        }
        obj.position = handTransform.position;
        interactable.isSelectable = true; // Reactivar la selección después de mover el objeto
    }

    public void enableSelectionMode()
    {
        isSelecting = true;
    }

    public void disableSelectionMode()
    {
        isSelecting = false;
    }
}
