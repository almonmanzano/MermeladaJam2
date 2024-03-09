using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float m_enlargeSpeed = 0.3f;

    private void Update()
    {
        if (GameController.Instance.IsGameOver()) return;

        transform.localScale += Vector3.one * m_enlargeSpeed * Time.deltaTime;
    }
}
