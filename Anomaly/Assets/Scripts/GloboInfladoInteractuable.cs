using UnityEngine;

public class GloboInfladoInteractuable : Interactable
{
    public Nivel15PayasoManager manager;

    public override void Interact()
    {
        if (manager == null)
        {
            Debug.LogWarning("GloboInfladoInteractuable: no se ha asignado el manager.");
            return;
        }

        manager.RecogerGlobo();
    }
}