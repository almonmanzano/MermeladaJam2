using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Area : MonoBehaviour
{
    [SerializeField] private string m_colorTag;
    [SerializeField] private int m_nBallsForSacrifice = 5;
    [SerializeField] private TextMeshProUGUI m_nBallsText;
    [SerializeField] private God m_god;
    [SerializeField] private GameObject m_sacrificeVFX;
    [SerializeField] private Transform m_sacrificePoint;

    private List<GameObject> m_balls = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(m_colorTag))
        {
            m_balls.Add(collision.gameObject);
            CheckIfSacrifice();
            UpdateNBallsText();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(m_colorTag))
        {
            m_balls.Remove(collision.gameObject);
            UpdateNBallsText();
        }
    }

    private void CheckIfSacrifice()
    {
        if (m_balls.Count == m_nBallsForSacrifice)
        {
            GameObject[] toDestroy = m_balls.ToArray();
            m_balls.Clear();
            m_god.Sacrifice();
            foreach (GameObject ball in toDestroy)
            {
                StartCoroutine(ball.GetComponent<ColorBall>().BeSacrified(0.5f));
            }
            Instantiate(m_sacrificeVFX, m_sacrificePoint.position, m_sacrificeVFX.transform.rotation);
        }
    }

    private void UpdateNBallsText()
    {
        m_nBallsText.text = m_balls.Count.ToString();
    }
}
