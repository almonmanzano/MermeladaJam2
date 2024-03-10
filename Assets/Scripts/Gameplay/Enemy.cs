using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private string m_targetType = "ColorBall";
    [SerializeField] private float m_rotationSpeed = 25f;
    [SerializeField] private float m_beerProbability = 0.05f;
    [SerializeField] private GameObject m_beerPrefab;
    [SerializeField] private GameObject m_deathVFX;
    [SerializeField] private AudioClip[] m_deathSFXs; 
    
    private float m_moveSpeed = 5f;

    public void SetMoveSpeed(float moveSpeed)
    {
        m_moveSpeed = moveSpeed;
    }

    private void Update()
    {
        if (GameController.Instance.IsGameOver()) return;

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
        // Target is ColorBall
        if (m_targetType == "ColorBall")
        {
            float minDist = Mathf.Infinity;
            GameObject closestTarget = null;
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
            if (closestTarget != null)
            {
                return closestTarget;
            }
        }

        // Target is Player, or there are no targets
        return GameObject.FindWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ColorBall colorBall;
        if (collision.TryGetComponent(out colorBall))
        {
            colorBall.BeEaten();
        }
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().CollideWithEnemy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (!gameObject.scene.isLoaded) return; // To avoid error "some objects were not cleaned up when closing the scene"

        // Beer
        if (Random.value < m_beerProbability)
        {
            Instantiate(m_beerPrefab, transform.position, Quaternion.identity);
            GameController.Instance.PlayBeerSFX();
        }

        Instantiate(m_deathVFX, transform.position, Quaternion.identity);

        PlayDeathSFX();
    }

    private void PlayDeathSFX()
    {
        int rnd = Random.Range(0, m_deathSFXs.Length);
        if (m_targetType == "Player")
        {
            GameController.Instance.Monster1Death(m_deathSFXs[rnd]);
        }
        else // if (m_targetType == "ColorBall")
        {
            GameController.Instance.Monster2Death(m_deathSFXs[rnd]);
        }
    }
}
