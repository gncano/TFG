using System.Collections;
using TMPro;
using UnityEngine;

public class TextoFinalProgresivo : MonoBehaviour
{
    [Header("Texto")]
    public TMP_Text textoFinal;

    [TextArea(5, 12)]
    public string textoCompleto;

    [Header("Velocidad")]
    public float velocidadEscritura = 0.035f;

    private Coroutine rutinaEscritura;

    private void OnEnable()
    {
        if (textoFinal == null)
            textoFinal = GetComponent<TMP_Text>();

        if (textoFinal == null)
        {
            Debug.LogWarning("TextoFinalProgresivo: no se encontró TMP_Text.");
            return;
        }

        if (rutinaEscritura != null)
            StopCoroutine(rutinaEscritura);

        rutinaEscritura = StartCoroutine(EscribirTexto());
    }

    private IEnumerator EscribirTexto()
    {
        textoFinal.text = textoCompleto;
        textoFinal.ForceMeshUpdate();
        textoFinal.maxVisibleCharacters = 0;

        int totalCaracteres = textoFinal.textInfo.characterCount;

        for (int i = 0; i <= totalCaracteres; i++)
        {
            textoFinal.maxVisibleCharacters = i;
            yield return new WaitForSeconds(velocidadEscritura);
        }
    }
}