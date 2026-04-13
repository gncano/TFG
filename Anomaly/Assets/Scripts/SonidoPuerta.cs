using UnityEngine;

public class SonidoPuerta : MonoBehaviour
{
    public AudioSource audioSource;
    private bool yaSonado=false;

    void OnTriggerEnter(Collider player)
    {
        
        Debug.Log("El jugador ha entrado en rango de puerta");

        if (!yaSonado)
        {
            audioSource.Play();
            yaSonado=true;
        }
    }
}
