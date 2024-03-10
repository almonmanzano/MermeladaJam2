using UnityEngine;

public class Intro : MonoBehaviour
{
    [SerializeField] private GameObject[] m_sequences;
    [SerializeField] private SceneManagement m_sceneManagement;

    private int m_id = 0;

    private void Start()
    {
        NextSequence();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (m_id < m_sequences.Length)
            {
                NextSequence();
            }
            else
            {
                m_sceneManagement.LoadScene("GameScene");
            }
        }
    }

    private void NextSequence()
    {
        m_sequences[m_id].GetComponent<Animator>().SetTrigger("show");
        m_id++;
    }
}
