using UnityEngine;

public class Player : MonoBehaviour
{
    //[SerializeField] private float m_enlargeSpeed = 0.01f;
    [SerializeField] private float m_upgradeMultiplier = 0.05f;

    private PlayerAttack m_playerAttack;
    private MoveToCursor2D m_playerMovement;

    private void Start()
    {
        m_playerAttack = GetComponent<PlayerAttack>();
        m_playerMovement = GetComponent<MoveToCursor2D>();
    }

    public void UpgradeMoveSpeed()
    {
        m_playerMovement.UpgradeMoveSpeed(m_upgradeMultiplier);
    }

    public void UpgradeProjectileSpeed()
    {
        m_playerAttack.UpgradeProjectileSpeed(m_upgradeMultiplier);
    }

    public void UpgradeProjectileCooldown()
    {
        m_playerAttack.UpgradeProjectileCooldown(m_upgradeMultiplier);
    }

    //private void Update()
    //{
    //    if (GameController.Instance.IsGameOver()) return;

    //    //transform.localScale += Vector3.one * m_enlargeSpeed * Time.deltaTime;
    //}
}
