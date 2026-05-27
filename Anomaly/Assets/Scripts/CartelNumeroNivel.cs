using TMPro;
using UnityEngine;

public class CartelNumeroNivel : MonoBehaviour
{
    [Header("Texto del cartel")]
    public TMP_Text textoNivel;

    [Header("Formato")]
    public string prefijo = "NIVEL ";

    [Header("Si no hay GameFlowManager")]
    public int numeroFallback = 0;

    private void Start()
    {
        ActualizarTexto();
    }

    public void ActualizarTexto()
    {
        if (textoNivel == null)
        {
            textoNivel = GetComponent<TMP_Text>();
        }

        if (textoNivel == null)
        {
            Debug.LogWarning("CartelNumeroNivel: no se ha encontrado TMP_Text.");
            return;
        }

        int numeroNivel = numeroFallback;

        if (GameFlowManager.instancia != null)
        {
            numeroNivel = GameFlowManager.instancia.ObtenerNumeroRondaActual();
        }
        else
        {
            Debug.LogWarning("CartelNumeroNivel: no hay GameFlowManager. Usando numeroFallback.");
        }

        textoNivel.text = prefijo + numeroNivel;
    }
}