using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float m_fireballCooldown = 0.3f;
    [SerializeField] private Transform m_fireballOrigin;
    [SerializeField] private GameObject m_fireball;
    [SerializeField] private float m_fireballMoveSpeed = 30f;
    [SerializeField] private AudioClip[] m_attackSFXs;

    private float m_timeBtwFireball;
    private AudioRandomizer m_audioRandomizer;

    private void Awake()
    {
        m_audioRandomizer = GetComponent<AudioRandomizer>();
    }

    public void UpgradeProjectileSpeed(float multiplier)
    {
        m_fireballMoveSpeed *= (1f + multiplier);
    }

    public void UpgradeProjectileCooldown(float multiplier)
    {
        m_fireballCooldown *= (1f - multiplier);
    }

    private void Update()
    {
        if (GameController.Instance.IsGameOver()) return;

        // Fireball
        if (m_timeBtwFireball <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                m_audioRandomizer.PlayRandom(m_attackSFXs);

                GameObject fireball = Instantiate(m_fireball, m_fireballOrigin.position, transform.rotation);
                fireball.GetComponent<MoveInDirection2D>().SetMoveSpeed(m_fireballMoveSpeed);

                m_timeBtwFireball = m_fireballCooldown;
            }
        }
        else
        {
            m_timeBtwFireball -= Time.deltaTime;
        }
    }
}
