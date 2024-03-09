using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance; // Singleton

    [SerializeField] private Animator m_gameOverAnim;
    [SerializeField] private ColorBallSpawner m_colorBallSpawner;
    [SerializeField] private EnemySpawner m_enemySpawner;

    private bool m_gameIsOver = false;

    private void Awake()
    {
        Instance = this;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        m_gameIsOver = true;
        m_gameOverAnim.SetTrigger("GameOver");
        m_colorBallSpawner.StopAllCoroutines();
        m_enemySpawner.StopAllCoroutines();
    }

    public bool IsGameOver()
    {
        return m_gameIsOver;
    }
}
