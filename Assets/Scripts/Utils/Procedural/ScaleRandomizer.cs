using UnityEngine;

public class ScaleRandomizer : MonoBehaviour
{
    [SerializeField] private float m_scaleDiffPerc = 0.15f;

    private void Start()
    {
        transform.localScale *= Random.Range(1f - m_scaleDiffPerc, 1f + m_scaleDiffPerc);
    }
}
