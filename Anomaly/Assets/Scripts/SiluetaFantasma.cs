using UnityEngine;
using System.Collections;

public class SiluetaFantasma : MonoBehaviour
{
    private Renderer miRenderer;
    private Coroutine rutinaParpadeo;

    [Header("Estado")]
    public bool anomalíaActiva = false;
    public bool jugadorMirando = false;

    [Header("Tiempos de parpadeo")]
    public float tiempoVisibleMin = 0.6f;
    public float tiempoVisibleMax = 1.2f;
    public float tiempoOcultoMin = 0.5f;
    public float tiempoOcultoMax = 1.8f;

    [Header("Retraso inicial")]
    public float retrasoInicialMin = 0f;
    public float retrasoInicialMax = 1.5f;

    private void Awake()
    {
        miRenderer = GetComponent<Renderer>();

        if (miRenderer != null)
        {
            miRenderer.enabled = false;
        }
    }

    public void ActivarAnomalia()
    {
        if (anomalíaActiva) return;

        anomalíaActiva = true;

        if (rutinaParpadeo == null)
        {
            rutinaParpadeo = StartCoroutine(ParpadeoIrregular());
        }
    }

    public void SetJugadorMirando(bool mirando)
    {
        jugadorMirando = mirando;

        if (miRenderer != null && jugadorMirando)
        {
            miRenderer.enabled = false;
        }
    }

    IEnumerator ParpadeoIrregular()
    {
        yield return new WaitForSeconds(Random.Range(retrasoInicialMin, retrasoInicialMax));

        while (anomalíaActiva)
        {
            if (!jugadorMirando)
            {
                if (miRenderer != null)
                {
                    miRenderer.enabled = true;
                }

                yield return new WaitForSeconds(Random.Range(tiempoVisibleMin, tiempoVisibleMax));

                if (miRenderer != null)
                {
                    miRenderer.enabled = false;
                }

                yield return new WaitForSeconds(Random.Range(tiempoOcultoMin, tiempoOcultoMax));
            }
            else
            {
                if (miRenderer != null)
                {
                    miRenderer.enabled = false;
                }

                yield return null;
            }
        }

        if (miRenderer != null)
        {
            miRenderer.enabled = false;
        }
    }
}