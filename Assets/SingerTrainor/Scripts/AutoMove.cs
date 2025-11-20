using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class AutoMove : MonoBehaviour
{
    // Points de chemin à suivre (à définir dans l'Inspecteur)
    public List<Transform> pathPoints; 

    
    public VRFade vrFade; // **Glissez-y le FadePanel**
    public Transform teleportTarget; // **Glissez-y le GameObject de destination finale**
    public float fadeDuration = 1.5f; // Durée du fondu en secondes
    private bool isPathCompleted = false;

    private UnityEngine.AI.NavMeshAgent agent;
    private int currentPointIndex = 0;

    void Start()
    {
        // Récupérer le NavMeshAgent attaché à cet objet
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        // Lancer le déplacement vers le premier point
        if (pathPoints.Count > 0)
        {
            MoveToNextPoint();
        }
    }

    void Update()
    {
        // Vérifier si l'agent a atteint sa destination actuelle
        // 'agent.remainingDistance' doit être inférieur à 'agent.stoppingDistance'
        // et l'agent ne doit pas calculer de nouveau chemin ('agent.pathPending')

        // Vérifie s'il ne bouge plus (pour éviter des bugs de très petite distance)
        if (isPathCompleted) return;

        if (agent.velocity.sqrMagnitude == 0f)
            {
                Debug.Log("N pressé");
                GoToNextPointInPath();
            }
        
    }

    // Définit la destination de l'agent
    void MoveToNextPoint()
    {
        if (currentPointIndex < pathPoints.Count)
        {
            // Assigne la position du 'Transform' au NavMeshAgent
            agent.SetDestination(pathPoints[currentPointIndex].position);
            Debug.Log("Déplacement vers le point : " + currentPointIndex);
        }
        else
        {
            Debug.Log("Fin du chemin.");
            // OPTIONNEL : Désactiver le mouvement ou faire quelque chose à la fin
            // agent.isStopped = true; 
        }
    }

    // Incrémente l'index et appelle MoveToNextPoint
    void GoToNextPointInPath()
    {
        // VÉRIFICATION DU DERNIER POINT
        if (currentPointIndex == pathPoints.Count)
        {
            // Arrêter l'agent et lancer la séquence de fin
            agent.isStopped = true;
            StartCoroutine(EndPathSequence());
        }
        else // Sinon, continuer le chemin
        {
            currentPointIndex++;
            MoveToNextPoint();
        }
    }

    IEnumerator EndPathSequence()
    {
        Debug.Log("Fin de chemin atteinte. Lancement du fondu.");

        // 1. Démarrer le fondu au noir
        yield return StartCoroutine(vrFade.FadeOut(fadeDuration));

        // 2. Changer la position du joueur (téléportation instantanée)
        if (teleportTarget != null)
        {
            transform.position = teleportTarget.position;

           /* float rotation = teleportTarget.rotation.eulerAngles.y;

            float newrotation = rotation - 90f;

            Quaternion finalRotation = Quaternion.Euler(teleportTarget.rotation.eulerAngles.x, newrotation, teleportTarget.rotation.eulerAngles.z);*/

            

            transform.rotation = teleportTarget.rotation;
            

            // Réinitialiser l'agent pour qu'il soit en phase avec la nouvelle position
            agent.Warp(teleportTarget.position);

            Debug.Log("Téléportation effectuée.");
        }
        else
        {
            Debug.LogError("Le Transform de destination de téléportation est manquant!");
        }

        // 3. Réapparition progressive
        yield return StartCoroutine(vrFade.FadeIn(fadeDuration));

        Debug.Log("Séquence terminée. Nouvelle position de départ.");

        // Vous pouvez ajouter ici le code pour lancer une nouvelle séquence, charger un nouveau niveau, etc.
        isPathCompleted = true;
    }
}
