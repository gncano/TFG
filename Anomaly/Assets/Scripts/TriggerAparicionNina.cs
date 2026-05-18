using UnityEngine;

public class TriggerAparicionNina : MonoBehaviour
{
    public NivelTelevisionManager manager;

    private bool usado = false;

    private void OnTriggerEnter(Collider other)
    {
        if (usado)
            return;

        if (!other.CompareTag("Player"))
            return;

        usado = true;

        if (manager != null)
        {
            manager.ActivarSiguienteAparicion();
        }
        else
        {
            Debug.LogWarning("TriggerAparicionNina: no se ha asignado el manager.");
        }
    }
}