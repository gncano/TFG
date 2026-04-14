using UnityEngine;

public class RecogerLlave : Interactable
{
    public override void Interact()
    {
        if (InventarioJugador.instancia != null)
        {
            InventarioJugador.instancia.tieneLlave = true;
            Debug.Log("Llave recogida");
        }

        gameObject.SetActive(false);
    }
}