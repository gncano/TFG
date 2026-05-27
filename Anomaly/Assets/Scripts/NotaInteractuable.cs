using UnityEngine;

public class NotaInteractuable : Interactable
{
    [Header("Nota que se mostrará en pantalla")]
    public Sprite imagenGrandeNota;

    public override void Interact()
    {
        if (NotaPantallaManager.instancia == null)
        {
            Debug.LogWarning("No hay NotaPantallaManager en la escena.");
            return;
        }

        NotaPantallaManager.instancia.AbrirNota(imagenGrandeNota);
    }
}