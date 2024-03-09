using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private string m_targetType = "ColorBall";
    [SerializeField] private float m_rotationSpeed = 25f;
    [SerializeField] private float m_moveSpeed = 5f;

    private void Update()
    {
        GameObject target = GetTarget();
        if (target != null)
        {
            // Rotation
            Vector3 direction = target.transform.position - transform.position;
            if (!Mathf.Approximately(direction.magnitude, 0f))
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, m_rotationSpeed * Time.deltaTime);
            }

            // Movement
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, m_moveSpeed * Time.deltaTime);
        }
    }

    private GameObject GetTarget()
    {
        // Target is Player
        if (m_targetType == "Player")
        {
            return GameObject.FindWithTag("Player");
        }

        // Target is ColorBall
        float minDist = Mathf.Infinity;
        GameObject closestTarget = null;
        if (m_targetType == "ColorBall")
        {
            ColorBall[] colorBalls = FindObjectsOfType<ColorBall>();
            foreach (ColorBall colorBall in colorBalls)
            {
                float dist = Vector3.Distance(transform.position, colorBall.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    closestTarget = colorBall.gameObject;
                }
            }
        }
        return closestTarget;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ColorBall colorBall;
        if (collision.TryGetComponent(out colorBall))
        {
            Destroy(colorBall.gameObject);
        }
    }
}
