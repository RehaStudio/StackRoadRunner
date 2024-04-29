using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CollectibleObject : MonoBehaviour, IInteract<Player>
{
    private VFXManager _VFXManager;
    public VFX_Type CollectVFXType;
    [Inject]
    private void Constructor(VFXManager vfxManager)
    {
        _VFXManager = vfxManager;
    }
    public void Interact(Player player)
    {
        gameObject.SetActive(false);
        _VFXManager.PlayVFX(CollectVFXType, transform.position);
    }
}
