using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CollectibleObject : MonoBehaviour, IInteract<Player>
{
    #region 
    [SerializeField] private List<GameObject> CollectibleObjects;
    private VFXManager _VFXManager;
    private CollectibleObject.Pool _CollectibleObjectPool;
    public CollectibleType CollectibleType;
    #endregion
    [Inject]
    private void Constructor(VFXManager vfxManager,CollectibleObject.Pool pool)
    {
        _VFXManager = vfxManager;
        _CollectibleObjectPool = pool;
    }
    public void SetType(CollectibleType type)
    {
        CollectibleType = type;
        CollectibleObjects.ForEach(e => e.SetActive(false));
        CollectibleObjects[((int)type)].SetActive(true);
    }
    public void Interact(Player player)
    {
        _CollectibleObjectPool.Despawn(this);
        _VFXManager.PlayVFX(CollectibleType, transform.position);
    }
    public class Pool : MemoryPool<CollectibleObject>
    {
        protected override void OnDespawned(CollectibleObject _collectibleObject)
        {
            _collectibleObject.gameObject.SetActive(false);
            base.OnDespawned(_collectibleObject);
        }
        protected override void OnSpawned(CollectibleObject _collectibleObject)
        {
            _collectibleObject.gameObject.SetActive(true);
            base.OnSpawned(_collectibleObject);
        }
    }
}
