using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private float elapsedTime;
    private float slElapsedTime;
    private float clElapsedTime;
    private bool timerRunning;
    private bool standardLocomotionTimerRunning;
    private string finalTime;
    private bool finished;

    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = slElapsedTime = clElapsedTime = 0f;
        timerRunning = standardLocomotionTimerRunning = true;
        finalTime = "";
        finished = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerRunning) //Si no se para el contador
        {
            elapsedTime += Time.deltaTime; //Se incrementa el temporizador del tiempo total transcurrido
        }

        if (!standardLocomotionTimerRunning) //Si se está usando el método de movimiento continuo
        {
            clElapsedTime += Time.deltaTime; //Se incrementa el temporizador correspondiente
        }
        else                                 //Si se está usando el método de movimiento por teletransporte
        {
            slElapsedTime += Time.deltaTime; //Se incrementa el temporizador correspondiente   
        }
    }

    public void stopTimer()
    {
        timerRunning = false;
        finalTime = formatTimeToString(elapsedTime);
    }

    public void setSLTimer(bool state)
    {
        standardLocomotionTimerRunning = state;
    }

    public string getFinalTime()
    {
        return finalTime;
    }

    public string getSLFinalTime()
    {
        return formatTimeToString(slElapsedTime);
    }
    public string getCLFinalTime()
    {
        return formatTimeToString(clElapsedTime);
    }

    public void setFinished(bool state)
    {
        finished = state;
    }

    public bool isFinished()
    {
        return finished;
    }

    private string formatTimeToString(float time)
    {
        int minutes = (int)(time / 60f);
        int seconds = (int)(time - minutes * 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
