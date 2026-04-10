using UnityEngine;

public class Interactable : MonoBehaviour
{
    // Interacción normal (pulso)
    public virtual void Interact()
    {
        Debug.Log("Interacción simple en " + gameObject.name);
    }
    public virtual void Interact(GameObject player)
    {
        Debug.Log("Interacción simple en " + gameObject.name);
    }

    public virtual void Look()
    {
        Debug.Log("El jugador ha mirado el tiempo necesario"+ gameObject.name);
    }

    // Mantener pulsado (cada frame mientras se mantiene)
    public virtual void HoldInteract()
    {
       
    }

    // Cuando se completa el tiempo de hold
    public virtual void HoldCompleted()
    {
        Debug.Log("Interacción completa en " + gameObject.name);
    }

}