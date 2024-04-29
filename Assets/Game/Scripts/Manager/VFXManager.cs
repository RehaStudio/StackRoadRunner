using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    #region Fields
    public ParticleSystem[] CollectibleVFX;
    #endregion
    public void PlayVFX(CollectibleType type,Vector3 position)
    {
        CollectibleVFX[((int)type)].transform.position = position;
        CollectibleVFX[((int)type)].Play();
    }
}
