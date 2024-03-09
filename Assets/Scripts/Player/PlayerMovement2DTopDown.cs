using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class PlayerMovement2DTopDown : MonoBehaviour
{
    [SerializeField] private float m_moveSpeed = 5f;
    [SerializeField] private bool m_canDash = false;
    [SerializeField] private float m_dashSpeed = 25f;
    [SerializeField] private float m_dashDuration = 0.3f;
    [SerializeField] private float m_dashCd = 1f;
    [SerializeField] private GameObject m_dashFX;

    private Rigidbody2D m_rb;
    private Camera m_camera;
    private TrailRenderer m_trailRenderer;

    private Vector2 m_movement;
    private Vector2 m_mousePos;
    private bool m_isDashing = false;
    private float m_dashTime = 0f;
    private float m_timeSinceDash;

    private void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_camera = Camera.main;
        m_trailRenderer = GetComponent<TrailRenderer>();
        m_timeSinceDash = m_dashCd;
    }

    private void Update()
    {
        m_movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        m_mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);

        // Dash
        if (m_canDash)
        {
            if (!m_isDashing)
            {
                if (Input.GetKeyDown(KeyCode.Space) && m_movement != Vector2.zero && m_timeSinceDash >= m_dashCd)
                {
                    m_isDashing = true;
                    m_dashTime = 0f;
                    m_timeSinceDash = 0f;
                    Instantiate(m_dashFX, transform.position, transform.rotation);
                    if (m_trailRenderer)
                    {
                        m_trailRenderer.emitting = true;
                    }
                }
                else
                {
                    m_timeSinceDash += Time.deltaTime;
                }
            }
            else
            {
                m_dashTime += Time.deltaTime;
                if (m_dashTime >= m_dashDuration)
                {
                    m_isDashing = false;
                    if (m_trailRenderer)
                    {
                        m_trailRenderer.emitting = false;
                    }
                }
            }
        }
    }

    private void FixedUpdate()
    {
        // Movement
        float speed = m_isDashing ? m_dashSpeed : m_moveSpeed;
        m_rb.velocity = m_movement * speed;

        // Rotation
        Vector2 lookDir = m_mousePos - m_rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        m_rb.rotation = angle;
    }
}
