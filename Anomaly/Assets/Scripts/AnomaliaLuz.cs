using UnityEngine;

public class AnomaliaLuz : Interactable
{
    public Light luz;
    public override void Look()
    {
        if (luz != null)
        {
            luz.intensity = 3f;
            luz.color = new Color(1f, 1f, 1f);
            Debug.Log("Luz cambiada");
        }
    }
}
