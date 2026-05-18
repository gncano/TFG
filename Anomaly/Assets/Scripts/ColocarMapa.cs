using UnityEngine;

public class ColocarMapa : Interactable
{
    [Header("Mapa que aparecerá en la pared")]
    public GameObject mapaEnPared;

    private bool colocado = false;

    public override void Interact()
    {
        Colocar();
    }

    public override void Interact(GameObject player)
    {
        Colocar();
    }

    private void Colocar()
    {
        Debug.Log("Intentando colocar mapa");

        if (colocado) return;

        if (InventarioJugador.instancia == null)
        {
            Debug.Log("No se encontró InventarioJugador");
            return;
        }

        if (!InventarioJugador.instancia.tieneMapa)
        {
            Debug.Log("No tienes el mapa");
            return;
        }

        if (mapaEnPared == null)
        {
            Debug.Log("Mapa En Pared no está asignado");
            return;
        }

        InventarioJugador.instancia.tieneMapa = false;
        mapaEnPared.SetActive(true);

        colocado = true;

        Debug.Log("Mapa colocado");

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