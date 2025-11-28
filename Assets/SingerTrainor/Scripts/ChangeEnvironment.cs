using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ChangeEnvironment : MonoBehaviour
{
    public List<GameObject> modelPrefabs;   // Tes prefabs
    private GameObject currentModel;        // Instance en cours
    private DropdownField dropdown;

    void Start()
    {
        var ui = GetComponent<UIDocument>();
        var root = ui.rootVisualElement;

        dropdown = root.Q<DropdownField>("modelSelector");

        // Remplit automatiquement le menu selon les noms des prefabs
        dropdown.choices = new List<string>();
        foreach (var prefab in modelPrefabs)
            dropdown.choices.Add(prefab.name);

        dropdown.value = dropdown.choices[0];

        dropdown.RegisterValueChangedCallback(evt => LoadModel(evt.newValue));

        LoadModel(dropdown.value);
    }

    private void LoadModel(string modelName)
    {
        if (currentModel != null)
            Destroy(currentModel);

        // Trouve le prefab
        GameObject prefab = modelPrefabs.Find(p => p.name == modelName);

        if (prefab == null)
        {
            Debug.LogWarning("Prefab introuvable : " + modelName);
            return;
        }

        // Cherche le Transform "SpawnPoint" dans le prefab
        Transform spawnPoint = prefab.transform.Find("SpawnPoint");

        if (spawnPoint == null)
        {
            Debug.LogWarning("Pas de SpawnPoint dans le prefab : " + prefab.name);
            // Si pas trouvé, instantiate au (0,0,0)
            currentModel = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        }
        else
        {
            // Instantie à la position du SpawnPoint du prefab
            currentModel = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
