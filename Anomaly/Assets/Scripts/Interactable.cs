using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float holdTime = 2f;

    // Interacción normal
    public virtual void Interact()
    {
        Debug.Log("Interacción simple en " + gameObject.name);
    }

    public virtual void Interact(GameObject player)
    {
        Interact();
    }

    public virtual void Look()
    {
        Debug.Log("El jugador ha mirado el tiempo necesario"+ gameObject.name);
    }

    // Mantener pulsado 
    public virtual void HoldInteract()
    {
    }

    // Cuando se completa el tiempo
    public virtual void HoldCompleted()
    {
        Debug.Log("Interacción completa en " + gameObject.name);
    }

    // Cuando se suelta la E antes de completar
    public virtual void ResetHold()
    {
    }

    protected void MarcarNivelComoResuelto()
    {
        if (EstadoNivel.instancia != null)
        {
            EstadoNivel.instancia.MarcarAnomaliaResuelta();
        }
        else
        {
            Debug.LogWarning("No se encontró EstadoNivel en la escena.");
        }
    }
}