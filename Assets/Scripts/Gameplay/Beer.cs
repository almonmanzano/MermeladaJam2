using System.Collections;
using UnityEngine;

public class Beer : MonoBehaviour
{
    [SerializeField] private float m_timeBeforeReduce = 1f;
    [SerializeField] private float m_reducingTime = 5f;

    private void Start()
    {
        StartCoroutine(StartReducing());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if (!player.HasBeer())
            {
                player.DrinkBeer();
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator StartReducing()
    {
        yield return new WaitForSeconds(m_timeBeforeReduce);
        StartCoroutine(Reduce(m_reducingTime));
    }

    private IEnumerator Reduce(float duration)
    {
        float t = 0f;
        while (!Mathf.Approximately(transform.localScale.x, 0f))
        {
            t += Time.deltaTime / duration;
            transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, t);
            yield return new WaitForEndOfFrame();
        }
        transform.localScale = Vector3.zero;
        Destroy(gameObject);
    }
}
