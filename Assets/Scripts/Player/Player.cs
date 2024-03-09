using UnityEngine;

public class Player : MonoBehaviour
{
    //[SerializeField] private float m_enlargeSpeed = 0.01f;
    [SerializeField] private float m_upgradeMultiplier = 0.05f;
    [SerializeField] private GameObject m_beerIndicator;

    private PlayerAttack m_playerAttack;
    private MoveToCursor2D m_playerMovement;

    private bool m_hasBeer = false;

    private void Start()
    {
        m_playerAttack = GetComponent<PlayerAttack>();
        m_playerMovement = GetComponent<MoveToCursor2D>();
    }

    private void SetBeer(bool hasBeer)
    {
        m_hasBeer = hasBeer;
        m_beerIndicator.SetActive(hasBeer);
    }

    public void DrinkBeer()
    {
        SetBeer(true);
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

    public bool HasBeer()
    {
        return m_hasBeer;
    }

    public void CollideWithEnemy(GameObject enemy)
    {
        if (m_hasBeer)
        {
            SetBeer(false);
            Destroy(enemy);
        }
        else
        {
            GameController.Instance.GameOver();
        }
    }

    //private void Update()
    //{
    //    if (GameController.Instance.IsGameOver()) return;

    //    //transform.localScale += Vector3.one * m_enlargeSpeed * Time.deltaTime;
    //}
}
