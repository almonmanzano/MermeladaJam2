using UnityEngine;

public class Tentacle : MonoBehaviour
{
    [SerializeField] private int m_length;
    [SerializeField] private LineRenderer m_lineRend;

    [SerializeField] private bool m_variableLength;

    [SerializeField] private Transform m_targetDir;
    [SerializeField] private float m_targetDist = 0.2f;
    [SerializeField] private float m_smoothSpeed = 0.02f;
    [SerializeField] private float m_trailSpeed = 350f;

    [SerializeField] private Transform m_wiggleDir;
    [SerializeField] private float m_wiggleSpeed = 10f;
    [SerializeField] private float m_wiggleMagnitude = 20f;

    private Vector3[] m_segmentPoses;
    private Vector3[] m_segmentV;

    private void Start()
    {
        m_lineRend.positionCount = m_length;
        m_segmentPoses = new Vector3[m_length];
        m_segmentV = new Vector3[m_length];

        if (m_variableLength)
        {
            m_smoothSpeed /= 20f;
        }

        ResetPos();
    }

    private void Update()
    {
        m_wiggleDir.localRotation = Quaternion.Euler(0f, 0f, Mathf.Sin(Time.time * m_wiggleSpeed) * m_wiggleMagnitude);

        m_segmentPoses[0] = m_targetDir.position;

        for (int i = 1; i < m_segmentPoses.Length; i++)
        {
            if (m_variableLength)
            {
                Vector3 targetPos = m_segmentPoses[i - 1] + (m_segmentPoses[i] - m_segmentPoses[i - 1]).normalized * m_targetDist;
                m_segmentPoses[i] = Vector3.SmoothDamp(m_segmentPoses[i], targetPos, ref m_segmentV[i], m_smoothSpeed);
            }
            else
            {
                m_segmentPoses[i] = Vector3.SmoothDamp(m_segmentPoses[i], m_segmentPoses[i - 1] + m_targetDir.right * m_targetDist, ref m_segmentV[i], m_smoothSpeed + i / m_trailSpeed);
            }
        }
        m_lineRend.SetPositions(m_segmentPoses);
    }

    private void ResetPos()
    {
        m_segmentPoses[0] = m_targetDir.position;
        for (int i = 1; i < m_length; i++)
        {
            m_segmentPoses[i] = m_segmentPoses[i - 1] + m_targetDir.right * m_targetDist;
        }
        m_lineRend.SetPositions(m_segmentPoses);
    }
}
