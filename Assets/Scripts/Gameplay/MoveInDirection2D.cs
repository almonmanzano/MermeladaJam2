using UnityEngine;

public class MoveInDirection2D : MonoBehaviour
{
    [SerializeField] private float m_moveSpeed = 10f;

    private void Update()
    {
        transform.position += transform.right * m_moveSpeed * Time.deltaTime;
    }
}
