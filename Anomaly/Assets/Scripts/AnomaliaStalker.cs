using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class AnomaliaStalker : MonoBehaviour
{
    [Header("Referencias")]
    public PlayerInteraction camara;
    public Transform player;
    public GameObject gameOverUI;

    [Header("Movimiento")]
    public float velocidad = 5f;
    public float retrasoInicio = 1.2f;

    private bool puedeMoverse = false;
    private bool muerto = false;

    private Animator animator;
    private NavMeshAgent agent;
    private AudioSource audioSource;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        if (gameOverUI != null)
            gameOverUI.SetActive(false);

        if (agent != null)
        {
            agent.speed = velocidad;
            agent.isStopped = true;
        }

        // Este nivel se supera sobreviviendo hasta la salida.
        // Si el monstruo te alcanza, el propio script te manda al nivel 0.
        if (EstadoNivel.instancia != null)
        {
            EstadoNivel.instancia.MarcarAnomaliaResuelta();
        }
        else
        {
            Debug.LogWarning("AnomaliaStalker: no se encontró EstadoNivel en la escena.");
        }

        StartCoroutine(InicioRetrasado());
    }

    IEnumerator InicioRetrasado()
    {
        yield return new WaitForSeconds(retrasoInicio);

        puedeMoverse = true;

        if (audioSource != null)
            audioSource.Play();

        Debug.Log("AnomaliaStalker: el monstruo empieza a perseguir.");
    }

    void Update()
    {
        if (muerto)
        {
            if (Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame)
            {
                VolverAlNivel0();
            }

            return;
        }

        if (!puedeMoverse)
            return;

        if (agent == null || player == null || camara == null)
            return;

        if (!agent.isOnNavMesh)
            return;

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

    void OnTriggerEnter(Collider other)
    {
        if (muerto)
            return;

        if (other.CompareTag("Player"))
        {
            Debug.Log("El monstruo ha alcanzado al jugador.");
            Morir();
        }
    }

    void Morir()
    {
        muerto = true;

        if (agent != null)
            agent.isStopped = true;

        if (gameOverUI != null)
            gameOverUI.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Debug.Log("Has muerto. Pulsa cualquier tecla para volver al nivel 0.");
    }

    private void VolverAlNivel0()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (GameFlowManager.instancia != null)
        {
            GameFlowManager.instancia.VolverAlNivel0PorFallo();
        }
        else
        {
            SceneManager.LoadScene("0-Inicio");
        }
    }
}