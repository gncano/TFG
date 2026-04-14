using UnityEngine;

public class RecogerCrucifijo : Interactable
{
    public override void Interact()
    {
        if (InventarioJugador.instancia != null)
        {
            InventarioJugador.instancia.tieneCrucifijo = true;
            Debug.Log("Crucifijo recogido");
        }

        gameObject.SetActive(false);
    }
}