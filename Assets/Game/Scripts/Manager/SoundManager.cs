using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource _AudioSource;

    public void PlayStackPerfectMatch(int level)
    {
        _AudioSource.pitch = 1f * Mathf.Pow(1.06f, Mathf.Min(level, 15) * 2);
        _AudioSource.Play();
    }
}
