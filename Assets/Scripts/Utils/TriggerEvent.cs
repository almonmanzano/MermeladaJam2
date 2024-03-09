using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    [SerializeField] private string m_tag = "Player";
    [SerializeField] private UnityEvent m_eventFirstTime;
    [SerializeField] private UnityEvent m_eventEveryTime;

    private bool m_triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(m_tag))
        {
            return;
        }

        if (!m_triggered)
        {
            m_triggered = true;
            m_eventFirstTime.Invoke();
        }

        m_eventEveryTime.Invoke();
    }
}