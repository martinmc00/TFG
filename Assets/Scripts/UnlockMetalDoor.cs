using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using static Oculus.Interaction.TwoGrabRotateTransformer;

public class UnlockMetalDoor : MonoBehaviour
{
    [SerializeField]
    private GameObject key;
    [SerializeField]
    private GameObject door;
    [SerializeField]
    private NavMeshSurface navMeshSurface;
    [SerializeField]
    private NavMeshModifierVolume navMeshModifierVolume;
    [SerializeField]
    private AudioSource audioClip;
    

    private bool doorUnlocked = false;

    private int doorType = -1;

    private OneGrabRotateTransformer oneGrabRotateTransformer;
    private TwoGrabRotateTransformer twoGrabRotateTransformer;

    // Start is called before the first frame update
    void Start()
    {
        if(door.gameObject.name == "Silver Hinge Door")
        {
            doorType = 0;
        }else if(door.gameObject.name == "Golden Hinge Door")
        {
            doorType = 1;
        }

        oneGrabRotateTransformer = door.GetComponentInChildren<OneGrabRotateTransformer>();
        twoGrabRotateTransformer = door.GetComponentInChildren<TwoGrabRotateTransformer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(correctKey(other.gameObject.name) && !doorUnlocked) //Si se acerca la llave correcta y la puerta está bloqueada
        {
            unlockDoor(); //Se desbloquea la puerta
            audioClip.Play(); //Se reproduce un sonido para indicar el desbloqueo
            other.gameObject.SetActive(false); //Se desactiva la llave para que desaparezca de la escena
            StartCoroutine(recalculateNavMesh()); //Se vuelve a calcular la superficie
                                                  //por la que el jugador se puede desplazar
        }
    }

    private void unlockDoor()
    {
        doorUnlocked = true;
        oneGrabRotateTransformer.Constraints.MinAngle.Value = -90f; //Se cambian los límites de rotación de
        oneGrabRotateTransformer.Constraints.MaxAngle.Value = 90f; //la puerta tanto para cuando se abre con una mano

        //Como cuando se usan las dos manos para abrir la puerta
        TwoGrabRotateTransformer.TwoGrabRotateConstraints _constraints = new TwoGrabRotateTransformer.TwoGrabRotateConstraints
        {
            MinAngle = new FloatConstraint
            {
                Constrain = true,
                Value = -90f
            },
            MaxAngle = new FloatConstraint
            {
                Constrain = true,
                Value = 90f
            }
        };
        twoGrabRotateTransformer.InjectOptionalConstraints(_constraints);
    }
    

    private bool correctKey(string keyName)
    {
        //Se comprueba si la llave de plata se usa en la puerta de plata
        if (doorType == 0 && keyName == "Key_Silver") 
        {
            return true;
        }
        //Se comprueba si la llave de oro se usa en la puerta de oro
        else if (doorType == 1 && keyName == "Key_Golden")
        {
            return true;
        }

        return false;
    }

    IEnumerator recalculateNavMesh()
    {
        yield return new WaitForSeconds(1);

        //Se establece como "Walkable" el área que se vaya a desbloquear
        navMeshModifierVolume.area = NavMesh.GetAreaFromName("Walkable");
        navMeshSurface.BuildNavMesh(); //Se reconstruye la superficie de navegación
    }
}
