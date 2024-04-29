using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Level : MonoBehaviour
{
    #region Fields
    public int StackCount;
    public List<Transform> PositionsStar;
    public List<Transform> PositionCoin;
    public List<Transform> PositionDiamonds;

    private CollectibleObject.Pool _CollectibleObjectPool;
    private List<CollectibleObject> _CollectibleObjects = new List<CollectibleObject>();
    private float _StartPosition;
    #endregion
    [Inject]
    private void Constructor(CollectibleObject.Pool pool)
    {
        _CollectibleObjectPool = pool;
    }
    public void CreateCollectibles(float _startPosition)
    {
        _StartPosition = _startPosition;
        CreateStars();
        CreateCoins();
        CreateDiamonds();
    }
    public void DespawnLevelCollectibles()
    {
        foreach (var item in _CollectibleObjects)
        {
            if (item.gameObject.activeSelf)
                _CollectibleObjectPool.Despawn(item);
        }
        _CollectibleObjects.Clear();
    }
    private void CreateStars()
    {
        PositionsStar.ForEach(e =>
        {
            CollectibleObject collectibleObject = _CollectibleObjectPool.Spawn();
            collectibleObject.transform.position = e.position + _StartPosition * Vector3.forward;
            collectibleObject.SetType(CollectibleType.Star);
            _CollectibleObjects.Add(collectibleObject);
        });
    }
    private void CreateCoins()
    {
        PositionCoin.ForEach(e =>
        {
            CollectibleObject collectibleObject = _CollectibleObjectPool.Spawn();
            collectibleObject.transform.position = e.position + _StartPosition * Vector3.forward;
            collectibleObject.SetType(CollectibleType.Coin);
            _CollectibleObjects.Add(collectibleObject);
        });

    }
    private void CreateDiamonds()
    {
        PositionDiamonds.ForEach(e =>
        {
            CollectibleObject collectibleObject = _CollectibleObjectPool.Spawn();
            collectibleObject.transform.position = e.position + _StartPosition * Vector3.forward;
            collectibleObject.SetType(CollectibleType.Diamond);
            _CollectibleObjects.Add(collectibleObject);
        });
    }
}
