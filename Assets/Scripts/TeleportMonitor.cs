using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TeleportMonitor : MonoBehaviour
{
    [SerializeField]
    private Transform cameraRig;
    [SerializeField]
    private NavMeshAgent navMeshAgent;
    [SerializeField]
    private float positionThreshold = 1.0f;
    [SerializeField]
    private bool gestureMovementEnabled = false;

    private Vector3 lastPosition;

    // Start is called before the first frame update
    void Start()
    {
        lastPosition = cameraRig.position;
        navMeshAgent.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Se calcula la distancia entre la posici�n actual de la c�mara y la anterior
        float distance = Vector3.Distance(cameraRig.position, lastPosition); 

        if(distance > positionThreshold) //Si la distancia supera un l�mite
        {
            OnTeleportComplete(cameraRig.position); //Se opera sobre el agente para evitar problemas de rutas
        }

        lastPosition = cameraRig.position; //Se establece la �ltima posici�n de la c�mara como la actual

        cameraRig.position = navMeshAgent.transform.position; //Se establece la posici�n actual de la c�mara
                                                              //como la posici�n actual del agente
    }

    private void OnTeleportComplete(Vector3 newPosition)
    {
        navMeshAgent.enabled = true;    //Se activa el agente para poder operar sobre �l
        navMeshAgent.Warp(newPosition); //Se teleporta el agente a la nueva posici�n
        navMeshAgent.ResetPath();       //Se reinicia y retira la ruta que tenga asignada el agente
        if (!gestureMovementEnabled)    //Si el movimiento por gestos no est� activo
        {
            navMeshAgent.enabled = false;//Se desactiva el agente
        }
    }

    public void ToggleGestureMovement()
    {
        //Se cambia el tipo de movimiento al contrario
        gestureMovementEnabled = !gestureMovementEnabled;
        
        if (!gestureMovementEnabled) //Si el movimiento continuo no est� activo
        {
            navMeshAgent.enabled = true; 
            navMeshAgent.ResetPath(); //Se resetea el camino del agente (soluciona problemas de rutas)
            navMeshAgent.enabled = false; //Se desactiva el agente
        }
        else //Si el movimiento continuo est� activo
        {
            navMeshAgent.enabled = true; //Se activa el agente
        }
    }
}
