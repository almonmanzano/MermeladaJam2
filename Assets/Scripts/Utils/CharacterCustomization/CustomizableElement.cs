using System.Collections.Generic;
using UnityEngine;

public class CustomizableElement : MonoBehaviour
{
    [SerializeField] private CustomizationType m_type;
    [SerializeField] private SpriteRenderer m_spriteRenderer;
    [SerializeField] private List<PositionedSprite> m_spriteOptions;
    [SerializeField] private List<Color> m_colorOptions;

    public int SpriteIndex;

    private int m_colorIndex;

    public Color CurrentColor => m_colorOptions.Count == 0 ? Color.white : m_colorOptions[m_colorIndex];

    private void Start()
    {
        UpdateSprite();
        UpdateColor();
    }

    [ContextMenu("Next Sprite")]
    public Sprite NextSprite()
    {
        SpriteIndex = (SpriteIndex + 1) % m_spriteOptions.Count;
        UpdateSprite();
        return m_spriteOptions[SpriteIndex].sprite;
    }

    [ContextMenu("Previous Sprite")]
    public Sprite PreviousSprite()
    {
        SpriteIndex = (SpriteIndex - 1 + m_spriteOptions.Count) % m_spriteOptions.Count;
        UpdateSprite();
        return m_spriteOptions[SpriteIndex].sprite;
    }

    [ContextMenu("Next Color")]
    public Color NextColor()
    {
        m_colorIndex = (m_colorIndex + 1) % m_colorOptions.Count;
        UpdateColor();
        return m_colorOptions[m_colorIndex];
    }

    [ContextMenu("Previous Color")]
    public Color PreviousColor()
    {
        m_colorIndex = (m_colorIndex - 1 + m_colorOptions.Count) % m_colorOptions.Count;
        UpdateColor();
        return m_colorOptions[m_colorIndex];
    }

    [ContextMenu("Update Position Modifier")]
    public void UpdateSpritePositionModifier()
    {
        m_spriteOptions[SpriteIndex].position = transform.localPosition;
    }

    [ContextMenu("Randomize")]
    public void Randomize()
    {
        SpriteIndex = Random.Range(0, m_spriteOptions.Count);
        UpdateSprite();
        m_colorIndex = Random.Range(0, m_colorOptions.Count);
        UpdateColor();
    }

    public CustomizationData GetCustomizationData()
    {
        return new CustomizationData(m_type, m_spriteOptions[SpriteIndex], m_spriteRenderer.color);
    }

    private void UpdateSprite()
    {
        SpriteIndex = Mathf.Clamp(SpriteIndex, 0, m_spriteOptions.Count - 1);
        m_spriteRenderer.sprite = m_spriteOptions[SpriteIndex].sprite;
        transform.localPosition = m_spriteOptions[SpriteIndex].position;
    }

    private void UpdateColor()
    {
        m_colorIndex = Mathf.Clamp(m_colorIndex, 0, m_colorOptions.Count - 1);
        m_spriteRenderer.color = m_colorOptions[m_colorIndex];
    }
}
