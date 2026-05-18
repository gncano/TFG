using System.Collections;
using UnityEngine;

public class NivelTelevisionManager : MonoBehaviour
{
    [Header("Apariciones de la niña")]
    public GameObject ninaAparicion01;
    public GameObject ninaAparicion02;
    public GameObject ninaAparicion03;
    public GameObject ninaAparicion04;

    [Header("Ajustes de desaparición")]
    public float duracionParpadeo = 1.2f;
    public float intervaloParpadeo = 0.12f;

    [Header("Estado")]
    public int aparicionActual = 1;
    public bool mandoRecogido = false;
    public bool televisionResuelta = false;
    public bool mandoDisponible = false;

    [Header("Audio opcional")]
    public AudioSource audioDesaparicionNina;

    private bool cambiandoAparicion = false;

    private void Start()
    {
        if (ninaAparicion01 != null)
            ninaAparicion01.SetActive(true);

        if (ninaAparicion02 != null)
            ninaAparicion02.SetActive(false);

        if (ninaAparicion03 != null)
            ninaAparicion03.SetActive(false);

        if (ninaAparicion04 != null)
            ninaAparicion04.SetActive(false);

        aparicionActual = 1;
        mandoRecogido = false;
        televisionResuelta = false;
        mandoDisponible = false;
        cambiandoAparicion = false;
    }

    public void ActivarSiguienteAparicion()
    {
        if (cambiandoAparicion)
            return;

        StartCoroutine(SecuenciaCambioAparicion());
    }

    private IEnumerator SecuenciaCambioAparicion()
    {
        cambiandoAparicion = true;

        GameObject ninaActual = ObtenerNinaActual();
        GameObject ninaSiguiente = ObtenerNinaSiguiente();

        if (audioDesaparicionNina != null)
            audioDesaparicionNina.Play();

        if (ninaActual != null)
            yield return StartCoroutine(ParpadearYDesaparecer(ninaActual));

        if (ninaSiguiente != null)
        {
            ninaSiguiente.SetActive(true);
            Debug.Log("Nivel TV: aparece la siguiente niña.");
        }
        else
        {
            mandoDisponible = true;
            Debug.Log("Nivel TV: última niña desaparecida. El mando ya puede recogerse.");
        }

        aparicionActual++;
        cambiandoAparicion = false;
    }

    private GameObject ObtenerNinaActual()
    {
        if (aparicionActual == 1)
            return ninaAparicion01;

        if (aparicionActual == 2)
            return ninaAparicion02;

        if (aparicionActual == 3)
            return ninaAparicion03;

        if (aparicionActual == 4)
            return ninaAparicion04;

        return null;
    }

    private GameObject ObtenerNinaSiguiente()
    {
        if (aparicionActual == 1)
            return ninaAparicion02;

        if (aparicionActual == 2)
            return ninaAparicion03;

        if (aparicionActual == 3)
            return ninaAparicion04;

        return null;
    }

    private IEnumerator ParpadearYDesaparecer(GameObject nina)
    {
        SpriteRenderer[] renderers = nina.GetComponentsInChildren<SpriteRenderer>();

        float tiempo = 0f;
        bool visible = true;

        while (tiempo < duracionParpadeo)
        {
            visible = !visible;

            foreach (SpriteRenderer sr in renderers)
            {
                if (sr != null)
                    sr.enabled = visible;
            }

            yield return new WaitForSeconds(intervaloParpadeo);
            tiempo += intervaloParpadeo;
        }

        foreach (SpriteRenderer sr in renderers)
        {
            if (sr != null)
                sr.enabled = true;
        }

        nina.SetActive(false);
    }

    public void RecogerMando()
    {
        if (!mandoDisponible)
        {
            Debug.Log("Nivel TV: todavía no puedes recoger el mando.");
            return;
        }

        mandoRecogido = true;
        Debug.Log("Nivel TV: mando recogido.");
    }

    public void ResolverTelevision()
    {
        if (televisionResuelta)
            return;

        televisionResuelta = true;
        Debug.Log("Nivel TV: televisión resuelta.");

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