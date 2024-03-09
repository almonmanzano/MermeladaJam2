using TMPro;
using UnityEngine;

public class WobblyText : MonoBehaviour
{
    [SerializeField] private Vector2 m_charIndexRange;
    [SerializeField] private Color[] m_colors = { Color.red, Color.yellow, Color.green, Color.cyan, Color.blue, Color.magenta };
    [SerializeField] private float m_timeMultiplier = 7f;
    [SerializeField] private float m_waveWidth = 0.07f;
    [SerializeField] private float m_waveHeight = 5f;

    private TMP_Text m_textComponent;

    private void Awake()
    {
        m_textComponent = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        m_textComponent.ForceMeshUpdate();
        var textInfo = m_textComponent.textInfo;

        int colorIndex = 0;
        for (int i = 0; i < textInfo.characterCount; ++i)
        {
            if (i < m_charIndexRange.x || i > m_charIndexRange.y)
            {
                continue;
            }

            var charInfo = textInfo.characterInfo[i];

            if (!charInfo.isVisible)
            {
                continue;
            }

            var meshInfo = textInfo.meshInfo[charInfo.materialReferenceIndex];

            for (int j = 0; j < 4; ++j)
            {
                var index = charInfo.vertexIndex + j;
                var orig = meshInfo.vertices[index];
                meshInfo.vertices[index] = orig + new Vector3(0f, Mathf.Sin(Time.time * m_timeMultiplier + orig.x * m_waveWidth) * m_waveHeight, 0f);
                meshInfo.colors32[index] = m_colors[colorIndex % m_colors.Length];
                if (j % 2 != 0)
                {
                    colorIndex++;
                }
            }

        }

        for (int i = 0; i < textInfo.meshInfo.Length; ++i)
        {
            var meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            meshInfo.mesh.colors32 = meshInfo.colors32;
            m_textComponent.UpdateGeometry(meshInfo.mesh, i);
        }
    }

    public void SetAnimatedRange(bool animate, Vector2 range)
    {
        if (animate)
        {
            m_charIndexRange = range;
        }
        else
        {
            m_charIndexRange = new Vector2(-1f, -1f);
        }
    }
}
