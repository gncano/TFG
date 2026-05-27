using UnityEngine;

public class AnomaliaBurger : Interactable
{
    private bool resuelta = false;

    public override void Interact(GameObject player)
    {
        if (resuelta)
            return;

        Debug.Log("Comida");

        if (player == null)
        {
            player = Camera.main.transform.root.gameObject;
        }

        player.transform.localScale = new Vector3(0.61f, 1.09f, 1f);

        CharacterController cc = player.GetComponent<CharacterController>();
        if (cc != null)
        {
            cc.radius = 0.5f;
        }

        resuelta = true;

        MarcarNivelComoResuelto();
        Destroy(gameObject);
    }
}