using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BreakStack : MonoBehaviour
{
    public MeshRenderer MeshRenderer { get; set; }
    private MaterialPropertyBlock _MaterialPropertyBlock;
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
        transform.DOMoveY(-5, 2f).SetDelay(0.1f);
    }
    public class Pool : MemoryPool<BreakStack>
    {
        protected override void OnCreated(BreakStack breakStack)
        {
            breakStack.MeshRenderer = breakStack.GetComponent<MeshRenderer>();
            base.OnCreated(breakStack);
        }
    }
}
