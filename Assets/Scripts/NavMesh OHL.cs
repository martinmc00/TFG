using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshOHL : MonoBehaviour
{

    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private Transform _playerHead;
    [SerializeField]
    private bool moveForward = false;
    [SerializeField]
    private bool moveBackwards = false;
    [SerializeField, Range(3.5f, 10f)]
    private float speed = 7f;
    [SerializeField]
    private TMP_Text speedText;

    private NavMeshAgent agent;
    private Vector3 direccionMovimiento;
    private Vector3 tempPosition;
    private float LerpTime = 0f;

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

    // Start is called before the first frame update
    void Start()
    {
        agent = playerTransform.gameObject.GetComponent<NavMeshAgent>();
        agent.speed = speed;
        speedText.text = speed.ToString();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        direccionMovimiento = _playerHead.forward; //Se toma el vector en el que mira el usuario
        if(direccionMovimiento.magnitude >= 0.1f && !agent.isStopped) //Si se detecta un movimiento significativo
                                                                      //y el agente no está parado:
        {
            Vector3 destino = transform.position;
            if(moveForward) //Si se detecta el movimiento hacia adelante
            {
                destino = transform.position + direccionMovimiento * speed * Time.deltaTime; //Se establece el destino como el resultado
                                                                                             //de la adición de la posición actual con la
                                                                                             //dirección en la que mira el usuario,
                                                                                             //multiplicado por el tiempo y la velocidad
            }
            else if (moveBackwards) //Si se detecta el movimiento hacia atrás
            {
                direccionMovimiento = -direccionMovimiento; //La dirección de movimiento se establece como negativa
                                                            //para que el desplazamiento sea inverso
                destino = transform.position + direccionMovimiento * speed * Time.deltaTime;
            }
            agent.SetDestination(destino); //Se indica el destino al NavMesh Agent
            agent.updateRotation = false;
        }
        else
        {
            agent.SetDestination(transform.position); //Si no se detecta movimiento significativo y/o el agente está parado,
                                                      //se actualiza la posición a la actual del jugador (evita problemas de rutas)
        }
    }

    private void LateUpdate()
    {
        if(agent.velocity.magnitude > 0.01f)
        {
            Vector3 lookDirection = new Vector3(agent.velocity.x, 0, agent.velocity.z);
            transform.rotation = Quaternion.LookRotation(lookDirection);
        }
    }

    public void alternateEnable()
    {
        if (this.enabled)
        {
            this.enabled = false;
        }
        else
        {
            this.enabled = true;
        }
    }

    public void increaseSpeed()
    {
        if(speed < 10f)
        {
            speed++;
            speedText.text = speed.ToString();
        }
    }

    public void decreaseSpeed()
    {
        if(speed > 3.5f)
        {
            speed--;
            speedText.text = speed.ToString();
        }
    }


}
