using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject[] m_popUps;

    private int m_popUpIndex;

    private void Update()
    {
        for (int i = 0; i < m_popUps.Length; i++)
        {
            if (i == m_popUpIndex)
            {
                m_popUps[i].SetActive(true);
            }
            else
            {
                m_popUps[i].SetActive(false);
            }
        }

        if (m_popUpIndex == 0)
        {
            // If player completes the first step of the tutorial
            if (Input.GetAxisRaw("Horizontal") != 0f || Input.GetAxisRaw("Vertical") != 0f)
            {
                m_popUpIndex++;
            }
        }
        else if (m_popUpIndex == 1)
        {
            // If player completes the second step of the tutorial... etc
        }
    }
}
