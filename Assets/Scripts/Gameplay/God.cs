using UnityEngine;
using UnityEngine.UI;

public class God : MonoBehaviour
{
    [SerializeField] private Slider m_patienceSlider;
    [SerializeField] private Image m_dangerImage;
    [SerializeField] private float m_dangerZone = 0.25f;
    [SerializeField] private float m_patienceDecreasingSpeed = 1f;
    [SerializeField] private float m_patienceDecreaseMultiplier = 0.05f;
    [SerializeField] private float m_totalPatience = 100f;

    [SerializeField] private Animator m_upgradeMoveSpeedAnim;
    [SerializeField] private Animator m_upgradeProjectileSpeedAnim;
    [SerializeField] private Animator m_upgradeProjectileCooldownAnim;

    private float m_patience;
    private Player m_player;
    private AudioSource m_audioSource;

    private void Start()
    {
        m_patience = m_totalPatience;
        m_player = FindObjectOfType<Player>();
        m_audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (GameController.Instance.IsGameOver()) return;

        m_patience -= m_patienceDecreasingSpeed * Time.deltaTime;
        CheckPatience();
        UpdatePatienceSlider();
    }

    public void Sacrifice()
    {
        m_audioSource.Play();
        m_patience = m_totalPatience;
        UpdatePatienceSlider();
        GameController.Instance.AddSacrifice();
        UpgradePlayer();
        m_patienceDecreasingSpeed *= (1f + m_patienceDecreaseMultiplier);
    }

    private void UpgradePlayer()
    {
        float rnd = Random.Range(0, 3);
        if (rnd == 0)
        {
            // Upgrade move speed
            m_player.UpgradeMoveSpeed();
            m_upgradeMoveSpeedAnim.SetTrigger("ShowAndHide");
        }
        else if (rnd == 1)
        {
            // Upgrade projectile speed
            m_player.UpgradeProjectileSpeed();
            m_upgradeProjectileSpeedAnim.SetTrigger("ShowAndHide");
        }
        else if (rnd == 2)
        {
            // Upgrade projectile cooldown
            m_player.UpgradeProjectileCooldown();
            m_upgradeProjectileCooldownAnim.SetTrigger("ShowAndHide");
        }
    }

    private void CheckPatience()
    {
        m_dangerImage.enabled = m_patience / m_totalPatience < m_dangerZone;

        if (m_patience <= 0f)
        {
            GameController.Instance.GameOver();
        }
    }

    private void UpdatePatienceSlider()
    {
        m_patienceSlider.value = m_patience / m_totalPatience;
    }
}
