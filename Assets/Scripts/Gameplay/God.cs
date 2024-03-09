using UnityEngine;
using UnityEngine.UI;

public class God : MonoBehaviour
{
    [SerializeField] private Slider m_patienceSlider;
    [SerializeField] private float m_patienceDecreasingSpeed = 1f;
    [SerializeField] private float m_totalPatience = 100f;

    private float m_patience;

    private void Start()
    {
        m_patience = m_totalPatience;
    }

    private void Update()
    {
        if (GameController.Instance.IsGameOver()) return;

        m_patience -= m_patienceDecreasingSpeed * Time.deltaTime;
        CheckPatience();
        UpdatePatienceSlider();
    }

    public void Sacrifice()
    {
        m_patience = m_totalPatience;
        UpdatePatienceSlider();
    }

    private void CheckPatience()
    {
        if (m_patience <= 0f)
        {
            GameController.Instance.GameOver();
        }
    }

    private void UpdatePatienceSlider()
    {
        m_patienceSlider.value = m_patience / m_totalPatience;
    }
}
