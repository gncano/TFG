using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool tieneSpray = false;
    public bool tieneTrapo = false;
    public bool tieneMonedas = false;

    void Awake()
    {
        instance = this;
    }
}