using UnityEngine;

public class RecogerTrapo : Interactable
{
    public override void Interact()
    {
        Debug.Log("Trapo recogido");

        GameManager.instance.tieneTrapo = true;

        Destroy(gameObject);
    }
}
