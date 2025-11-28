using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    [Header("Lights")]
    public List<Light> lights;

    [Header("Audience Prefabs")]
    public List<GameObject> audiencePrefabs; // <-- le public propre à cet environnement

    [Header("Audience Spawners")]
    public List<AudienceSpawner> audienceSpawners;

    public void SetLights(bool on)
    {
        foreach (var l in lights)
            l.enabled = on;
    }

    public void SetAudience(int index)
    {
        if (index < 0 || index >= audiencePrefabs.Count)
        {
            Debug.LogWarning("Audience index invalide pour " + name);
            return;
        }

        GameObject prefab = audiencePrefabs[index];

        foreach (var spawner in audienceSpawners)
        {
            spawner.GenerateAudience(prefab);
        }
    }
}
