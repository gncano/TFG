using UnityEngine;

public class MandoTVInteractuable : Interactable
{
    [Header("Manager del nivel")]
    public NivelTelevisionManager manager;

    [Header("Objeto visual del mando")]
    public GameObject objetoMando;

    private bool recogido = false;

    private void Start()
    {
        if (objetoMando == null)
            objetoMando = gameObject;
    }

    public override void Interact()
    {
        if (recogido)
            return;

        if (manager == null)
        {
            Debug.LogWarning("MandoTVInteractuable: no se ha asignado el manager.");
            return;
        }

        if (!manager.mandoDisponible)
        {
            Debug.Log("Todavía no puedes recoger el mando.");
            return;
        }

        recogido = true;

        manager.RecogerMando();

        if (objetoMando != null)
            objetoMando.SetActive(false);

        Debug.Log("Mando recogido.");
    }
}