using UnityEngine;

public class PayasoFinalInteractuable : Interactable
{
    [Header("Manager del nivel 15")]
    public Nivel15PayasoManager manager;

    public override void Interact()
    {
        if (manager == null)
        {
            Debug.LogWarning("PayasoFinalInteractuable: no se ha asignado el manager.");
            return;
        }

        manager.EntregarGloboAlPayaso();
    }
}