using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public static PauseManager instancia;

    [Header("UI principal")]
    public GameObject canvasPausa;
    public GameObject panelMenuPrincipal;
    public GameObject panelControles;
    public GameObject panelOpciones;
    public GameObject panelCreditos;

    [Header("Sliders")]
    public Slider sliderMusica;
    public Slider sliderEfectos;

    [Header("Volúmenes")]
    [Range(0.15f, 1f)]
    public float volumenMusica = 0.10f;

    [Range(0.15f, 1f)]
    public float volumenEfectos = 0.70f;

    private bool juegoPausado = false;

    private GameObject jugadorActual;
    private GameObject camaraActual;

    private MonoBehaviour[] scriptsJugador;
    private MonoBehaviour[] scriptsCamara;

    private Dictionary<AudioSource, float> volumenesBase = new Dictionary<AudioSource, float>();

    private void Awake()
    {
        if (instancia != null && instancia != this)
        {
            Destroy(gameObject);
            return;
        }

        instancia = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        if (canvasPausa != null)
            canvasPausa.SetActive(false);

        MostrarMenuPrincipal();

        if (sliderMusica != null)
        {
            sliderMusica.minValue = 0.15f;
            sliderMusica.maxValue = 1f;
            sliderMusica.value = volumenMusica;
            sliderMusica.onValueChanged.AddListener(CambiarVolumenMusica);
        }

        if (sliderEfectos != null)
        {
            sliderEfectos.minValue = 0.15f;
            sliderEfectos.maxValue = 1f;
            sliderEfectos.value = volumenEfectos;
            sliderEfectos.onValueChanged.AddListener(CambiarVolumenEfectos);
        }

        BuscarJugadorYCamara();
        RegistrarAudiosDeEscena();
        AplicarVolumenes();
    }

    private void Update()
    {
        if (Keyboard.current == null)
            return;

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (juegoPausado)
                ContinuarPartida();
            else
                PausarPartida();
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (juegoPausado)
            ContinuarPartida();

        BuscarJugadorYCamara();
        RegistrarAudiosDeEscena();
        AplicarVolumenes();
    }

    private void BuscarJugadorYCamara()
    {
        jugadorActual = GameObject.FindGameObjectWithTag("Player");

        Camera cam = Camera.main;
        if (cam != null)
            camaraActual = cam.gameObject;
        else
            camaraActual = null;

        if (jugadorActual != null)
            scriptsJugador = jugadorActual.GetComponents<MonoBehaviour>();
        else
            scriptsJugador = null;

        if (camaraActual != null)
            scriptsCamara = camaraActual.GetComponents<MonoBehaviour>();
        else
            scriptsCamara = null;
    }

    public void PausarPartida()
    {
        if (juegoPausado)
            return;

        juegoPausado = true;

        if (canvasPausa != null)
            canvasPausa.SetActive(true);

        MostrarMenuPrincipal();

        BloquearJugador(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Debug.Log("Juego pausado.");
    }

    public void ContinuarPartida()
    {
        juegoPausado = false;

        if (canvasPausa != null)
            canvasPausa.SetActive(false);

        BloquearJugador(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Debug.Log("Juego reanudado.");
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

        if (scriptsCamara != null)
        {
            foreach (MonoBehaviour script in scriptsCamara)
            {
                if (script != null)
                    script.enabled = !bloquear;
            }
        }
    }

    public void MostrarMenuPrincipal()
    {
        if (panelMenuPrincipal != null)
            panelMenuPrincipal.SetActive(true);

        if (panelControles != null)
            panelControles.SetActive(false);

        if (panelOpciones != null)
            panelOpciones.SetActive(false);

        if (panelCreditos != null)
            panelCreditos.SetActive(false);
    }

    public void MostrarControles()
    {
        if (panelMenuPrincipal != null)
            panelMenuPrincipal.SetActive(false);

        if (panelControles != null)
            panelControles.SetActive(true);

        if (panelOpciones != null)
            panelOpciones.SetActive(false);

        if (panelCreditos != null)
            panelCreditos.SetActive(false);
    }

    public void MostrarOpciones()
    {
        if (panelMenuPrincipal != null)
            panelMenuPrincipal.SetActive(false);

        if (panelControles != null)
            panelControles.SetActive(false);

        if (panelOpciones != null)
            panelOpciones.SetActive(true);

        if (panelCreditos != null)
            panelCreditos.SetActive(false);
    }

    public void MostrarCreditos()
    {
        if (panelMenuPrincipal != null)
            panelMenuPrincipal.SetActive(false);

        if (panelControles != null)
            panelControles.SetActive(false);

        if (panelOpciones != null)
            panelOpciones.SetActive(false);

        if (panelCreditos != null)
            panelCreditos.SetActive(true);
    }

    public void SalirDelJuego()
    {
        Debug.Log("Saliendo del juego.");

        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void CambiarVolumenMusica(float valor)
    {
        volumenMusica = Mathf.Clamp(valor, 0.15f, 1f);

        if (MusicManager.instancia != null)
        {
            MusicManager.instancia.CambiarVolumen(volumenMusica);
        }
    }

    public void CambiarVolumenEfectos(float valor)
    {
        volumenEfectos = Mathf.Clamp(valor, 0.15f, 1f);
        AplicarVolumenEfectos();
    }

    private void RegistrarAudiosDeEscena()
    {
        volumenesBase.Clear();

        AudioSource[] audios = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
        
        foreach (AudioSource audio in audios)
        {
            if (audio == null)
                continue;

            if (MusicManager.instancia != null && audio == MusicManager.instancia.audioSource)
                continue;

            if (!volumenesBase.ContainsKey(audio))
                volumenesBase.Add(audio, audio.volume);
        }
    }

    private void AplicarVolumenes()
    {
        CambiarVolumenMusica(volumenMusica);
        AplicarVolumenEfectos();
    }

    private void AplicarVolumenEfectos()
    {
        foreach (KeyValuePair<AudioSource, float> par in volumenesBase)
        {
            if (par.Key != null)
            {
                par.Key.volume = par.Value * volumenEfectos;
            }
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}