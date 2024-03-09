using UnityEngine;

public class MoveToCursor2D : MonoBehaviour
{
    [SerializeField] private float m_rotationSpeed = 25f;
    [SerializeField] private float m_moveSpeed = 10f;

    private Vector2 m_direction;

    private void Update()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Rotation
        m_direction = cursorPos - (Vector2)transform.position;
        if (!Mathf.Approximately(m_direction.magnitude, 0f))
        {
            float angle = Mathf.Atan2(m_direction.y, m_direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, m_rotationSpeed * Time.deltaTime);
        }

        // Movement
        transform.position = Vector2.MoveTowards(transform.position, cursorPos, m_moveSpeed * Time.deltaTime);
    }
}
