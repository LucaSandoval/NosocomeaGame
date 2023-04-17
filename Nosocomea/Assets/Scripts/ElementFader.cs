using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ElementFader : MonoBehaviour
{
    public delegate void CallbackMethod();
    CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void FadeIn(CallbackMethod callbackMethod = null, float callbackDelay = 0)
    {
        canvasGroup.blocksRaycasts= true;
        StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 1, callbackMethod, callbackDelay));
    }

    public void FadeOut(CallbackMethod callbackMethod = null, float callbackDelay = 0)
    {
        canvasGroup.blocksRaycasts = false;
        StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 0, callbackMethod, callbackDelay));
    }

    public void SetAlpha(float i)
    {
        canvasGroup.alpha = i;
    }

    public float GetAlpha()
    {
        return canvasGroup.alpha;
    }

    IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float startAlpha, float endAlpha, CallbackMethod callbackMethod = null, float callbackDelay = 0, float duration = 1f)
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
        yield return new WaitForSeconds(callbackDelay);
        callbackMethod?.Invoke();
    }

}
