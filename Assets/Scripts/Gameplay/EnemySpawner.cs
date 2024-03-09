using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] m_enemyPrefabs;
    [SerializeField] private float m_minTimeBtwSpawns = 0.5f;
    [SerializeField] private float m_maxTimeBtwSpawns = 1.5f;
    [SerializeField] private float m_timeBtwSpawnsDecrease = 0.01f;
    [SerializeField] private Transform m_upperLeftBound;
    [SerializeField] private Transform m_bottomRightBound;

    [SerializeField] private float m_initialEnemyMoveSpeed = 5f;
    [SerializeField] private float m_enemyMoveSpeedSum = 0.2f;

    private float m_enemyMoveSpeed;

    private void Start()
    {
        m_enemyMoveSpeed = m_initialEnemyMoveSpeed;
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(Random.Range(m_minTimeBtwSpawns, m_maxTimeBtwSpawns));
        float x = Random.Range(m_upperLeftBound.position.x, m_bottomRightBound.position.x);
        float y = Random.Range(m_upperLeftBound.position.y, m_bottomRightBound.position.y);
        GameObject enemyObj = m_enemyPrefabs[Random.Range(0, m_enemyPrefabs.Length)];
        GameObject enemyInstance = Instantiate(enemyObj, new Vector2(x, y), Quaternion.identity);
        enemyInstance.GetComponent<Enemy>().SetMoveSpeed(m_enemyMoveSpeed);

        if (m_minTimeBtwSpawns > 0.1f) m_minTimeBtwSpawns -= m_timeBtwSpawnsDecrease;
        if (m_maxTimeBtwSpawns > 0.3f) m_maxTimeBtwSpawns -= m_timeBtwSpawnsDecrease;

        m_enemyMoveSpeed += m_enemyMoveSpeedSum;

        StartCoroutine(SpawnEnemy());
    }
}
