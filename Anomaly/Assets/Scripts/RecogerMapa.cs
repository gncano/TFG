using UnityEngine;

public class RecogerMapa : Interactable
{
    public override void Interact()
    {
        if (InventarioJugador.instancia != null)
        {
            InventarioJugador.instancia.tieneMapa = true;
            Debug.Log("Mapa recogido");
        }

        gameObject.SetActive(false);
    }
}