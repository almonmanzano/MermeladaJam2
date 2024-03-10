using UnityEngine;

public class MoveInDirection2D : MonoBehaviour
{
    private float m_moveSpeed = 30f;

    public void SetMoveSpeed(float moveSpeed)
    {
        m_moveSpeed = moveSpeed;
    }

    private void Update()
    {
        transform.position += transform.right * m_moveSpeed * Time.deltaTime;
        transform.GetChild(0).rotation = Quaternion.identity;
    }
}
