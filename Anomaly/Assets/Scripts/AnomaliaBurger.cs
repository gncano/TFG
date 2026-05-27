using UnityEngine;

public class AnomaliaBurger : Interactable
{
    public override void Interact(GameObject player)
    {
        Debug.Log("Comida");
        GameObject realPlayer = Camera.main.transform.root.gameObject;

        realPlayer.transform.localScale = new Vector3(0.61f, 1.09f, 1f);

        CharacterController cc = realPlayer.GetComponent<CharacterController>();
        if (cc != null)
        {
            cc.radius = 0.5f;
        }

        Destroy(gameObject);
    }
}