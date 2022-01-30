using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFx : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (!_audioSource.isPlaying)
            gameObject.SetActive(false);
    }
}
