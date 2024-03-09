using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent2D : MonoBehaviour
{
    [SerializeField] private string m_tag = "Player";
    [SerializeField] private bool m_oneTime = true;
    [SerializeField] private UnityEvent m_event;

    private bool m_triggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(m_tag) || (m_oneTime && m_triggered))
        {
            return;
        }

        m_triggered = true;
        m_event.Invoke();
    }
}
