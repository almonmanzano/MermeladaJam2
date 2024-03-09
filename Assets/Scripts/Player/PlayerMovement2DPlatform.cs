using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class PlayerMovement2DPlatform : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float checkRadius = 0.1f;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float speed = 4f;
    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private GameObject dustFX;
    [SerializeField] private GameObject trailFX;
    [SerializeField] private AudioClip landingSound;
    [SerializeField] private int extraJumpsValue;

    private Rigidbody2D rb;
    private Animator anim;
    private AudioSource source;
    private bool isGrounded;
    private float moveInput;
    private bool spawnDust;
    private float timeBtwTrail;
    private float startTimeBtwTrail = 3f;
    private int extraJumps;

    private void Start()
    {
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if (moveInput != 0f)
        {
            float y = moveInput > 0f ? 0f : 180f;
            transform.eulerAngles = new Vector3(0f, y, 0f);
        }

        if (isGrounded)
        {
            bool isRunning = moveInput != 0f;
            anim.SetBool("isRunning", isRunning);
        }
    }

    private void Update()
    {
        if (moveInput != 0f)
        {
            if (timeBtwTrail <= 0f)
            {
                if (trailFX)
                    Instantiate(trailFX, groundCheck.position, Quaternion.identity);
                timeBtwTrail = startTimeBtwTrail;
            }
            else
            {
                timeBtwTrail -= Time.deltaTime;
            }
        }

        if (isGrounded)
        {
            extraJumps = extraJumpsValue;

            if (spawnDust)
            {
                if (landingSound)
                {
                    source.clip = landingSound;
                    source.Play();
                }
                Camera.main.GetComponent<CameraShake>().Shake(0.1f, 0.97f);
                if (dustFX)
                    Instantiate(dustFX, groundCheck.position, Quaternion.identity);
                spawnDust = false;
            }
        }
        else
        {
            spawnDust = true;
        }

        //if (isGrounded && Input.GetKey(KeyCode.Space))
        bool canJump = extraJumps > 0 || isGrounded;
        if (Input.GetKey(KeyCode.Space) && canJump)
        {
            if (dustFX)
                Instantiate(dustFX, groundCheck.position, Quaternion.identity);
            anim.SetTrigger("jump");
            rb.velocity = Vector2.up * jumpForce;
            if (extraJumps > 0)
                extraJumps--;
        }
    }
}
