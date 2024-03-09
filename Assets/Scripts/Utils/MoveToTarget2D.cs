using UnityEngine;

public class MoveToTarget2D : MonoBehaviour
{
    [SerializeField] private float m_moveSpeed = 10f;

    private Transform m_target = null;

    private void Update()
    {
        if (m_target == null)
        {
            return;
        }

        transform.position = Vector2.MoveTowards(transform.position, m_target.position, m_moveSpeed * Time.deltaTime);
    }

    public void SetTarget(Transform target)
    {
        m_target = target;
        transform.LookAt(m_target.position);
    }
}
