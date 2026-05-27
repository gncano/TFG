using System.Collections;
using UnityEngine;

public class Nivel15PayasoManager : MonoBehaviour
{
    [Header("Estado del nivel")]
    public bool globoInflado = false;
    public bool globoRecogido = false;
    public bool globoEntregado = false;
    public bool finalActivado = false;

    [Header("Objetos del globo")]
    public GameObject globoDesinflado;
    public GameObject globoInfladoObj;

    [Header("Objetos del payaso")]
    public GameObject payasoSentadoTriste;
    public GameObject payasoSentadoConGlobo;
    public GameObject payasoDePieFinal;

    [Header("Puertas finales")]
    public PuertaVagonDoble puertaFinal;

    [Header("Cámaras")]
    public GameObject camaraJugador;
    public GameObject camaraFinal;

    [Header("Jugador a bloquear")]
    public GameObject jugador;
    public GameObject visualJugador;
    public Renderer rendererJugador;

    private MonoBehaviour[] scriptsJugador;
    private MonoBehaviour[] scriptsCamaraJugador;

    [Header("Audio final")]
    public AudioSource audioTrenFinal;

    [Header("UI final")]
    public GameObject canvasFinal;

    [Header("Tiempos de la secuencia final")]
    public float tiempoViendoPayasoSentadoConGlobo = 3f;
    public float tiempoViendoPayasoDePie = 3f;
    public float tiempoTemblorAntesNegro = 3f;

    [Header("Temblor cámara final")]
    public float intensidadTemblor = 0.04f;
    public float velocidadTemblor = 25f;

    private Vector3 posicionInicialCamaraFinal;

    private void Start()
    {
        if (globoDesinflado != null)
            globoDesinflado.SetActive(true);

        if (globoInfladoObj != null)
            globoInfladoObj.SetActive(false);

        if (payasoSentadoTriste != null)
            payasoSentadoTriste.SetActive(true);

        if (payasoSentadoConGlobo != null)
            payasoSentadoConGlobo.SetActive(false);

        if (payasoDePieFinal != null)
            payasoDePieFinal.SetActive(false);

        if (canvasFinal != null)
            canvasFinal.SetActive(false);

        if (camaraFinal != null)
            camaraFinal.SetActive(false);

        if (jugador != null)
            scriptsJugador = jugador.GetComponents<MonoBehaviour>();

        if (camaraJugador != null)
            scriptsCamaraJugador = camaraJugador.GetComponents<MonoBehaviour>();

        if (camaraFinal != null)
            posicionInicialCamaraFinal = camaraFinal.transform.localPosition;
    }

    public void InflarGlobo()
    {
        globoInflado = true;

        if (globoDesinflado != null)
            globoDesinflado.SetActive(false);

        if (globoInfladoObj != null)
            globoInfladoObj.SetActive(true);

        Debug.Log("Nivel 15: globo inflado.");
    }

    public void RecogerGlobo()
    {
        if (!globoInflado)
        {
            Debug.Log("Nivel 15: todavía no puedes recoger el globo, no está inflado.");
            return;
        }

        globoRecogido = true;

        if (globoInfladoObj != null)
            globoInfladoObj.SetActive(false);

        Debug.Log("Nivel 15: globo recogido.");
    }

    public void EntregarGloboAlPayaso()
    {
        if (!globoRecogido)
        {
            Debug.Log("Nivel 15: no tienes el globo todavía.");
            return;
        }

        if (globoEntregado)
            return;

        globoEntregado = true;
        finalActivado = true;

        StartCoroutine(SecuenciaFinal());
    }

    private IEnumerator SecuenciaFinal()
    {
        Debug.Log("Nivel 15: empieza secuencia final.");

        BloquearJugador(true);

        if (visualJugador != null)
            visualJugador.SetActive(false);

        if (rendererJugador != null)
            rendererJugador.enabled = false;

        if (payasoSentadoTriste != null)
            payasoSentadoTriste.SetActive(false);

        if (payasoSentadoConGlobo != null)
            payasoSentadoConGlobo.SetActive(true);

        if (audioTrenFinal != null && !audioTrenFinal.isPlaying)
            audioTrenFinal.Play();

        yield return new WaitForSeconds(tiempoViendoPayasoSentadoConGlobo);

        if (payasoSentadoConGlobo != null)
            payasoSentadoConGlobo.SetActive(false);

        if (payasoDePieFinal != null)
            payasoDePieFinal.SetActive(true);

        CambiarACamaraFinal();

        yield return new WaitForSeconds(tiempoViendoPayasoDePie);

        if (puertaFinal != null)
            puertaFinal.CerrarPuertas();

        yield return StartCoroutine(TemblarCamaraFinal(tiempoTemblorAntesNegro));

        if (canvasFinal != null)
            canvasFinal.SetActive(true);

        Debug.Log("Nivel 15: final en pantalla negra.");
    }

    private void BloquearJugador(bool bloquear)
    {
        if (scriptsJugador != null)
        {
            foreach (MonoBehaviour script in scriptsJugador)
            {
                if (script != null)
                    script.enabled = !bloquear;
            }
        }

        if (scriptsCamaraJugador != null)
        {
            foreach (MonoBehaviour script in scriptsCamaraJugador)
            {
                if (script != null)
                    script.enabled = !bloquear;
            }
        }
    }

    private void CambiarACamaraFinal()
    {
        if (camaraJugador != null)
            camaraJugador.SetActive(false);

        if (camaraFinal != null)
            camaraFinal.SetActive(true);
    }

    private IEnumerator TemblarCamaraFinal(float duracion)
    {
        if (camaraFinal == null)
            yield break;

        float tiempo = 0f;
        Transform camTransform = camaraFinal.transform;

        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;

            float offsetX = Mathf.Sin(Time.time * velocidadTemblor) * intensidadTemblor;
            float offsetY = Mathf.Cos(Time.time * velocidadTemblor * 1.3f) * intensidadTemblor;

            camTransform.localPosition = posicionInicialCamaraFinal + new Vector3(offsetX, offsetY, 0f);

            yield return null;
        }

        camTransform.localPosition = posicionInicialCamaraFinal;
    }
}