using UnityEngine;

public class MoveInDirection2D : MonoBehaviour
{
    [SerializeField] private float m_moveSpeed = 10f;

    private Vector2 m_direction;
    private bool m_isMoving = false;

    private void Update()
    {
        if (!m_isMoving)
        {
            return;
        }

        float angle = Mathf.Atan2(m_direction.y, m_direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + m_direction, m_moveSpeed * Time.deltaTime);
    }

    public void SetDirection(Vector2 direction)
    {
        m_direction = direction;
        m_isMoving = true;
    }
}
