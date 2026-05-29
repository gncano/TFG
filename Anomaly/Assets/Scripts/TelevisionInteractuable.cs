using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TelevisionInteractuable : Interactable
{
    [Header("Manager del nivel")]
    public NivelTelevisionManager manager;

    [Header("Pantalla de la TV en el mundo")]
    public SpriteRenderer pantallaRenderer;

    [Header("Pantalla grande en UI")]
    public GameObject canvasTVGrande;
    public Image imagenTVGrande;

    [Header("Sprites de ruido")]
    public Sprite ruido01;
    public Sprite ruido02;

    [Header("Sprites de secuencia final")]
    public Sprite imagenPozo;
    public Sprite imagenPozoNina;

    [Header("Audio")]
    public AudioSource audioRuido;
    public AudioSource audioApagar;

    [Header("Jugador a bloquear durante la secuencia")]
    public GameObject jugador;
    public GameObject camaraJugador;

    private MonoBehaviour[] scriptsJugador;
    private MonoBehaviour[] scriptsCamara;

    [Header("Tiempos")]
    public float intervaloRuidoNormal = 0.08f;
    public float tiempoRuidoCorto = 0.35f;
    public float tiempoImagenPozo = 1.4f;
    public float tiempoImagenPozoNina = 1.8f;

    private bool usandoTele = false;
    private bool televisionApagada = false;
    private Coroutine rutinaRuido;

    private void Start()
    {
        if (jugador != null)
            scriptsJugador = jugador.GetComponents<MonoBehaviour>();

        if (camaraJugador != null)
            scriptsCamara = camaraJugador.GetComponents<MonoBehaviour>();

        if (pantallaRenderer != null)
        {
            pantallaRenderer.gameObject.SetActive(true);

            if (ruido01 != null)
                pantallaRenderer.sprite = ruido01;
        }

        if (canvasTVGrande != null)
            canvasTVGrande.SetActive(false);

        ReproducirRuido();

        rutinaRuido = StartCoroutine(AlternarRuidoNormal());
    }

    private IEnumerator AlternarRuidoNormal()
    {
        bool usarPrimero = true;

        while (!televisionApagada)
        {
            Sprite spriteActual = usarPrimero ? ruido01 : ruido02;
            CambiarImagenPantalla(spriteActual, false);

            usarPrimero = !usarPrimero;

            yield return new WaitForSeconds(intervaloRuidoNormal);
        }
    }

    public override void Interact()
    {
        if (usandoTele || televisionApagada)
            return;

        if (manager == null)
        {
            Debug.LogWarning("TelevisionInteractuable: no se ha asignado el manager.");
            return;
        }

        if (!manager.mandoRecogido)
        {
            Debug.Log("La tele no responde. Necesitas el mando.");
            return;
        }

        StartCoroutine(SecuenciaResolverTelevision());
    }

    private IEnumerator SecuenciaResolverTelevision()
    {
        usandoTele = true;

        BloquearJugador(true);

        if (rutinaRuido != null)
            StopCoroutine(rutinaRuido);

        if (canvasTVGrande != null)
            canvasTVGrande.SetActive(true);

        Debug.Log("Nivel TV: empieza secuencia de la televisión.");

        // Ruido
        yield return StartCoroutine(MostrarRuidoCorto(true));

        // Pozo sin
        PararRuido();

        CambiarImagenPantalla(imagenPozo, true);

        yield return new WaitForSeconds(tiempoImagenPozo);

        // Ruido
        yield return StartCoroutine(MostrarRuidoCorto(true));

        // Pozo + niña
        PararRuido();

        CambiarImagenPantalla(imagenPozoNina, true);

        yield return new WaitForSeconds(tiempoImagenPozoNina);

        // Ruido
        yield return StartCoroutine(MostrarRuidoCorto(true));

        // Apagar tele
        PararRuido();

        if (audioApagar != null)
            audioApagar.Play();

        if (pantallaRenderer != null)
            pantallaRenderer.gameObject.SetActive(false);

        if (canvasTVGrande != null)
            canvasTVGrande.SetActive(false);

        televisionApagada = true;

        if (manager != null)
            manager.ResolverTelevision();

        yield return new WaitForSeconds(0.4f);

        BloquearJugador(false);

        usandoTele = false;

        Debug.Log("Nivel TV: televisión apagada y anomalía resuelta.");
    }

    private IEnumerator MostrarRuidoCorto(bool mostrarEnGrande)
    {
        ReproducirRuido();

        if (pantallaRenderer != null)
            pantallaRenderer.gameObject.SetActive(true);

        float tiempo = 0f;
        bool usarPrimero = true;

        while (tiempo < tiempoRuidoCorto)
        {
            Sprite spriteActual = usarPrimero ? ruido01 : ruido02;

            CambiarImagenPantalla(spriteActual, mostrarEnGrande);

            usarPrimero = !usarPrimero;

            yield return new WaitForSeconds(intervaloRuidoNormal);

            tiempo += intervaloRuidoNormal;
        }

        PararRuido();
    }

    private void CambiarImagenPantalla(Sprite nuevoSprite, bool cambiarTambienGrande)
    {
        if (nuevoSprite == null)
            return;

        if (pantallaRenderer != null)
        {
            pantallaRenderer.gameObject.SetActive(true);
            pantallaRenderer.sprite = nuevoSprite;
        }

        if (cambiarTambienGrande && imagenTVGrande != null)
        {
            imagenTVGrande.sprite = nuevoSprite;
        }
    }

    private void ReproducirRuido()
    {
        if (audioRuido != null && !audioRuido.isPlaying)
            audioRuido.Play();
    }

    private void PararRuido()
    {
        if (audioRuido != null && audioRuido.isPlaying)
            audioRuido.Stop();
    }

    private void BloquearJugador(bool bloquear)
    {
        if (scriptsJugador != null)
        {
            foreach (MonoBehaviour script in scriptsJugador)
            {
                if (script != null)
                    script.enabled = !bloquear;
            }
        }

        if (scriptsCamara != null)
        {
            foreach (MonoBehaviour script in scriptsCamara)
            {
                if (script != null)
                    script.enabled = !bloquear;
            }
        }
    }
}