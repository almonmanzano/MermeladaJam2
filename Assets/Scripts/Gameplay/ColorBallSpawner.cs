using System.Collections;
using UnityEngine;

public class ColorBallSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] m_colorBallPrefabs;
    [SerializeField] private float m_minTimeBtwSpawns = 0.5f;
    [SerializeField] private float m_maxTimeBtwSpawns = 1.5f;
    [SerializeField] private Transform m_upperLeftBound;
    [SerializeField] private Transform m_bottomRightBound;

    private void Start()
    {
        StartCoroutine(SpawnColorBall());
    }

    private IEnumerator SpawnColorBall()
    {
        yield return new WaitForSeconds(Random.Range(m_minTimeBtwSpawns, m_maxTimeBtwSpawns));
        float x = Random.Range(m_upperLeftBound.position.x, m_bottomRightBound.position.x);
        float y = Random.Range(m_upperLeftBound.position.y, m_bottomRightBound.position.y);
        GameObject colorBallObj = m_colorBallPrefabs[Random.Range(0, m_colorBallPrefabs.Length)];
        Instantiate(colorBallObj, new Vector2(x, y), Quaternion.identity);

        StartCoroutine(SpawnColorBall());
    }
}
