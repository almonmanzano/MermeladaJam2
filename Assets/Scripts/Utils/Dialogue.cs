using System.Collections;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private GameObject m_textGameObject;
    [SerializeField] private Line[] m_lines;
    [SerializeField] private float m_textSpeed;

    [System.Serializable]
    public struct Line
    {
        public string text;
        public bool animate;
        public Vector2 animatedRange;
    }

    private int m_index;
    private TextMeshProUGUI m_textComponent;
    private WobblyText m_wobblyText;

    private void Awake()
    {
        m_textComponent = m_textGameObject.GetComponent<TextMeshProUGUI>();
        m_wobblyText = m_textGameObject.GetComponent<WobblyText>();
    }

    private void Start()
    {
        m_textComponent.text = string.Empty;
        StartDialogue();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (m_textComponent.text == m_lines[m_index].text)
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                m_textComponent.text = m_lines[m_index].text;
            }
        }
    }

    private void StartDialogue()
    {
        m_index = 0;
        StartCoroutine(TypeLine());
    }

    private IEnumerator TypeLine()
    {
        m_wobblyText.SetAnimatedRange(m_lines[m_index].animate, m_lines[m_index].animatedRange);
        foreach (char c in m_lines[m_index].text.ToCharArray())
        {
            m_textComponent.text += c;
            yield return new WaitForSeconds(1f / m_textSpeed);
        }
    }

    private void NextLine()
    {
        if (m_index < m_lines.Length - 1)
        {
            m_index++;
            m_textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
