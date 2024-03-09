using UnityEngine;

public class PositionRendererSorter : MonoBehaviour
{
    [SerializeField] private int m_sortingOrderBase = 5000;
    [SerializeField] private int m_offset = -10;
    [SerializeField] private bool m_runOnlyOnce = false;

    private float m_timer;
    private float m_timerMax = 0.1f;
    private Renderer m_renderer;

    private void Awake()
    {
        m_renderer = GetComponent<Renderer>();
    }

    private void LateUpdate()
    {
        m_timer -= Time.deltaTime;
        if (m_timer <= 0f)
        {
            m_timer = m_timerMax;
            m_renderer.sortingOrder = (int)(m_sortingOrderBase - transform.position.y - m_offset);
            if (m_runOnlyOnce)
            {
                Destroy(this);
            }
        }
    }
}
