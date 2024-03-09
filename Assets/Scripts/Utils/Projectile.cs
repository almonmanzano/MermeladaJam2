using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private string m_tag;
    [SerializeField] private GameObject m_destructionFx;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(m_tag))
        {
            // Damage enemy
            //collision.GetComponent<Health>().TakeDamage(?);
            print("Damage enemy: " + collision.name);
            if (m_destructionFx)
            {
                Instantiate(m_destructionFx, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
