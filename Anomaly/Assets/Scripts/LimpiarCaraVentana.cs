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

    private Material materialInterior;
    private Material materialExterior;

    private Color colorInteriorInicial;
    private Color colorExteriorInicial;

    private float progresoLimpieza = 0f;
    private bool puedeLimpiar = false;
    private bool limpiezaCompletada = false;

    void Start()
    {
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
        if (limpiezaCompletada) return;

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
            Debug.Log("Te falta el spray o el trapo");
        }
    }

    public override void HoldInteract()
    {
        if (!puedeLimpiar || limpiezaCompletada) return;

        progresoLimpieza += Time.deltaTime;

        float alpha = Mathf.Clamp01(1f - (progresoLimpieza / tiempoLimpieza));
        AplicarTransparencia(alpha);
    }

    public override void HoldCompleted()
    {
        if (!puedeLimpiar || limpiezaCompletada) return;

        limpiezaCompletada = true;
        Debug.Log("Cara limpiada completamente");

        if (caraInterior != null)
            Destroy(caraInterior);

        if (caraExterior != null)
            Destroy(caraExterior);
    }

    public void ResetLimpieza()
    {
        if (limpiezaCompletada) return;

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
}