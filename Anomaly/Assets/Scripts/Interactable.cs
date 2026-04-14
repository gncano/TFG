using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float holdTime = 2f; // valor por defecto

    // Interacción normal (pulso)
    public virtual void Interact()
    {
        Debug.Log("Interacción simple en " + gameObject.name);
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