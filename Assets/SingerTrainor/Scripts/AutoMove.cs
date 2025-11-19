using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class AutoMove : MonoBehaviour
{
    // Points de chemin à suivre (à définir dans l'Inspecteur)
    public List<Transform> pathPoints; 

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
            if (agent.velocity.sqrMagnitude == 0f || Input.GetKeyDown(KeyCode.N))
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
        currentPointIndex++;
        MoveToNextPoint();
    }
}
