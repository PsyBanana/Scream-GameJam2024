using UnityEngine;
using System.Collections;

public class UIFade : MonoBehaviour
{
    CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponentInChildren<CanvasGroup>();
    }

    private void OnEnable()
    {
        _canvasGroup.alpha = 1f;
        Fade(1f, 0f, 0.1f);
    }

    public Coroutine Fade(float from, float to, float duration)
    {
        return StartCoroutine(FadeRoutine(from, to, duration));
    }

    IEnumerator FadeRoutine(float from, float to, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            _canvasGroup.alpha = Mathf.Lerp(from, to, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _canvasGroup.alpha = to;
    }

    public void Set(float opacity)
    {
        _canvasGroup.alpha = opacity;
    }
}
