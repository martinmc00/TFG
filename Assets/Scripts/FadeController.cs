using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    [SerializeField]
    private Image blackPanel;
    [SerializeField]
    private TMP_Text gameOverText;
    [SerializeField]
    private GameObject OVRInteractionComprehensive;
    [SerializeField]
    private bool prueba = false;

    private float fadeDuration = 1.0f;

    private bool isFading = false;

    void Start()
    {
        blackPanel.gameObject.SetActive(true);
        Color color = blackPanel.color;
        color.a = 0f;
        blackPanel.color = color;
    }

    public void StartFadeIn()
    {
        if (!isFading)
        {
            StartCoroutine(FadeIn());
        }
    }

    private IEnumerator FadeIn()
    {
        isFading = true;
        float elapsedTime = 0f;
        Color color = blackPanel.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            gameOverText.alpha = Mathf.Clamp01(elapsedTime / fadeDuration + 0.3f);
            blackPanel.color = color;
            yield return null;
        }
        OVRInteractionComprehensive.SetActive(false);
    }

    private void Update()
    {
        if (prueba)
        {
            StartCoroutine(FadeIn());
        }
    }
}
