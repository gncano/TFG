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

    [Header("Hold")]
    public float tiempoMinimoParaMonedas = 0.2f;

    private bool usada = false;
    private bool secuenciaIniciada = false;
    private bool sonidoMonedasReproducido = false;
    private float tiempoManteniendo = 0f;

    public override void Interact()
    {
        // No hacemos nada al pulsar una vez
    }

    public override void HoldInteract()
    {
        if (usada || secuenciaIniciada)
            return;

        if (GameManager.instance == null)
        {
            Debug.LogWarning("No hay GameManager en la escena.");
            return;
        }

        if (!GameManager.instance.tieneMonedas)
        {
            return;
        }

        tiempoManteniendo += Time.deltaTime;

        if (!sonidoMonedasReproducido && tiempoManteniendo >= tiempoMinimoParaMonedas)
        {
            sonidoMonedasReproducido = true;

            if (audioSource != null && sonidoMonedas != null)
            {
                audioSource.PlayOneShot(sonidoMonedas);
            }

            UsarMaquina();
        }
    }

    public override void HoldCompleted()
    {
        if (usada || secuenciaIniciada)
            return;

        if (GameManager.instance == null)
        {
            Debug.LogWarning("No hay GameManager en la escena.");
            return;
        }

        if (!GameManager.instance.tieneMonedas)
        {
            Debug.Log("No tienes monedas");
            return;
        }

        UsarMaquina();
    }

    public override void ResetHold()
    {
        if (usada || secuenciaIniciada)
            return;

        tiempoManteniendo = 0f;
        sonidoMonedasReproducido = false;
    }

    private void UsarMaquina()
    {
        if (usada || secuenciaIniciada)
            return;

        if (GameManager.instance == null)
        {
            Debug.LogWarning("No hay GameManager en la escena.");
            return;
        }

        if (!GameManager.instance.tieneMonedas)
        {
            Debug.Log("No tienes monedas");
            return;
        }

        usada = true;
        secuenciaIniciada = true;

        Debug.Log("Máquina usada correctamente");

        GameManager.instance.tieneMonedas = false;

        StartCoroutine(ActivarMaquina());
    }

    IEnumerator ActivarMaquina()
    {
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

        yield return new WaitForSeconds(0.8f);

        if (audioSource != null && sonidoBeber != null)
        {
            audioSource.PlayOneShot(sonidoBeber);
        }

        yield return new WaitForSeconds(duracionLuz);

        // APAGADO FINAL
        if (luzMaquina != null)
        {
            luzMaquina.enabled = false;
        }

        MarcarNivelComoResuelto();
    }
}