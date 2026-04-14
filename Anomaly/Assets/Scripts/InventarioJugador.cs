using UnityEngine;

public class InventarioJugador : MonoBehaviour
{
    public static InventarioJugador instancia;

    public bool tieneLlave = false;
    public bool tieneCrucifijo = false;

    private void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}