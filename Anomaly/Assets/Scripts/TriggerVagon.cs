using UnityEngine;

public class TriggerVagon : MonoBehaviour
{
    public AudioSource audioJigsaw;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (audioJigsaw != null)
            {
                audioJigsaw.Play();
            }

            Debug.Log("Entró en el vagón");
        }
    }
}