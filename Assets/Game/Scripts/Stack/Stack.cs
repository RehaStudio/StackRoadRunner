using DG.Tweening;
using UnityEngine;
using Zenject;

public class Stack : MonoBehaviour
{
    private MaterialPropertyBlock _MaterialPropertyBlock;
    public MeshRenderer MeshRenderer { get; set; }
    public void MoveHorizontal(float targetX)
    {
        transform.DOMoveX(targetX, 2f).SetEase(Ease.Linear).SetSpeedBased();
    }
    public void SetLocalPosition(Vector3 localPosition)
    {
        transform.localPosition = localPosition;
    }
    public void SetSize(float size)
    {
        transform.localScale = Vector3.one.SetX(size);
    }
    public void SetColor(Color color)
    {
        if (_MaterialPropertyBlock == null)
            _MaterialPropertyBlock = new MaterialPropertyBlock();
        _MaterialPropertyBlock.SetColor(Constants.Color, color);
        MeshRenderer.SetPropertyBlock(_MaterialPropertyBlock);
    }
    public class Pool : MemoryPool<Stack>
    {
        protected override void OnCreated(Stack stack)
        {
            stack.MeshRenderer = stack.GetComponent<MeshRenderer>();
            base.OnCreated(stack);
        }
    }
}
