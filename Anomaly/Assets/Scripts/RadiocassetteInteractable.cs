using UnityEngine;

public class RadiocasetteInteractable : Interactable
{
    [Header("Audio de la anomalía")]
    public AudioSource audioPasos;

    [Header("Audio al apagar")]
    public AudioSource audioApagado;

    private bool apagado = false;

    public override void Interact()
    {
        if (apagado)
            return;

        apagado = true;

        if (audioPasos != null)
        {
            audioPasos.Stop();
        }

        if (audioApagado != null)
        {
            audioApagado.Play();
        }

        Debug.Log("Radiocasette apagado");

        MarcarNivelComoResuelto();
    }
}