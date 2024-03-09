using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private string m_tag;
    [SerializeField] private GameObject m_destructionFx;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(m_tag))
        {
            if (m_destructionFx)
            {
                Instantiate(m_destructionFx, transform.position, Quaternion.identity);
            }
            Camera.main.GetComponent<CameraShake>().Shake(0.1f, 0.99f);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
