using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float m_startTimeBtwAttack = 0.3f;
    [SerializeField] private float m_startTimeBtwFireball = 0.3f;
    [SerializeField] private Transform m_attackPos;
    [SerializeField] private LayerMask m_enemiesLayers;
    [SerializeField] private float m_attackRange = 1.5f;
    //[SerializeField] private int m_damage = 1;
    [SerializeField] private Transform m_fireballOrigin;
    [SerializeField] private GameObject m_fireball;

    private float m_timeBtwAttack;
    private float m_timeBtwFireball;

    private void Update()
    {
        // Atack
        if (m_timeBtwAttack <= 0)
        {
            // Then the player can attack
            if (Input.GetMouseButton(0))
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(m_attackPos.position, m_attackRange, m_enemiesLayers);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    // Damage enemy
                    //enemiesToDamage[i].GetComponent<Health>().TakeDamage(m_damage);
                    print("Damage enemy: " + enemiesToDamage[i].name);
                }

                m_timeBtwAttack = m_startTimeBtwAttack;

                //CinemachineShake.Instance.ShakeCamera(5f, 0.3f);
            }
        }
        else
        {
            m_timeBtwAttack -= Time.deltaTime;
        }

        // Fireball
        if (m_timeBtwFireball <= 0)
        {
            if (Input.GetMouseButton(1))
            {
                GameObject fireball = Instantiate(m_fireball, m_fireballOrigin.position, Quaternion.identity);
                Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
                fireball.GetComponent<MoveInDirection2D>().SetDirection(direction);

                m_timeBtwFireball = m_startTimeBtwFireball;
            }
        }
        else
        {
            m_timeBtwFireball -= Time.deltaTime;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(m_attackPos.position, m_attackRange);
    }
}
