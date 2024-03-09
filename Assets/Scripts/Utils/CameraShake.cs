using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private bool useSize;

    private Camera m_cam;
    private float m_originalSize;

    private void Start()
    {
        m_cam = GetComponent<Camera>();
        m_originalSize = m_cam.orthographicSize;
    }

    public void Shake(float duration, float magnitude)
    {
        if (useSize)
        {
            StartCoroutine(ShakeUsingSize(duration, magnitude));
        }
        else
        {
            StartCoroutine(ShakeUsingPosition(duration, magnitude));
        }
    }

    private IEnumerator ShakeUsingSize(float duration, float magnitude)
    {
        float otherSize = m_originalSize * magnitude;

        duration /= 2f;
        float t = 0f;
        while (!Mathf.Approximately(m_cam.orthographicSize, otherSize))
        {
            t += Time.deltaTime / duration;
            m_cam.orthographicSize = Mathf.Lerp(m_originalSize, otherSize, t);
            yield return new WaitForEndOfFrame();
        }

        otherSize = m_cam.orthographicSize;

        t = 0f;
        while (!Mathf.Approximately(m_cam.orthographicSize, m_originalSize))
        {
            t += Time.deltaTime / duration;
            m_cam.orthographicSize = Mathf.Lerp(otherSize, m_originalSize, t);
            yield return new WaitForEndOfFrame();
        }

        m_cam.orthographicSize = m_originalSize;
    }

    private IEnumerator ShakeUsingPosition(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }
}
