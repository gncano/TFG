using UnityEngine;

public class AnomaliaBurger : Interactable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Interact(GameObject player)
    {
        Debug.Log("Comida");
        Destroy(gameObject);

        player = Camera.main.transform.root.gameObject;
        player.transform.localScale = new Vector3(0.61f, 1.09f, 1f);
        CharacterController cc = player.GetComponent<CharacterController>();
        cc.radius = 0.5f;
    }
}