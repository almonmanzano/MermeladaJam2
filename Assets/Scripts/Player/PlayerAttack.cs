using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float m_startTimeBtwFireball = 0.3f;
    [SerializeField] private Transform m_fireballOrigin;
    [SerializeField] private GameObject m_fireball;

    private float m_timeBtwFireball;

    private void Update()
    {
        // Fireball
        if (m_timeBtwFireball <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                Instantiate(m_fireball, m_fireballOrigin.position, transform.rotation);

                m_timeBtwFireball = m_startTimeBtwFireball;
            }
        }
        else
        {
            m_timeBtwFireball -= Time.deltaTime;
        }
    }
}
