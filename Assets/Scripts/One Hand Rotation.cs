using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class OneHandRotation : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private Transform _playerHead;
    [SerializeField]
    private Transform cameraTransform;
    [SerializeField]
    private bool rotateLeft = false;
    [SerializeField]
    private bool rotateRight = false;
    [SerializeField, Range(10f, 100f)]
    private float rotationSpeed = 50;
    [SerializeField]
    private TMP_Text rotationSpeedText;

    private float rotacionInput = 0f;
    private NavMeshAgent agent;

    public void enableRotateLeft()
    {
        rotateLeft = true;
    }

    public void disableRotateLeft()
    {
        rotateLeft = false;
    }

    public void enableRotateRight()
    {
        rotateRight = true;
    }

    public void disableRotateRight()
    {
        rotateRight = false;
    }

    private void setDireccionRotacion()
    {
        if (rotateLeft)
        {
            rotacionInput = -1f;
        }
        else if (rotateRight)
        {
            rotacionInput = 1f;
        }else if(!rotateLeft && !rotateRight)
        {
            rotacionInput = 0f;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = playerTransform.GetComponent<NavMeshAgent>();
        agent.angularSpeed = rotationSpeed;
        agent.updateRotation = true;
        rotationSpeedText.text = rotationSpeed.ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float rotacionY;
        setDireccionRotacion(); //Se establece el sentido de la rotaci�n (Izquierda: -1 / Derecha: 1)
        if (rotateLeft || rotateRight) //Si se detecta un gesto de rotaci�n:
        {
            agent.enabled = false; //Se desactiva el agente (evita problemas con las rutas)

            rotacionY = rotacionInput * rotationSpeed * Time.deltaTime; //Se crea la rotaci�n como el producto de
                                                                        //la velocidad de rotaci�n por el tiempo
                                                                        //por el sentido (negativo o positivo) de la misma
            Quaternion rotacion = Quaternion.Euler(0, rotacionY, 0);
            //Se realiza la rotaci�n, suaviz�ndola al usar el m�todo Slerp y el producto del tiempo por la velocidad de rotaci�n
            playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, playerTransform.rotation * rotacion, Time.deltaTime * rotationSpeed);

            if(cameraTransform != null)
            {                                                        //Para evitar problemas con la c�mara:
                cameraTransform.position = playerTransform.position; //Se cambia la posici�n de la c�mara a la posici�n del jugador
                cameraTransform.rotation = cameraTransform.rotation; //Se establece la rotaci�n de la c�mara
            }

            agent.enabled = true;
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

    public void increaseRotationSpeed()
    {
        if(rotationSpeed < 100f)
        {
            rotationSpeed++;
            rotationSpeedText.text = rotationSpeed.ToString();
        }
    }
    public void decreaseRotationSpeed()
    {
        if(rotationSpeed > 1f)
        {
            rotationSpeed--;
            rotationSpeedText.text = rotationSpeed.ToString();
        }
    }

}
