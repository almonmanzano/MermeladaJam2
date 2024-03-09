using UnityEngine;

public class MoveToCursor2D : MonoBehaviour
{
    [SerializeField] private float m_rotationSpeed = 25f;
    [SerializeField] private float m_moveSpeed = 10f;

    [SerializeField] private Transform m_upperLeftBound;
    [SerializeField] private Transform m_bottomRightBound;

    private Vector2 m_direction;

    public void UpgradeMoveSpeed(float multiplier)
    {
        m_moveSpeed *= (1f + multiplier);
    }

    private void Update()
    {
        if (GameController.Instance.IsGameOver()) return;

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
        Vector2 nextPos = (Vector2)transform.position + cursorPos * m_moveSpeed * Time.deltaTime;
        bool canMoveInX = nextPos.x > m_upperLeftBound.position.x && nextPos.x < m_bottomRightBound.position.x;
        bool canMoveInY = nextPos.y > m_upperLeftBound.position.y && nextPos.y < m_bottomRightBound.position.y;
        if (canMoveInX && canMoveInY)
        {
            transform.position = Vector2.MoveTowards(transform.position, cursorPos, m_moveSpeed * Time.deltaTime);
        }
    }
}
