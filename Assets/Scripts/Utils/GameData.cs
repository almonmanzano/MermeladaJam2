using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance; // Singleton

    public static float MusicVolume = 0.5f;
    public static float SFXVolume = 0.5f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
}
