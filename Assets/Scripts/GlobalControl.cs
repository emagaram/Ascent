using UnityEngine;
using System.Collections;

public class GlobalControl : MonoBehaviour
{
    public static GlobalControl Instance;
    public Vector2 playerLocation;
    public GameObject checkPoint;
    public float timeAtRestart;
    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}