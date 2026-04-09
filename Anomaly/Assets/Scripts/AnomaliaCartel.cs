using UnityEngine;

public class AnomaliaCartel : Interactable
{
    public Transform cartel;        
    public Vector3 rotacionCorrecta;  

    public override void Interact()
    {
        if (cartel != null)
        {
            cartel.eulerAngles = rotacionCorrecta; 
            Debug.Log("Cartel colocado correctamente");
        }
    }
}