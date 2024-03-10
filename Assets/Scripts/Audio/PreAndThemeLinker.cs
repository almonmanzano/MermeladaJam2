using System.Collections;
using UnityEngine;

public class PreAndThemeLinker : MonoBehaviour
{
    [SerializeField] private AudioClip m_theme;
    [SerializeField] private float m_preDuration = 1.7f;

    private AudioSource m_audioSource;

    private void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlayAfter(m_theme, m_preDuration));
    }

    private IEnumerator PlayAfter(AudioClip clip, float duration)
    {
        yield return new WaitForSeconds(duration);
        m_audioSource.clip = clip;
        m_audioSource.Play();
    }
}
