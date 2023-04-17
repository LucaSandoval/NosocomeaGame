using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScreenFader : MonoBehaviour
{
    public delegate void CallbackMethod();
    CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void FadeIn()
    {
        StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 1));
    }

    public void FadeOut()
    {
        StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 0));
    }

    public void FadeIn(CallbackMethod callbackMethod)
    {
        StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 1, callbackMethod));
    }

    public void FadeOut(CallbackMethod callbackMethod)
    {
        StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 0, callbackMethod));
    }

    public void SetAlpha(float i)
    {
        canvasGroup.alpha = i;
    }

    IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float startAlpha, float endAlpha, CallbackMethod callbackMethod = null, float duration = 1f)
    {
        float startTime = Time.time;
        float endTime = startTime + duration;

        while (Time.time < endTime)
        {
            float timeRatio = (Time.time - startTime) / duration;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, timeRatio);
            yield return null;
        }

        canvasGroup.alpha = endAlpha;
        callbackMethod?.Invoke();
    }

}
