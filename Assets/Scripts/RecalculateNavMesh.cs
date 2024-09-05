using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class RecalculateNavMesh : MonoBehaviour
{
    [SerializeField]
    private NavMeshSurface navMeshSurface;
    [SerializeField]
    private NavMeshModifierVolume navMeshModifierVolume;

    public bool doIt = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (doIt)
        {
            doIt = false;
            recalculate();
        }
    }

    public void recalculate()
    {
        StartCoroutine(recalculateNavMesh());
    }
    IEnumerator recalculateNavMesh()
    {
        yield return new WaitForSeconds(5);
        navMeshModifierVolume.area = NavMesh.GetAreaFromName("Walkable");
        navMeshSurface.BuildNavMesh();
    }
}
