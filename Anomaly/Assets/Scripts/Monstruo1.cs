using UnityEngine;

public class Monstruo1 : MonoBehaviour
{
    public GameObject monstruo;

    private bool haSidoActivado=false;

    void Awake()
    {
        monstruo.SetActive(false);
    }

    void OnTriggerEnter(Collider player)
    {
        Debug.Log("Dentro del rango");
        if (!haSidoActivado)
        {
            monstruo.SetActive(true);
            haSidoActivado = true;
        }
    }
    void OnTriggerExit(Collider player)
    {
        monstruo.SetActive(false);
    }
}
