using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance; // Singleton

    [SerializeField] private Animator m_gameOverAnim;
    [SerializeField] private ColorBallSpawner m_colorBallSpawner;
    [SerializeField] private EnemySpawner m_enemySpawner;
    [SerializeField] private TextMeshProUGUI m_nSacrificesText;

    [SerializeField] private Leaderboard m_leaderboard;
    [SerializeField] private GameObject[] m_leaderboardPanels;
    [SerializeField] private GameObject m_addYourNamePanel;
    [SerializeField] private TMP_InputField m_playerNameInputText;

    private int m_nSacrifices = 0;

    private bool m_gameIsOver = false;

    private void Awake()
    {
        Instance = this;
    }

    public void AddSacrifice()
    {
        m_nSacrifices++;
        m_nSacrificesText.text = m_nSacrifices.ToString();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1f;
    }

    public async void GameOver()
    {
        m_gameIsOver = true;
        m_colorBallSpawner.StopAllCoroutines();
        m_enemySpawner.StopAllCoroutines();
        m_gameOverAnim.SetTrigger("GameOver");

        int playerMaxScore = await m_leaderboard.GetPlayerScore();
        if (playerMaxScore < m_nSacrifices)
        {
            m_addYourNamePanel.SetActive(true);
        }
        else
        {
            UpdateLeaderboard();
        }
    }

    public async void SaveScore()
    {
        CleanLeaderboard();
        await m_leaderboard.AddScore(m_nSacrifices, m_playerNameInputText.text);
        UpdateLeaderboard();
    }

    private void CleanLeaderboard()
    {
        foreach (GameObject panel in m_leaderboardPanels)
        {
            panel.SetActive(false);
        }
    }

    private async void UpdateLeaderboard()
    {
        Dictionary<string, int> scores = await m_leaderboard.GetScores();
        print(scores);
        int i = 0;
        foreach (KeyValuePair<string, int> pair in scores)
        {
            string name = pair.Key;
            int score = pair.Value;
            GameObject panel = m_leaderboardPanels[i];
            panel.SetActive(true);
            panel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = name;
            panel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = score.ToString();
            i++;
        }
        for (int j = i; j < 5; j++)
        {
            m_leaderboardPanels[j].SetActive(false);
        }
    }

    public bool IsGameOver()
    {
        return m_gameIsOver;
    }
}
