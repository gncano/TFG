using System.Collections;
using UnityEngine;

public class PuertaVagonDoble : Interactable
{
    [Header("Referencias")]
    public Transform puertaIzquierda;
    public Transform puertaDerecha;

    [Header("Posiciones cerradas")]
    public Vector3 posicionCerradaIzquierda;
    public Vector3 posicionCerradaDerecha;

    [Header("Posiciones abiertas")]
    public Vector3 posicionAbiertaIzquierda;
    public Vector3 posicionAbiertaDerecha;

    [Header("Ajustes")]
    public float velocidad = 2f;

    [Header("Configuración de nivel")]
    public bool requiereLlave = false;

    [Header("Evento tras abrir")]
    public bool activarEventoTrasAbrir = false;
    public float retrasoEvento = 2f;
    public AudioSource audioTriciclo;
    public GameObject jigsaw;

    public bool abierta = false;
    private bool moviendo = false;
    private static bool eventoLanzado = false;
    public override void Interact()
    {
        if (puertaIzquierda == null || puertaDerecha == null)
            return;

        if (abierta || moviendo)
            return;

        if (requiereLlave)
        {
            if (InventarioJugador.instancia == null)
            {
                Debug.Log("No se encontró InventarioJugador");
                return;
            }

            if (!InventarioJugador.instancia.tieneLlave)
            {
                Debug.Log("La puerta está cerrada. Necesitas la llave.");
                return;
            }
        }

        StartCoroutine(MoverPuertas(posicionAbiertaIzquierda, posicionAbiertaDerecha));
    }

    private IEnumerator MoverPuertas(Vector3 destinoIzquierda, Vector3 destinoDerecha)
    {
        moviendo = true;

        while (Vector3.Distance(puertaIzquierda.localPosition, destinoIzquierda) > 0.01f ||
               Vector3.Distance(puertaDerecha.localPosition, destinoDerecha) > 0.01f)
        {
            puertaIzquierda.localPosition = Vector3.Lerp(
                puertaIzquierda.localPosition,
                destinoIzquierda,
                velocidad * Time.deltaTime
            );

            puertaDerecha.localPosition = Vector3.Lerp(
                puertaDerecha.localPosition,
                destinoDerecha,
                velocidad * Time.deltaTime
            );

            yield return null;
        }

        puertaIzquierda.localPosition = destinoIzquierda;
        puertaDerecha.localPosition = destinoDerecha;

        abierta = true;
        moviendo = false;

        Debug.Log("Puertas abiertas");

        if (activarEventoTrasAbrir && !eventoLanzado)
        {
            eventoLanzado = true;
            StartCoroutine(EventoTrasAbrir());
        }
    }

    private IEnumerator EventoTrasAbrir()
    {
        yield return new WaitForSeconds(retrasoEvento);

        if (audioTriciclo != null)
        {
            audioTriciclo.Play();
        }

        if (jigsaw != null)
        {
            jigsaw.SetActive(true);
        }

        Debug.Log("Evento del triciclo/Jigsaw activado");
    }
}