using UnityEngine;

public class RecogerMonedas : Interactable
{
    public override void Interact()
    {
        GameManager.instance.tieneMonedas = true;

        Debug.Log("Has recogido monedas");

        Destroy(gameObject);
    }
}