using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class NotaPantallaManager : MonoBehaviour
{
    public static NotaPantallaManager instancia;

    [Header("UI")]
    public GameObject canvasNotas;
    public Image imagenNotaGrande;

    [Header("Objetos del jugador a bloquear")]
    public GameObject jugador;
    public GameObject camaraJugador;

    private MonoBehaviour[] scriptsJugador;
    private MonoBehaviour[] scriptsCamara;

    private bool notaAbierta = false;
    private float tiempoApertura = 0f;
    private float tiempoMinimoAntesDeCerrar = 0.15f;

    private void Awake()
    {
        instancia = this;
    }

    private void Start()
    {
        if (canvasNotas != null)
            canvasNotas.SetActive(false);

        if (jugador != null)
            scriptsJugador = jugador.GetComponents<MonoBehaviour>();

        if (camaraJugador != null)
            scriptsCamara = camaraJugador.GetComponents<MonoBehaviour>();
    }

    private void Update()
    {
        if (!notaAbierta)
            return;

        if (Time.time - tiempoApertura < tiempoMinimoAntesDeCerrar)
            return;

        bool clickRaton = Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame;
        bool cualquierTecla = Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame;

        if (clickRaton || cualquierTecla)
        {
            CerrarNota();
        }
    }

    public void AbrirNota(Sprite spriteNota)
    {
        if (spriteNota == null)
        {
            Debug.LogWarning("No se ha asignado sprite de nota.");
            return;
        }

        if (imagenNotaGrande != null)
            imagenNotaGrande.sprite = spriteNota;

        if (canvasNotas != null)
            canvasNotas.SetActive(true);

        notaAbierta = true;
        tiempoApertura = Time.time;

        BloquearJugador(true);

        Debug.Log("Nota abierta.");
    }

    public void CerrarNota()
    {
        if (canvasNotas != null)
            canvasNotas.SetActive(false);

        notaAbierta = false;

        BloquearJugador(false);

        Debug.Log("Nota cerrada.");
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
}