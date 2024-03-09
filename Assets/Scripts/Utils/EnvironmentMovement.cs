using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnvironmentMovement : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetTrigger("move");
        }
    }
}
