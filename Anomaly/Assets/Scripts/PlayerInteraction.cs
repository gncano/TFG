using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Distancia y capa")]
    public float interactionDistance = 3f;
    public LayerMask interactionLayer;

    [Header("Mantener pulsado")]
    private float holdTimer = 0f;

    [Header("Mirar")]
    private float viewTimer = 0f;
    public float viewTimerRequired = 10f;

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
                // Si estamos mirando a otro objeto distinto, reiniciamos el anterior si era limpiable
                if (currentInteractable != null && currentInteractable != interactable)
                {
                    LimpiarCaraVentana limpiarAnterior = currentInteractable as LimpiarCaraVentana;
                    if (limpiarAnterior != null)
                    {
                        limpiarAnterior.ResetLimpieza();
                    }
                }

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

                    if (holdTimer >= interactable.holdTime)
                    {
                        interactable.HoldCompleted();
                        holdTimer = 0f;
                    }
                }
                else
                {
                    // Si suelta la E, reinicia la limpieza si era la cara
                    LimpiarCaraVentana limpiarCara = interactable as LimpiarCaraVentana;
                    if (limpiarCara != null)
                    {
                        limpiarCara.ResetLimpieza();
                    }

                    holdTimer = 0f;
                }

                viewTimer += Time.deltaTime;

                if (viewTimer >= viewTimerRequired)
                {
                    interactable.Look();
                    viewTimer = 0f;
                }
            }
            else
            {
                // Si deja de mirar un interactuable, reinicia la limpieza si era la cara
                if (currentInteractable != null)
                {
                    LimpiarCaraVentana limpiarCara = currentInteractable as LimpiarCaraVentana;
                    if (limpiarCara != null)
                    {
                        limpiarCara.ResetLimpieza();
                    }
                }

                currentInteractable = null;
                holdTimer = 0f;
                viewTimer = 0f;
            }
        }
        else
        {
            // Si no mira a nada, reinicia la limpieza si estaba limpiando la cara
            if (currentInteractable != null)
            {
                LimpiarCaraVentana limpiarCara = currentInteractable as LimpiarCaraVentana;
                if (limpiarCara != null)
                {
                    limpiarCara.ResetLimpieza();
                }
            }

            currentInteractable = null;
            holdTimer = 0f;
            viewTimer = 0f;
        }
    }
}