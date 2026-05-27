using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    [Header("Pantallas")]
    public GameObject pantallaTitulo;
    public GameObject pantallaLore;

    [Header("Textos que parpadean")]
    public TMP_Text textoPulsaTitulo;
    public TMP_Text textoPulsaLore;

    [Header("Lore")]
    public TMP_Text textoLore;
    public GameObject textoControles;

    [TextArea(4, 8)]
    public string textoLoreCompleto =
        "Durante su turno de noche, un guardia de seguridad queda atrapado en un extraño bucle dentro de una estación de metro.\n\n" +
        "Para avanzar, deberá observar cuidadosamente el entorno, detectar anomalías y resolverlas antes de continuar.";

    [Header("Ajustes de escritura")]
    public float velocidadEscritura = 0.035f;

    [Header("Parpadeo")]
    public float velocidadParpadeo = 0.6f;

    [Header("Audio")]
    public AudioSource audioTren;

    [Header("Escena siguiente")]
    public string escenaSiguiente = "0-Inicio";

    private int pantallaActual = 0;
    private bool escribiendoLore = false;
    private bool loreTerminado = false;

    private Coroutine rutinaEscritura;
    private Coroutine rutinaParpadeoTitulo;
    private Coroutine rutinaParpadeoLore;

    private void Start()
    {
        if (pantallaTitulo != null)
            pantallaTitulo.SetActive(true);

        if (pantallaLore != null)
            pantallaLore.SetActive(false);

        if (textoControles != null)
            textoControles.SetActive(false);

        if (textoLore != null)
            textoLore.text = "";

        if (audioTren != null)
            audioTren.Play();

        rutinaParpadeoTitulo = StartCoroutine(ParpadearTexto(textoPulsaTitulo));
    }

    private void Update()
    {
        if (Keyboard.current == null)
            return;

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            GestionarPulsacionEspacio();
        }
    }

    private void GestionarPulsacionEspacio()
    {
        if (pantallaActual == 0)
        {
            MostrarPantallaLore();
            return;
        }

        if (pantallaActual == 1)
        {
            if (escribiendoLore)
            {
                CompletarLoreInstantaneamente();
                return;
            }

            if (loreTerminado)
            {
                CargarNivelInicial();
            }
        }
    }

    private void MostrarPantallaLore()
    {
        pantallaActual = 1;

        if (rutinaParpadeoTitulo != null)
            StopCoroutine(rutinaParpadeoTitulo);

        if (pantallaTitulo != null)
            pantallaTitulo.SetActive(false);

        if (pantallaLore != null)
            pantallaLore.SetActive(true);

        if (textoPulsaLore != null)
            textoPulsaLore.gameObject.SetActive(false);

        if (textoControles != null)
            textoControles.SetActive(false);

        rutinaEscritura = StartCoroutine(EscribirLore());
    }

    private IEnumerator EscribirLore()
    {
        escribiendoLore = true;
        loreTerminado = false;

        if (textoLore != null)
        {
            textoLore.text = textoLoreCompleto;
            textoLore.maxVisibleCharacters = 0;

            int totalCaracteres = textoLore.textInfo.characterCount;

            // Forzar actualización para que TextMeshPro calcule bien los caracteres
            textoLore.ForceMeshUpdate();
            totalCaracteres = textoLore.textInfo.characterCount;

            for (int i = 0; i <= totalCaracteres; i++)
            {
                textoLore.maxVisibleCharacters = i;
                yield return new WaitForSeconds(velocidadEscritura);
            }
        }

        TerminarLore();
    }

    private void CompletarLoreInstantaneamente()
    {
        if (rutinaEscritura != null)
            StopCoroutine(rutinaEscritura);

        if (textoLore != null)
        {
            textoLore.text = textoLoreCompleto;
            textoLore.ForceMeshUpdate();
            textoLore.maxVisibleCharacters = textoLore.textInfo.characterCount;
        }

        TerminarLore();
    }

    private void TerminarLore()
    {
        escribiendoLore = false;
        loreTerminado = true;

        if (textoControles != null)
            textoControles.SetActive(true);

        if (textoPulsaLore != null)
            textoPulsaLore.gameObject.SetActive(true);

        if (rutinaParpadeoLore != null)
            StopCoroutine(rutinaParpadeoLore);

        rutinaParpadeoLore = StartCoroutine(ParpadearTexto(textoPulsaLore));
    }

    private IEnumerator ParpadearTexto(TMP_Text texto)
    {
        if (texto == null)
            yield break;

        while (true)
        {
            texto.enabled = true;
            yield return new WaitForSeconds(velocidadParpadeo);

            texto.enabled = false;
            yield return new WaitForSeconds(velocidadParpadeo);
        }
    }

    private void CargarNivelInicial()
    {
        SceneManager.LoadScene(escenaSiguiente);
    }
}