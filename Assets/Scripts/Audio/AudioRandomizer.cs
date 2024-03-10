using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRandomizer : MonoBehaviour
{
    private AudioSource m_audioSource;

    private void Awake()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    public void PlayRandom(AudioClip[] clips)
    {
        int rnd = Random.Range(0, clips.Length);
        m_audioSource.clip = clips[rnd];
        m_audioSource.Play();
    }
}
