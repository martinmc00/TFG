using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class OpenMetalGate : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private AudioSource audioClip;
    [SerializeField]
    private NavMeshSurface navMeshSurface;
    [SerializeField]
    private NavMeshModifierVolume navMeshModifierVolume;

    private bool open = false;
    private bool alreadyOpen = false;

    IEnumerator recalculateNavMesh()
    {
        yield return new WaitForSeconds(1);
        navMeshModifierVolume.area = NavMesh.GetAreaFromName("Walkable");
        navMeshSurface.BuildNavMesh();
    }

    // Update is called once per frame
    void Update()
    {
        if (!alreadyOpen && open)
        {
            //Debug.Log("Me abrooooo");
            alreadyOpen = true;
            animator.SetBool("open", true);
            animator.Play("MetalDoorOpen");
            audioClip.Play();
            StartCoroutine(recalculateNavMesh());
            Debug.Log("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\n");
        }
    }

    public void openDoor()
    {
        open = true;
    }

}
