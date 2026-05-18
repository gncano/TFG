using UnityEngine;
using UnityEngine.InputSystem;

public class GloboDesinfladoInteractuable : Interactable
{
    [Header("Configuración del globo")]
    public Nivel15PayasoManager manager;
    public AudioSource audioInflar;

    [Header("Opcional: pequeño efecto visual mientras se infla")]
    public Transform objetoVisualGlobo;
    public float escalaMaximaDuranteInflado = 1.08f;

    private Vector3 escalaInicial;
    private bool yaInflado = false;
    private float ultimoMomentoHold = 0f;

    private void Start()
    {
        holdTime = 5f;

        if (audioInflar == null)
            audioInflar = GetComponent<AudioSource>();

        if (objetoVisualGlobo == null)
            objetoVisualGlobo = transform;

        escalaInicial = objetoVisualGlobo.localScale;
    }

    private void Update()
    {
        if (yaInflado)
            return;

        bool seEstaManteniendoE = Keyboard.current != null && Keyboard.current.eKey.isPressed;

        bool hacePocoQueSeLlamoHold = Time.time - ultimoMomentoHold < 0.15f;

        if (!seEstaManteniendoE || !hacePocoQueSeLlamoHold)
        {
            DetenerInfladoVisualYSonido();
        }
    }

    public override void HoldInteract()
    {
        if (yaInflado)
            return;

        ultimoMomentoHold = Time.time;

        if (audioInflar != null && !audioInflar.isPlaying)
            audioInflar.Play();

        if (objetoVisualGlobo != null)
        {
            float factor = 1f + Mathf.PingPong(Time.time * 0.8f, escalaMaximaDuranteInflado - 1f);
            objetoVisualGlobo.localScale = escalaInicial * factor;
        }
    }

    public override void HoldCompleted()
    {
        if (yaInflado)
            return;

        yaInflado = true;

        if (audioInflar != null)
            audioInflar.Stop();

        if (objetoVisualGlobo != null)
            objetoVisualGlobo.localScale = escalaInicial;

        if (manager != null)
        {
            manager.InflarGlobo();
        }
        else
        {
            Debug.LogWarning("GloboDesinfladoInteractuable: no se ha asignado el manager.");
        }
    }

    private void DetenerInfladoVisualYSonido()
    {
        if (audioInflar != null && audioInflar.isPlaying)
            audioInflar.Stop();

        if (objetoVisualGlobo != null)
            objetoVisualGlobo.localScale = escalaInicial;
    }
}