using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VRFade : MonoBehaviour
{
    // Le composant qui contrôle l'opacité du panneau noir
    private CanvasGroup canvasGroup;

    // Assurez-vous d'avoir ce composant sur l'objet qui contient ce script
    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("VRFade nécessite un composant CanvasGroup sur le même GameObject.");
        }
    }

    /// <summary>Déclenche un fondu au noir.</summary>
    /// <param name="duration">Durée de la transition en secondes.</param>
    public IEnumerator FadeOut(float duration)
    {
        float startAlpha = canvasGroup.alpha;
        float rate = 1.0f / duration;
        float t = 0.0f;

        while (t < 1.0f)
        {
            t += Time.deltaTime * rate;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 1f, t);
            yield return null;
        }
        canvasGroup.alpha = 1f; // Assure que c'est complètement noir
    }

    /// <summary>Déclenche un fondu d'apparition (du noir à la vision normale).</summary>
    /// <param name="duration">Durée de la transition en secondes.</param>
    public IEnumerator FadeIn(float duration)
    {
        float startAlpha = canvasGroup.alpha;
        float rate = 1.0f / duration;
        float t = 0.0f;

        while (t < 1.0f)
        {
            t += Time.deltaTime * rate;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, t);
            yield return null;
        }
        canvasGroup.alpha = 0f; // Assure que c'est complètement transparent
    }
}
