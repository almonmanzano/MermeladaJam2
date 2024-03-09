using UnityEngine;

[System.Serializable]
public class CustomizationData
{
    [field: SerializeField] public CustomizationType Type { get; private set; }
    [field: SerializeField] public PositionedSprite Sprite { get; private set; }
    [field: SerializeField] public Color Color { get; private set; }

    public CustomizationData(CustomizationType type, PositionedSprite sprite, Color color)
    {
        Type = type;
        Sprite = sprite;
        Color = color;
    }
}
