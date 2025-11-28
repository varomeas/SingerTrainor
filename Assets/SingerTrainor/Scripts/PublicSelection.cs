using UnityEngine;
using UnityEngine.UIElements;

public class PublicSelection : MonoBehaviour
{
    public GameObject fullPublic;         // Le public complet
    public GameObject examinersOnly;      // Seulement les examinateurs

    private RadioButtonGroup radioGroup;

    void Start()
    {
        var ui = GetComponent<UIDocument>();
        var root = ui.rootVisualElement;

        // On récupère le RadioButtonGroup par son name
        radioGroup = root.Q<RadioButtonGroup>("publicModeSelector");

        // On écoute quand l'utilisateur change d'option
        radioGroup.RegisterValueChangedCallback(evt => OnModeChanged(evt.newValue));

        // Applique l'état initial selon la sélection par défaut
        OnModeChanged(radioGroup.value);
    }

    void OnModeChanged(int index)
    {
        // index = option choisie (0,1,2...)
        switch (index)
        {
            case 0: // Public complet
                fullPublic.SetActive(true);
                examinersOnly.SetActive(false);
                break;

            case 1: // Examinateurs
                fullPublic.SetActive(false);
                examinersOnly.SetActive(true);
                break;

            default:
                Debug.LogWarning("Option inconnue dans le RadioButtonGroup");
                break;
        }
    }
}
