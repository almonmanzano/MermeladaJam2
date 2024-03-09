using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_CustomizationPicker : MonoBehaviour
{
    [SerializeField] private CustomizableElement m_customizableElement;

    [SerializeField] private Button m_previousSpriteButton;
    [SerializeField] private TMP_Text m_spriteId;
    [SerializeField] private Button m_nextSpriteButton;

    [SerializeField] private Button m_previousColorButton;
    [SerializeField] private Image m_colorIcon;
    [SerializeField] private Button m_nextColorButton;

    private void Start()
    {
        // Sprite
        if (m_spriteId)
        {
            UpdateSpriteId();
            m_previousSpriteButton.onClick.AddListener(() =>
            {
                m_customizableElement.PreviousSprite();
                UpdateSpriteId();
            });
            m_nextSpriteButton.onClick.AddListener(() =>
            {
                m_customizableElement.NextSprite();
                UpdateSpriteId();
            });
        }

        // Color
        if (m_colorIcon)
        {
            UpdateColorIcon();
            m_previousColorButton.onClick.AddListener(() =>
            {
                m_customizableElement.PreviousColor();
                UpdateColorIcon();
            });
            m_nextColorButton.onClick.AddListener(() =>
            {
                m_customizableElement.NextColor();
                UpdateColorIcon();
            });
        }
    }

    public void UpdateSpriteId()
    {
        if (!m_spriteId) return;
        m_spriteId.SetText((m_customizableElement.SpriteIndex + 1).ToString().PadLeft(2, '0'));
    }

    public void UpdateColorIcon()
    {
        if (!m_colorIcon) return;
        m_colorIcon.color = m_customizableElement.CurrentColor;
    }
}
