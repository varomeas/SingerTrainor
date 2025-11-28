using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnvironmentManager : MonoBehaviour
{
    public List<EnvironmentController> environments;

    private int currentEnv = 0;

    private DropdownField envDropdown;
    private RadioButtonGroup lightSelector;
    private RadioButtonGroup audienceSelector;

    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        envDropdown = root.Q<DropdownField>("modelSelector");
        lightSelector = root.Q<RadioButtonGroup>("roomLights");
        audienceSelector = root.Q<RadioButtonGroup>("publicModeSelector");

        // Remplir la liste des noms d'environnement
        envDropdown.choices = new List<string>();
        foreach (var env in environments)
            envDropdown.choices.Add(env.name);

        envDropdown.value = envDropdown.choices[0];
        envDropdown.RegisterValueChangedCallback(evt => ChangeEnvironment(evt.newValue));

        lightSelector.RegisterValueChangedCallback(evt => ApplyLight());
        audienceSelector.RegisterValueChangedCallback(evt => ApplyAudience());

        ChangeEnvironment(envDropdown.value);
    }

    void ChangeEnvironment(string name)
    {
        currentEnv = envDropdown.choices.IndexOf(name);
        ApplyLight();
        ApplyAudience();
    }

    void ApplyLight()
    {
        bool on = (lightSelector.value == 0);
        environments[currentEnv].SetLights(on);
    }

    void ApplyAudience()
    {
        int index = audienceSelector.value;
        environments[currentEnv].SetAudience(index);
    }
}
