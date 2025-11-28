using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class LightController : MonoBehaviour
{
    public List<Light> roomLights;      // Lumières de la salle
    private RadioButtonGroup lightMenu; // ou DropdownField ou Toggle

    void Start()
    {
        var ui = GetComponent<UIDocument>();
        var root = ui.rootVisualElement;

        lightMenu = root.Q<RadioButtonGroup>("roomLights");

        // Valeur initiale
        ApplyLightState(lightMenu.value);

        // Quand l’utilisateur change
        lightMenu.RegisterValueChangedCallback(evt => ApplyLightState(evt.newValue));
    }

    void ApplyLightState(int index)
    {
        bool lightsOn = (index == 0); // Option 0 = ON, 1 = OFF (à adapter selon ton menu)

        foreach (var lamp in roomLights)
            lamp.enabled = lightsOn;
    }
}
