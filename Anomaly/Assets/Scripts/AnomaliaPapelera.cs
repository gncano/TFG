using UnityEngine;

public class AnomaliaPapelera : Interactable
{
    public Transform papelera;
    public Vector3 posicionCorrecta;
    public Vector3 rotacionCorrecta;

    public bool resuelta = false;

    public override void Interact()
    {
        if (papelera != null && !resuelta)
        {
            papelera.localPosition = posicionCorrecta;
            papelera.localEulerAngles = rotacionCorrecta;
            resuelta = true;

            Debug.Log("Papelera colocada correctamente");
        }
    }
}