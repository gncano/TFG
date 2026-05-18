using UnityEngine;

public class Monstruo1 : MonoBehaviour
{
    public GameObject monstruo;

    private bool haSidoActivado = false;
    private bool anomaliaResuelta = false;

    void Awake()
    {
        if (monstruo != null)
            monstruo.SetActive(false);
    }

    void OnTriggerEnter(Collider player)
    {
        if (!player.CompareTag("Player"))
            return;

        Debug.Log("Dentro del rango");

        if (!haSidoActivado)
        {
            if (monstruo != null)
                monstruo.SetActive(true);

            haSidoActivado = true;
        }
    }

    void OnTriggerExit(Collider player)
    {
        if (!player.CompareTag("Player"))
            return;

        if (monstruo != null)
            monstruo.SetActive(false);

        if (haSidoActivado && !anomaliaResuelta)
        {
            anomaliaResuelta = true;

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
}