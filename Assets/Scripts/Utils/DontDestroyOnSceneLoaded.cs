using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnSceneLoaded : MonoBehaviour
{
    [SerializeField] private string m_sceneName;

    private bool m_firstScene = true;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        DontDestroyOnLoad(gameObject);
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (m_firstScene)
        {
            m_firstScene = false;
            return;
        }

        if (scene.name != m_sceneName)
        {
            Destroy(gameObject);
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
