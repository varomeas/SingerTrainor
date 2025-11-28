using System.Collections.Generic;
using UnityEngine;

public class AudienceSpawner : MonoBehaviour
{
    public List<GameObject> personPrefabs;

    public int rows = 5;
    public int seatsPerRow = 8;

    public float rowSpacingZ = 1.0f;
    public float seatSpacingX = 0.7f;
    public float rowHeightIncrease = 0.15f;

    public Transform lookTarget;
    public float lookAtTargetOffsetY = 1.6f;

    private List<GameObject> generatedPeople = new List<GameObject>();

    public void GenerateAudience(GameObject audiencePrefab)
    {
        Clear();

        // Remplace les prefabs par ceux choisis par le menu
        personPrefabs.Clear();
        personPrefabs.Add(audiencePrefab);

        for (int r = 0; r < rows; r++)
        {
            for (int s = 0; s < seatsPerRow; s++)
            {
                // Choix du prefab (ici 1 seul, mais compatible avec plusieurs)
                GameObject prefab = personPrefabs[Random.Range(0, personPrefabs.Count)];

                // Position (ta version inversée)
                Vector3 position = new Vector3(
                    r * -rowSpacingZ,
                    r * rowHeightIncrease,
                    s * seatSpacingX
                );

                // Instanciation
                GameObject person = Instantiate(
                    prefab,
                    transform.position + position,
                    Quaternion.identity,
                    transform
                );

                generatedPeople.Add(person);

                // Rotation vers la scène
                if (lookTarget != null)
                {
                    Vector3 lookPos = lookTarget.position;
                    lookPos.y += lookAtTargetOffsetY;
                    person.transform.LookAt(lookPos);
                }
            }
        }
    }

    public void Clear()
    {
        foreach (var p in generatedPeople)
            Destroy(p);

        generatedPeople.Clear();
    }
}
