using UnityEngine;

public class LimpiarCaraVentana : Interactable
{
    [Header("Referencias")]
    public GameObject caraExterior;
    public GameObject caraInterior;

    [Header("Renderers")]
    public MeshRenderer rendererCaraInterior;
    public MeshRenderer rendererCaraExterior;

    [Header("Ajustes de limpieza")]
    public float tiempoLimpieza = 4f;

    [Header("Audio")]
    public AudioSource audioLimpiar;

    private Material materialInterior;
    private Material materialExterior;

    private Color colorInteriorInicial;
    private Color colorExteriorInicial;

    private float progresoLimpieza = 0f;
    private bool puedeLimpiar = false;
    private bool limpiezaCompletada = false;

    void Start()
    {
        if (audioLimpiar == null)
            audioLimpiar = GetComponent<AudioSource>();

        if (rendererCaraInterior != null)
        {
            materialInterior = rendererCaraInterior.material;
            colorInteriorInicial = materialInterior.color;
        }

        if (rendererCaraExterior != null)
        {
            materialExterior = rendererCaraExterior.material;
            colorExteriorInicial = materialExterior.color;
        }
    }

    public override void Interact()
    {
        if (limpiezaCompletada)
            return;

        if (GameManager.instance == null)
        {
            Debug.LogWarning("No hay GameManager en la escena");
            return;
        }

        if (GameManager.instance.tieneSpray && GameManager.instance.tieneTrapo)
        {
            puedeLimpiar = true;
        }
        else
        {
            puedeLimpiar = false;
            DetenerSonidoLimpieza();
            Debug.Log("Te falta el spray o el trapo");
        }
    }

    public override void HoldInteract()
    {
        if (!puedeLimpiar || limpiezaCompletada)
            return;

        ReproducirSonidoLimpieza();

        progresoLimpieza += Time.deltaTime;

        float alpha = Mathf.Clamp01(1f - (progresoLimpieza / tiempoLimpieza));
        AplicarTransparencia(alpha);
    }

    public override void HoldCompleted()
    {
        if (!puedeLimpiar || limpiezaCompletada)
            return;

        limpiezaCompletada = true;

        DetenerSonidoLimpieza();

        Debug.Log("Cara limpiada completamente");

        if (caraInterior != null)
            Destroy(caraInterior);

        if (caraExterior != null)
            Destroy(caraExterior);

        MarcarNivelComoResuelto();
    }

    public void ResetLimpieza()
    {
        if (limpiezaCompletada)
            return;

        DetenerSonidoLimpieza();

        progresoLimpieza = 0f;
        puedeLimpiar = false;
        AplicarTransparencia(1f);
    }

    private void AplicarTransparencia(float alpha)
    {
        if (materialInterior != null)
        {
            Color c = colorInteriorInicial;
            c.a = alpha;
            materialInterior.color = c;
        }

        if (materialExterior != null)
        {
            Color c = colorExteriorInicial;
            c.a = alpha;
            materialExterior.color = c;
        }
    }

    private void ReproducirSonidoLimpieza()
    {
        if (audioLimpiar != null && !audioLimpiar.isPlaying)
        {
            audioLimpiar.Play();
        }
    }

    private void DetenerSonidoLimpieza()
    {
        if (audioLimpiar != null && audioLimpiar.isPlaying)
        {
            audioLimpiar.Stop();
        }
    }
}