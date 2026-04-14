using UnityEngine;

public class RecogerSpray : Interactable
{
    public override void Interact()
    {
        Debug.Log("Spray recogido");

        
        GameManager.instance.tieneSpray = true;

        
        Destroy(gameObject);
    }
}
