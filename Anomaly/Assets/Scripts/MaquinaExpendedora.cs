using UnityEngine;
using System.Collections;

public class MaquinaExpendedora : Interactable
{
    [Header("Luz de la máquina")]
    public Light luzMaquina;

    [Header("Sonidos")]
    public AudioSource audioSource;
    public AudioClip sonidoMonedas;
    public AudioClip sonidoMaquina;
    public AudioClip sonidoBeber;

    [Header("Ajustes")]
    public float duracionLuz = 3f;

    private bool puedeUsar = false;
    private bool usada = false;
    private bool sonidoMonedasReproducido = false;

    public override void Interact()
    {
        Debug.Log("He interactuado con la máquina correcta");

        if (usada) return;

        if (GameManager.instance.tieneMonedas)
        {
            puedeUsar = true;
        }
        else
        {
            Debug.Log("No tienes monedas");
        }
    }

    public override void HoldInteract()
    {
        if (!puedeUsar || usada) return;

        Debug.Log("Manteniendo E en la máquina");

        // Reproducir monedas solo una vez al empezar a mantener E
        if (!sonidoMonedasReproducido && audioSource != null && sonidoMonedas != null)
        {
            audioSource.PlayOneShot(sonidoMonedas);
            sonidoMonedasReproducido = true;
        }
    }

    public override void HoldCompleted()
    {
        Debug.Log("HoldCompleted de la máquina");

        if (!puedeUsar || usada) return;

        usada = true;

        Debug.Log("Máquina usada correctamente");

        // Gastar las monedas al usar la máquina
        GameManager.instance.tieneMonedas = false;

        StartCoroutine(ActivarMaquina());
    }

    IEnumerator ActivarMaquina()
    {
        // Sonido máquina expendedora al instante
        if (audioSource != null && sonidoMaquina != null)
        {
            audioSource.PlayOneShot(sonidoMaquina);
        }

        // PARPADEO INICIAL
        for (int i = 0; i < 6; i++)
        {
            if (luzMaquina != null)
            {
                luzMaquina.enabled = true;
                luzMaquina.intensity = Random.Range(3f, 10f);
            }

            yield return new WaitForSeconds(Random.Range(0.05f, 0.15f));

            if (luzMaquina != null)
            {
                luzMaquina.enabled = false;
            }

            yield return new WaitForSeconds(Random.Range(0.05f, 0.15f));
        }

        // SE QUEDA ENCENDIDA
        if (luzMaquina != null)
        {
            luzMaquina.enabled = true;
            luzMaquina.intensity = 8f;
        }

        // Espera un poco antes de beber
        yield return new WaitForSeconds(0.8f);

        // Sonido de beber
        if (audioSource != null && sonidoBeber != null)
        {
            audioSource.PlayOneShot(sonidoBeber);
        }

        // Mantener la luz encendida un poco más
        yield return new WaitForSeconds(duracionLuz);

        // APAGADO FINAL
        if (luzMaquina != null)
        {
            luzMaquina.enabled = false;
        }
    }
}