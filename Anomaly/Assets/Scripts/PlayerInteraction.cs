using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Distancia y capa")]
    public float interactionDistance = 3f;        
    public LayerMask interactionLayer;            

    [Header("Hold Interaction")]
    public float holdTimeRequired = 2f;          
    private float holdTimer = 0f;

    private Interactable currentInteractable;

    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * interactionDistance, Color.red);
        
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance, interactionLayer))
        {
           
            Interactable interactable = hit.collider.GetComponent<Interactable>();

            if (interactable != null)
            {
                currentInteractable = interactable;
               
                if (Keyboard.current.eKey.wasPressedThisFrame)
                {
                    interactable.Interact();
                    holdTimer = 0f; 
                }

               
                if (Keyboard.current.eKey.isPressed)
                {
                    holdTimer += Time.deltaTime;
                    interactable.HoldInteract();

                  
                    if (holdTimer >= holdTimeRequired)
                    {
                        interactable.HoldCompleted();
                        holdTimer = 0f;
                    }
                }
                else
                {
                    holdTimer = 0f; 
                }
            }
            else
            {
                currentInteractable = null;
                holdTimer = 0f;
            }
        }
        else
        {
            currentInteractable = null;
            holdTimer = 0f;
        }
    }
}