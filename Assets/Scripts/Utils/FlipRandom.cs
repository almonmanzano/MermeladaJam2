using UnityEngine;

public class FlipRandom : MonoBehaviour
{
    private void Start()
    {
        float rnd = Random.value;
        GetComponent<SpriteRenderer>().flipX = rnd < 0.5f;
    }
}
