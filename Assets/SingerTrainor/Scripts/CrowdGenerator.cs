using UnityEngine;
using System.Collections.Generic;

public class CrowdGenerator : MonoBehaviour
{
    public List<GameObject> personPrefabs;     // 1 ou plusieurs modèles de personnes
    public int rows = 8;                       // Nombre de rangées
    public int seatsPerRow = 12;               // Personnes par rangée

    public float seatSpacingX = 0.9f;          // Espace horizontal entre personnes
    public float rowSpacingZ = 0.8f;           // Décalage de profondeur entre rangées
    public float rowHeightIncrease = 0.3f;     // Hauteur qui monte chaque rangée

    public float lookAtTargetOffsetY = 2.0f;   // Pour que les gens regardent légèrement vers l’avant

    public Transform lookTarget;               // La scène ou une cible devant le public

    void Start()
    {
        GenerateCrowd();
    }

    void GenerateCrowd()
    {
        for (int r = 0; r < rows; r++)
        {
            for (int s = 0; s < seatsPerRow; s++)
            {
                // Modèle aléatoire
                GameObject prefab = personPrefabs[Random.Range(0, personPrefabs.Count)];

                // Position de la personne dans sa rangée
                Vector3 position = new Vector3(
                    r * -rowSpacingZ,          // Z devient X
                    r * rowHeightIncrease,
                    s * seatSpacingX          // X devient Z
                );

                // Instanciation
                GameObject person = Instantiate(prefab, transform.position + position, Quaternion.identity, transform);

                // Ajuste la rotation pour regarder la scène
                if (lookTarget != null)
                {
                    Vector3 lookPos = lookTarget.position;
                    lookPos.y += lookAtTargetOffsetY; // on lève un peu le regard
                    person.transform.LookAt(lookPos);
                }
            }
        }
    }
}
