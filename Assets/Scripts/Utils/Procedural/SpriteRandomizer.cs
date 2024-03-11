using UnityEngine;

public class SpriteRandomizer : MonoBehaviour
{
    [SerializeField] private Sprite[] m_sprites;

    private SpriteRenderer m_renderer;

    private void Start()
    {
        m_renderer = GetComponent<SpriteRenderer>();
        int rnd = Random.Range(0, m_sprites.Length);
        m_renderer.sprite = m_sprites[rnd];
    }
}
