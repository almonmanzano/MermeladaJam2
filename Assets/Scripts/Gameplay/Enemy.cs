using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float m_rotationSpeed = 25f;
    [SerializeField] private float m_moveSpeed = 5f;

    private void Update()
    {
        ColorBall colorBall = GetClosestBall();
        if (colorBall != null)
        {
            // Rotation
            Vector3 direction = colorBall.transform.position - transform.position;
            if (!Mathf.Approximately(direction.magnitude, 0f))
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, m_rotationSpeed * Time.deltaTime);
            }

            // Movement
            transform.position = Vector2.MoveTowards(transform.position, colorBall.transform.position, m_moveSpeed * Time.deltaTime);
        }
    }

    private ColorBall GetClosestBall()
    {
        float minDist = Mathf.Infinity;
        ColorBall closestBall = null;
        ColorBall[] colorBalls = FindObjectsOfType<ColorBall>();
        foreach (ColorBall colorBall in colorBalls)
        {
            float dist = Vector3.Distance(transform.position, colorBall.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closestBall = colorBall;
            }
        }
        return closestBall;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ColorBall colorBall;
        if (collision.TryGetComponent(out colorBall))
        {
            Destroy(colorBall.gameObject);
            Destroy(gameObject);
        }
    }
}
