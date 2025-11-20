using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ChangeEnvironment : MonoBehaviour
{
    public List<GameObject> modelPrefabs;   // Tes prefabs
    public Transform spawnPoint;            // Où placer le modèle instancié

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

        // Sélection initiale
        dropdown.value = dropdown.choices[0];

        // Abonne l'événement
        dropdown.RegisterValueChangedCallback(evt => LoadModel(evt.newValue));

        // Charge le premier modèle
        LoadModel(dropdown.value);
    }

    private void LoadModel(string modelName)
    {
        // Détruit l'ancien modèle
        if (currentModel != null)
            Destroy(currentModel);

        // Trouve le prefab correspondant
        var prefab = modelPrefabs.Find(p => p.name == modelName);

        if (prefab != null)
        {
            currentModel = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Debug.LogWarning("Prefab introuvable pour : " + modelName);
        }
    }
}
