using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Area : MonoBehaviour
{
    [SerializeField] private string m_colorTag;
    [SerializeField] private TextMeshProUGUI m_nBallsText;

    private int m_nBalls = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(m_colorTag))
        {
            m_nBalls++;
            UpdateNBallsText();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(m_colorTag))
        {
            m_nBalls--;
            UpdateNBallsText();
        }
    }

    private void UpdateNBallsText()
    {
        m_nBallsText.text = m_nBalls.ToString();
    }
}
