using DG.Tweening;
using UnityEngine;
using Zenject;

public class Stack : MonoBehaviour
{
    private MeshRenderer _MeshRenderer;
    private MaterialPropertyBlock _MaterialPropertyBlock;
    public void MoveHorizontal(float targetX)
    {
        transform.DOMoveX(targetX, 2f).SetEase(Ease.Linear).SetSpeedBased();
    }
    public void SetSize(float size)
    {
        transform.localScale = Vector3.one.SetX(size);
    }
    public void SetColor(Color color)
    {
        if (_MaterialPropertyBlock == null)
            _MaterialPropertyBlock = new MaterialPropertyBlock();
        _MaterialPropertyBlock.SetColor(Constants.BaseColor, color);
        _MeshRenderer.SetPropertyBlock(_MaterialPropertyBlock);
    }
    public class Pool : MemoryPool<Stack>
    { 
     
    }
}
