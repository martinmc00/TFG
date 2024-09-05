using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternateMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject locomotion;
    [SerializeField]
    private GameObject leftLocomotionInteractor;
    [SerializeField]
    private GameObject rightLocomotionInteractor;
    [SerializeField]
    private DataManager dataManager;

    private NavMeshOHL navMeshOHL;
    private OneHandRotation oneHandRotation;
    private BestSelectInteractorGroup leftBSIG;
    private BestSelectInteractorGroup rightBSIG;
    private bool bsigState = true;

    private void Start()
    {
        navMeshOHL = locomotion.GetComponent<NavMeshOHL>();
        oneHandRotation = locomotion.GetComponent<OneHandRotation>();
        leftBSIG = leftLocomotionInteractor.GetComponent<BestSelectInteractorGroup>();
        rightBSIG = rightLocomotionInteractor.GetComponent<BestSelectInteractorGroup>();
    }

    public void alternateMovement()
    {
        alternateMove(); //Cambiar el método de movimiento
        alternateRotation(); //Cambiar el método de rotación
        alternateLocomotionInteraction(); //Activar/desactivar el teletransporte
    }

    private void alternateMove()
    {
        if (navMeshOHL.enabled) //Si el movimiento continuo está activo
        {
            navMeshOHL.enabled = false; //Se desactiva
            dataManager.setSLTimer(true);//Se activa el temporizador del teletransporte
        }
        else                            //Si es el teletransporte el que se desactiva
        {
            navMeshOHL.enabled = true; //Se activa el movimiento continuo
            dataManager.setSLTimer(false); //Se desactiva el temporizador del teletransporte
        }
    }

    private void alternateRotation()
    {
        if (oneHandRotation.enabled) //Si la rotación continua está activada
        {
            oneHandRotation.enabled = false; //Se desactiva
        }
        else                            //En otro caso
        {
            oneHandRotation.enabled = true; //Se activa
        }
    }

    private void alternateLocomotionInteraction()
    {
        bsigState = !bsigState; //Se cambia el estado al opuesto
        leftBSIG.enabled = bsigState; //Se activa/desactiva el teletransporte con la mano izquierda
        rightBSIG.enabled = bsigState;//Se activa/desactiva el teletransporte con la mano derecha
    }

}

