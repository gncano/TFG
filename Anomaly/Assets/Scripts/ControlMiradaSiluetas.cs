using UnityEngine;

public class ControlMiradaSiluetas : MonoBehaviour
{
    public Transform camaraJugador;
    public Transform puntoMiradaTren;
    public SiluetaFantasma[] siluetas;

    [Header("Ajuste de mirada")]
    [Range(-1f, 1f)]
    public float umbralMirada = 0.95f;

    private void Update()
    {
        if (camaraJugador == null || puntoMiradaTren == null || siluetas == null || siluetas.Length == 0)
            return;

        Vector3 direccionAlTren = (puntoMiradaTren.position - camaraJugador.position).normalized;
        float producto = Vector3.Dot(camaraJugador.forward, direccionAlTren);

        bool mirandoAlTren = producto >= umbralMirada;

        for (int i = 0; i < siluetas.Length; i++)
        {
            if (siluetas[i] != null)
            {
                siluetas[i].SetJugadorMirando(mirandoAlTren);
            }
        }
    }
}