using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    public ParticleSystem[] CollectibleVFX;
    public void PlayVFX(CollectibleType type,Vector3 position)
    {
        CollectibleVFX[((int)type)].transform.position = position;
        CollectibleVFX[((int)type)].Play();
    }
}
public enum CollectibleType
{
    Coin,
    Star,
    Diamond,
}