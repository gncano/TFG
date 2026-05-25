using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class AnomaliaStalker : MonoBehaviour
{

    public PlayerInteraction camara;
    public Transform player;
    public float velocidad = 5f;
    private bool puedeMoverse = false;
    private Animator animator;
    private NavMeshAgent agent;

// Start is called once before the first execution of Update after the MonoBehaviour is created
void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(InicioRetrasado());
        animator = GetComponent<Animator>();
    }

    IEnumerator InicioRetrasado()
    {
        yield return new WaitForSeconds(8f);

        puedeMoverse = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!puedeMoverse) return;

        if (!camara.estaMirando(gameObject))
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }
        else
        {
            agent.isStopped = true;
        }
    }
}