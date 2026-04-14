using UnityEngine;

public class RadiocasetteInteractable : Interactable
{
    public AudioSource audioSource;
    private bool apagado = false;

    public override void Interact()
    {
        if (apagado) return;

        apagado = true;

        if (audioSource != null)
        {
            audioSource.Stop();
        }

        Debug.Log("Radiocasette apagado");
    }
}