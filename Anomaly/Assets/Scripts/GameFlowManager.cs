using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlowManager : MonoBehaviour
{
    public static GameFlowManager instancia;

    [Header("Secuencia generada")]
    public List<string> secuenciaNiveles = new List<string>();

    [Header("Estado actual")]
    public int indiceNivelActual = 0;

    [Header("Escena inicial")]
    public string escenaNivel0 = "0-Inicio";

    [Header("Nivel final fijo")]
    public string escenaPayasoFinal = "15-AnomaliaPayaso";

    [Header("Nivel sin anomalía")]
    public string escenaSinAnomalia = "16-SinAnomalia";

    private void Awake()
    {
        if (instancia != null && instancia != this)
        {
            Destroy(gameObject);
            return;
        }

        instancia = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (secuenciaNiveles.Count == 0)
        {
            GenerarNuevaSecuencia();
        }
    }

    public void GenerarNuevaSecuencia()
    {
        secuenciaNiveles.Clear();

        secuenciaNiveles.Add(escenaNivel0);

        List<string> faciles = new List<string>
        {
            "1-AnomaliaCartel",
            "2-AnomaliaPapelera",
            "3-AnomaliaSala",
            "4-AnomaliaAsiento",
            "5-AnomaliaVagonMovido",
            escenaSinAnomalia
        };

        AgregarAleatoriosSinRepetir(faciles, 5);

        List<string> intermedios = new List<string>
        {
            "6-AnomaliaEnano",
            "7-AnomaliaMapa",
            "8-AnomaliaSonido",
            "9-AnomaliaLuz",
            "10-AnomaliaMaquinaExpendedora",
            escenaSinAnomalia
        };

        AgregarAleatoriosSinRepetir(intermedios, 5);

        List<string> dificiles = new List<string>
        {
            "11-AnomaliaTelevision",
            "12-AnomaliaStalker",
            "13-AnomaliaVentanaTren",
            "14-AnomaliaSiluetas",
            escenaSinAnomalia
        };

        AgregarAleatoriosSinRepetir(dificiles, 4);

        secuenciaNiveles.Add(escenaPayasoFinal);

        indiceNivelActual = 0;

        Debug.Log("Nueva secuencia generada:");
        for (int i = 0; i < secuenciaNiveles.Count; i++)
        {
            Debug.Log("Ronda " + i + ": " + secuenciaNiveles[i]);
        }
    }

    private void AgregarAleatoriosSinRepetir(List<string> listaOriginal, int cantidad)
    {
        List<string> copia = new List<string>(listaOriginal);

        for (int i = 0; i < cantidad; i++)
        {
            int indiceAleatorio = Random.Range(0, copia.Count);
            string escenaElegida = copia[indiceAleatorio];

            secuenciaNiveles.Add(escenaElegida);
            copia.RemoveAt(indiceAleatorio);
        }
    }

    public void PasarAlSiguienteNivel()
    {
        indiceNivelActual++;

        if (indiceNivelActual >= secuenciaNiveles.Count)
        {
            Debug.Log("No hay más niveles. Fin del juego.");
            return;
        }

        string siguienteEscena = secuenciaNiveles[indiceNivelActual];

        Debug.Log("Cargando siguiente nivel: " + siguienteEscena);

        SceneManager.LoadScene(siguienteEscena);
    }

    public void VolverAlNivel0PorFallo()
    {
        Debug.Log("Fallo detectado. Volviendo al nivel 0 y generando nueva secuencia.");

        GenerarNuevaSecuencia();

        SceneManager.LoadScene(escenaNivel0);
    }

    public int ObtenerNumeroRondaActual()
    {
        return indiceNivelActual;
    }
}