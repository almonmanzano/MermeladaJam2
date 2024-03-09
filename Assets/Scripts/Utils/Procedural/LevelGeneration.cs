using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    [SerializeField] GameObject[] m_objects;

    private void Start()
    {
        int rand = Random.Range(0, m_objects.Length);
        Instantiate(m_objects[rand], transform.position, Quaternion.identity);
    }
}
