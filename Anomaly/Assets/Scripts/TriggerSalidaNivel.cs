using UnityEngine;
using UnityEngine.InputSystem;

public class TriggerSalidaNivel : MonoBehaviour
{
    public ConfirmarSalidaNivel confirmarSalida;

    private bool jugadorDentro = false;

    [Header("Reabrir panel")]
    public float tiempoMinimoEntreAperturas = 0.3f;
    private float ultimaApertura = -10f;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        jugadorDentro = true;

        AbrirPanelSiSePuede();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (!jugadorDentro)
            return;

        if (confirmarSalida == null)
            return;

        if (confirmarSalida.PanelAbierto)
            return;

        if (Time.time - ultimaApertura < tiempoMinimoEntreAperturas)
            return;

        bool intentaAvanzar = Keyboard.current != null &&
                              (Keyboard.current.wKey.wasPressedThisFrame ||
                               Keyboard.current.upArrowKey.wasPressedThisFrame);

        if (intentaAvanzar)
        {
            AbrirPanelSiSePuede();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        jugadorDentro = false;
    }

    private void AbrirPanelSiSePuede()
    {
        if (confirmarSalida != null)
        {
            confirmarSalida.AbrirPanel();
            ultimaApertura = Time.time;
        }
        else
        {
            Debug.LogWarning("TriggerSalidaNivel: no se ha asignado ConfirmarSalidaNivel.");
        }
    }
}