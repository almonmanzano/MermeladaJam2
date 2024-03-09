using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private bool useSize;

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
        Camera cam = GetComponent<Camera>();
        float originalSize = cam.orthographicSize;
        float otherSize = originalSize * magnitude;

        duration /= 2f;
        float t = 0f;
        while (!Mathf.Approximately(cam.orthographicSize, otherSize))
        {
            t += Time.deltaTime / duration;
            cam.orthographicSize = Mathf.Lerp(originalSize, otherSize, t);
            yield return new WaitForEndOfFrame();
        }

        otherSize = cam.orthographicSize;

        t = 0f;
        while (!Mathf.Approximately(cam.orthographicSize, originalSize))
        {
            t += Time.deltaTime / duration;
            cam.orthographicSize = Mathf.Lerp(otherSize, originalSize, t);
            yield return new WaitForEndOfFrame();
        }

        cam.orthographicSize = originalSize;
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
