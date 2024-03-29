using System.Collections;
using UnityEngine;

public class ColorBall : MonoBehaviour
{
    [SerializeField] private float m_enlargeDuration = 1f;
    [SerializeField] private GameObject m_colorBallEatenVFX;

    private float m_lifeDuration = 20f;
    private float m_reduceDuration = 7f;
    private Vector3 m_originalScale;

    private bool m_inArea = false;

    private void Start()
    {
        m_originalScale = transform.localScale * Random.Range(0.85f, 1.15f);
        transform.localScale = Vector3.zero;
        StartCoroutine(Enlarge(m_enlargeDuration));
    }

    private IEnumerator Enlarge(float duration)
    {
        float t = 0f;
        while (!Mathf.Approximately(Vector3.Distance(transform.localScale, m_originalScale), 0f))
        {
            t += Time.deltaTime / duration;
            transform.localScale = Vector3.Lerp(Vector3.zero, m_originalScale, t);
            yield return new WaitForEndOfFrame();
        }
        transform.localScale = m_originalScale;

        yield return new WaitForSeconds(m_lifeDuration - m_reduceDuration);

        if (!m_inArea)
        {
            StartCoroutine(Reduce(m_reduceDuration));
        }
    }

    private IEnumerator Reduce(float duration)
    {
        float t = 0f;
        while (!Mathf.Approximately(transform.localScale.x, 0f))
        {
            if (!m_inArea)
            {
                t += Time.deltaTime / duration;
                transform.localScale = Vector3.Lerp(m_originalScale, Vector3.zero, t);
            }
            yield return new WaitForEndOfFrame();
        }
        transform.localScale = Vector3.zero;
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(tag))
        {
            m_inArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(tag))
        {
            m_inArea = false;
        }
    }

    public IEnumerator BeSacrified(float duration)
    {
        Vector3 currentScale = transform.localScale;
        Destroy(gameObject.GetComponent<Collider2D>());
        float t = 0f;
        while (!Mathf.Approximately(transform.localScale.x, 0f))
        {
            t += Time.deltaTime / duration;
            transform.localScale = Vector3.Lerp(currentScale, Vector3.zero, t);
            yield return new WaitForEndOfFrame();
        }
        transform.localScale = Vector3.zero;
        Destroy(gameObject);
    }

    public void BeEaten()
    {
        Instantiate(m_colorBallEatenVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
