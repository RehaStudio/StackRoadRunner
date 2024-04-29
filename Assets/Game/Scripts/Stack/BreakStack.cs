using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BreakStack : MonoBehaviour
{
    private BreakStack.Pool _BreakPool;
    public MeshRenderer MeshRenderer { get; set; }
    private MaterialPropertyBlock _MaterialPropertyBlock; 
    [Inject]
    private void Constructor(BreakStack.Pool pool)
    {
        _BreakPool = pool;
    }
    public void SetColor(Color color)
    {
        if (_MaterialPropertyBlock == null)
            _MaterialPropertyBlock = new MaterialPropertyBlock();
        _MaterialPropertyBlock.SetColor(Constants.Color, color);
        MeshRenderer.SetPropertyBlock(_MaterialPropertyBlock);
    }
    public void SetSize(float size)
    {
        transform.localScale = new Vector3(size, 1f, Constants.StackStepSize);
    }
    public void SetLocalPosition(Vector3 position)
    {
        transform.localPosition = position;
    }
    public void Fall()
    {
        transform.DOMoveY(-5, 2f).SetDelay(0.1f).OnComplete(() => 
        {
            _BreakPool.Despawn(this);
        });
    }
    public class Pool : MemoryPool<BreakStack>
    {
        protected override void OnCreated(BreakStack breakStack)
        {
            breakStack.MeshRenderer = breakStack.GetComponent<MeshRenderer>();
            base.OnCreated(breakStack);
        }
        protected override void OnDespawned(BreakStack breakStack)
        {
            breakStack.gameObject.SetActive(false);
            base.OnDespawned(breakStack);
        }
        protected override void OnSpawned(BreakStack breakStack)
        {
            breakStack.gameObject.SetActive(true);
            base.OnSpawned(breakStack);
        }
    }
}
