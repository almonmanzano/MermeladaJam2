using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] m_enemyPrefabs;
    [SerializeField] private float m_minTimeBtwSpawns = 0.5f;
    [SerializeField] private float m_maxTimeBtwSpawns = 1.5f;
    [SerializeField] private Transform m_upperLeftBound;
    [SerializeField] private Transform m_bottomRightBound;

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(Random.Range(m_minTimeBtwSpawns, m_maxTimeBtwSpawns));
        float x = Random.Range(m_upperLeftBound.position.x, m_bottomRightBound.position.x);
        float y = Random.Range(m_upperLeftBound.position.y, m_bottomRightBound.position.y);
        GameObject enemyObj = m_enemyPrefabs[Random.Range(0, m_enemyPrefabs.Length)];
        Instantiate(enemyObj, new Vector2(x, y), Quaternion.identity);

        StartCoroutine(SpawnEnemy());
    }
}
