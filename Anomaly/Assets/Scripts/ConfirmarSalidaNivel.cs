using UnityEngine;

public class ConfirmarSalidaNivel : MonoBehaviour
{
    [Header("UI")]
    public GameObject canvasConfirmacion;

    [Header("Jugador a bloquear mientras decide")]
    public GameObject jugador;
    public GameObject camaraJugador;

    private MonoBehaviour[] scriptsJugador;
    private MonoBehaviour[] scriptsCamara;

    private bool panelAbierto = false;

    public bool PanelAbierto
    {
        get { return panelAbierto; }
    }

    private void Start()
    {
        if (canvasConfirmacion != null)
            canvasConfirmacion.SetActive(false);

        if (jugador != null)
            scriptsJugador = jugador.GetComponents<MonoBehaviour>();

        if (camaraJugador != null)
            scriptsCamara = camaraJugador.GetComponents<MonoBehaviour>();
    }

    public void AbrirPanel()
    {
        if (panelAbierto)
            return;

        panelAbierto = true;

        if (canvasConfirmacion != null)
            canvasConfirmacion.SetActive(true);

        BloquearJugador(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Debug.Log("Panel de confirmación de salida abierto.");
    }

    public void PulsarNo()
    {
        CerrarPanel();
    }

    public void PulsarSi()
    {
        Debug.Log("El jugador ha confirmado pasar al siguiente nivel.");

        if (EstadoNivel.instancia == null)
        {
            Debug.LogWarning("No hay EstadoNivel en la escena. No se puede comprobar la anomalía.");
            CerrarPanel();
            return;
        }

        if (GameFlowManager.instancia == null)
        {
            Debug.LogWarning("No hay GameFlowManager en la escena. No se puede cambiar de nivel.");
            CerrarPanel();
            return;
        }

        if (EstadoNivel.instancia.EstaResuelta())
        {
            Debug.Log("La anomalía está resuelta. Pasando al siguiente nivel.");
            CerrarPanel();
            GameFlowManager.instancia.PasarAlSiguienteNivel();
        }
        else
        {
            Debug.Log("La anomalía NO está resuelta. Volviendo al nivel 0.");
            CerrarPanel();
            GameFlowManager.instancia.VolverAlNivel0PorFallo();
        }
    }

    private void CerrarPanel()
    {
        panelAbierto = false;

        if (canvasConfirmacion != null)
            canvasConfirmacion.SetActive(false);

        BloquearJugador(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Debug.Log("Panel de confirmación de salida cerrado.");
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