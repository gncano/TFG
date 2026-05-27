using UnityEngine;

public class AnomaliaCartel : Interactable
{
    public Transform cartel;
    public Vector3 rotacionCorrecta;

    private bool resuelta = false;

    public override void Interact()
    {
        if (resuelta)
            return;

        if (cartel != null)
        {
            cartel.eulerAngles = rotacionCorrecta;
            Debug.Log("Cartel colocado correctamente");

            resuelta = true;
            MarcarNivelComoResuelto();
        }
    }
}