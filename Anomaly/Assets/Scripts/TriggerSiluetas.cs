using UnityEngine;

public class TriggerSiluetas : MonoBehaviour
{
    public SiluetaFantasma[] siluetas;
    private bool activado = false;

    private void OnTriggerEnter(Collider other)
    {
        if (activado) return;

        if (other.CompareTag("Player"))
        {
            activado = true;

            for (int i = 0; i < siluetas.Length; i++)
            {
                if (siluetas[i] != null)
                {
                    siluetas[i].ActivarAnomalia();
                }
            }

            Debug.Log("Anomalía de siluetas activada");
        }
    }
}