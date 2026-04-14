using UnityEngine;
using UnityEngine.InputSystem;

public class ResolverJigsaw : MonoBehaviour
{
    [Header("Tiempo de interacción")]
    public float tiempoNecesario = 2.5f;
    private float tiempoActual = 0f;

    [Header("Objetos a desactivar al resolver")]
    public GameObject grupoSiluetas;
    public GameObject jigsawDetras;

    private bool jugadorDentro = false;
    private bool resuelto = false;

    private void Update()
    {
        if (resuelto || !jugadorDentro)
            return;

        if (InventarioJugador.instancia == null)
            return;

        if (!InventarioJugador.instancia.tieneCrucifijo)
            return;

        if (Keyboard.current != null && Keyboard.current.eKey.isPressed)
        {
            tiempoActual += Time.deltaTime;

            if (tiempoActual >= tiempoNecesario)
            {
                ResolverAnomalia();
            }
        }
        else
        {
            tiempoActual = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorDentro = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorDentro = false;
            tiempoActual = 0f;
        }
    }

    private void ResolverAnomalia()
    {
        resuelto = true;

        if (grupoSiluetas != null)
        {
            grupoSiluetas.SetActive(false);
        }

        if (jigsawDetras != null)
        {
            jigsawDetras.SetActive(false);
        }

        gameObject.SetActive(false);

        Debug.Log("Anomalía resuelta");
    }
}