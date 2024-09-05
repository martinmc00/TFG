using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneHandLocomotion : MonoBehaviour
{

    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private Transform _playerOrigin;
    [SerializeField]
    private Transform _playerHead;
    [SerializeField]
    private bool moveForward = false;
    [SerializeField]
    private bool moveBackwards = false;
    [SerializeField]
    private float speed = 1.0f;

    private Vector3 direccion;
    private Collider collider;
    private CollisionEvent collisionEvent;
    private bool choque = false;

    // Start is called before the first frame update
    void Start()
    {
        collider = playerTransform.GetComponent<Collider>();
        
    }

    public void enableMoveForward()
    {
        moveForward = true;
    }

    public void disableMoveForward()
    {
        moveForward = false;
    }

    public void enableMoveBackwards()
    {
        moveBackwards = true;
    }

    public void disableMoveBackwards()
    {
        moveBackwards = false;
    }

    public void SetCollisionState(bool state)
    {
        choque = state;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (moveForward && !choque)
        {
            direccion = _playerHead.forward;
            direccion.y -= direccion.y;
            //Debug.Log(direccion);
            
            Vector3 movimiento = direccion * speed * Time.deltaTime;
            playerTransform.position += movimiento;
        }

        if(moveBackwards && !choque)
        {
            direccion = _playerHead.forward;
            direccion.y -= direccion.y;

            Vector3 movimiento = direccion * speed * Time.deltaTime;
            playerTransform.position -= movimiento;
        }
        
        /*
        Pose originalPose = _playerOrigin.GetPose();
        Pose delta = PoseUtils.Delta(originalPose, _playerOrigin.GetPose());
        while (move)
        {

            _playerOrigin.position += Vector3.forward;

        }
        */
    }
}
