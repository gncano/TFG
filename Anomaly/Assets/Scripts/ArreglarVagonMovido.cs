using UnityEngine;
using System.Collections;

public class ArreglarVagonMovido : Interactable
{
    [Header("Vagón a corregir")]
    public Transform vagon;

    [Header("Posición correcta")]
    public Vector3 posicionCorrecta;

    [Header("Movimiento")]
    public float velocidad = 1.5f;

    private bool arreglado = false;
    private bool moviendo = false;

    public override void Interact()
    {
        if (arreglado || moviendo) return;

        if (vagon == null)
        {
            Debug.Log("No se ha asignado el vagón");
            return;
        }

        StartCoroutine(MoverVagon());
    }

    private IEnumerator MoverVagon()
    {
        moviendo = true;

        while (Vector3.Distance(vagon.localPosition, posicionCorrecta) > 0.01f)
        {
            vagon.localPosition = Vector3.MoveTowards(
                vagon.localPosition,
                posicionCorrecta,
                velocidad * Time.deltaTime
            );

            yield return null;
        }

        vagon.localPosition = posicionCorrecta;

        arreglado = true;
        moviendo = false;

        Debug.Log("Vagón corregido");

        if (EstadoNivel.instancia != null)
        {
            EstadoNivel.instancia.MarcarAnomaliaResuelta();
        }
        else
        {
            Debug.LogWarning("No se encontró EstadoNivel en la escena.");
        }
    }
}