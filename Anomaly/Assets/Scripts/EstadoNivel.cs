using UnityEngine;

public class EstadoNivel : MonoBehaviour
{
    public static EstadoNivel instancia;

    [Header("Estado del nivel actual")]
    public bool anomaliaResuelta = false;

    [Header("Configuración")]
    public bool nivelSinAnomalia = false;

    private void Awake()
    {
        instancia = this;
    }

    private void Start()
    {
        if (nivelSinAnomalia)
        {
            anomaliaResuelta = true;
            Debug.Log("EstadoNivel: nivel sin anomalía, marcado como resuelto desde el inicio.");
        }
        else
        {
            Debug.Log("EstadoNivel: nivel con anomalía, pendiente de resolver.");
        }
    }

    public void MarcarAnomaliaResuelta()
    {
        anomaliaResuelta = true;
        Debug.Log("EstadoNivel: anomalía resuelta.");
    }

    public bool EstaResuelta()
    {
        return anomaliaResuelta;
    }
}