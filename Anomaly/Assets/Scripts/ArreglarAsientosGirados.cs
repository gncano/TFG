using UnityEngine;

public class ArreglarAsientosGirados : Interactable
{
    [Header("Objeto a corregir")]
    public Transform asientos;

    [Header("Transform correcto")]
    public Vector3 posicionCorrecta;
    public Vector3 rotacionCorrecta;

    private bool arreglado = false;

    public override void Interact()
    {
        if (arreglado) return;

        if (asientos == null)
        {
            Debug.Log("No se han asignado los asientos");
            return;
        }

        asientos.localPosition = posicionCorrecta;
        asientos.localRotation = Quaternion.Euler(rotacionCorrecta);

        arreglado = true;

        Debug.Log("Asientos corregidos");

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