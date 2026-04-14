using UnityEngine;

public class RecogerSpray : Interactable
{
    public override void Interact()
    {
        Debug.Log("Spray recogido");

        // Luego guardaremos que el jugador lo tiene
        GameManager.instance.tieneSpray = true;

        // Destruir el objeto
        Destroy(gameObject);
    }
}
