using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class OpenSecretPath : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioClip;
    [SerializeField]
    private NavMeshSurface navMeshSurface;
    [SerializeField]
    private NavMeshModifierVolume navMeshModifierVolume;
    [SerializeField]
    private PokeInteractable pokeInteractable;

    private bool open = false;
    private bool alreadyOpen = false;

    public bool doIt = false;

    IEnumerator openPath()
    {
        if(!alreadyOpen && open)
        {
            alreadyOpen = true;
            audioClip.Play();
            //navMeshSurface.gameObject.SetActive(true);
            yield return new WaitForSeconds(1);
            pokeInteractable.Disable();
            yield return new WaitForSeconds(1);
            this.gameObject.SetActive(false);
            Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!\n");
        }
        yield return null;
    }

    IEnumerator prueba()
    {
        alreadyOpen = true;
        audioClip.Play();
        //navMeshSurface.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        pokeInteractable.Disable();
        yield return new WaitForSeconds(1);
        this.gameObject.SetActive(false);
    }

    IEnumerator recalculateNavMesh()
    {
        yield return new WaitForSeconds(5);
        navMeshModifierVolume.area = NavMesh.GetAreaFromName("Walkable");
        navMeshSurface.BuildNavMesh();
    }

    void Update()
    {
        if (doIt)
        {
            doIt = false;
            StartCoroutine(prueba());
        }
    }

    public void openSecretPath()
    {
        open = true;
        StartCoroutine(openPath());
        StartCoroutine(recalculateNavMesh());
    }
}
