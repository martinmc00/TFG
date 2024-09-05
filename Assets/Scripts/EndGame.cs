using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField]
    private FadeController fadeController;

    [SerializeField]
    private DataManager dataManager;

    public void endGame()
    {
        fadeController.StartFadeIn();
        StartCoroutine(quitGame());
    }

    IEnumerator quitGame()
    {
        yield return new WaitForSeconds(3);
        while (!dataManager.isFinished())
        {
            yield return new WaitForSeconds(3);
        }
        Application.Quit();
    }
}
