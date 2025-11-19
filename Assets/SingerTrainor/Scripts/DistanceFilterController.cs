using UnityEngine;

public class DistanceFilterController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform playerTransform; // À assigner au joueur
    public AudioLowPassFilter lowPassFilter;

    [Header("Distance de Transition")]
    public float maxDistance = 15f; // Distance max où le filtre est actif
    public float minDistance = 5f; // Distance min où le filtre s'enlève

    [Header("Fréquences")]
    public float maxCutoffFreq = 22000f; // Fréquence max (pas de filtre)
    public float minCutoffFreq = 1800f;  // Fréquence min (son très étouffé)

    void Update()
    {
        if (playerTransform == null || lowPassFilter == null) return;

        // Calcul de la distance
        float distance = Vector3.Distance(transform.position, playerTransform.position);

        // Normalisation de la distance (entre 0 et 1) dans la zone de transition
        float t = Mathf.InverseLerp(maxDistance, minDistance, distance);

        // Mise à jour de la fréquence de coupure (Lerp)
        float newCutoff = Mathf.Lerp(minCutoffFreq, maxCutoffFreq, t);

        // Application de la nouvelle fréquence au filtre
        lowPassFilter.cutoffFrequency = newCutoff;
    }
}
