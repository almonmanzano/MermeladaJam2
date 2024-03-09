using System.Linq;
using UnityEngine;

public class CustomizedCharacterElement : MonoBehaviour
{
    [field: SerializeField] public CustomizationType Type { get; private set; }

    [SerializeField] private CustomizedCharacter m_character;

    private SpriteRenderer m_spriteRenderer;

    private void Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        var customization = m_character.Data.FirstOrDefault(d => d.Type == Type);
        if (customization == null) return;

        m_spriteRenderer.sprite = customization.Sprite.sprite;
        m_spriteRenderer.color = customization.Color;
        transform.localPosition = customization.Sprite.position;
    }
}
