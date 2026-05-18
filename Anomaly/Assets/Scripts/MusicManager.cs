using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instancia;

    [Header("Audio")]
    public AudioSource audioSource;

    [Header("Configuración")]
    [Range(0f, 1f)]
    public float volumen = 0.10f;

    private void Awake()
    {
        if (instancia != null && instancia != this)
        {
            Destroy(gameObject);
            return;
        }

        instancia = this;
        DontDestroyOnLoad(gameObject);

        ConfigurarMusica();
    }

    private void Start()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    private void ConfigurarMusica()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogWarning("MusicManager: no hay AudioSource asignado.");
            return;
        }

        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f;
        audioSource.volume = volumen;
    }

    public void CambiarVolumen(float nuevoVolumen)
    {
        volumen = Mathf.Clamp01(nuevoVolumen);

        if (audioSource != null)
            audioSource.volume = volumen;
    }

    public void PararMusica()
    {
        if (audioSource != null)
            audioSource.Stop();
    }

    public void ReanudarMusica()
    {
        if (audioSource != null && !audioSource.isPlaying)
            audioSource.Play();
    }
}