using UnityEngine;

[System.Serializable]
public class PositionedSprite
{
    [field: SerializeField] public Sprite sprite { get; private set; }
    [field: SerializeField] public Vector2 position { get; set; }
}
