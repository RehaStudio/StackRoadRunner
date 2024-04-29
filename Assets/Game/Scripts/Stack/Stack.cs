using DG.Tweening;
using System.Net.NetworkInformation;
using UnityEngine;
using Zenject;

public class Stack : MonoBehaviour
{
    #region Fields
    private StackManager _StackManager;
    private MaterialPropertyBlock _MaterialPropertyBlock;
    private Tween _HorizontalMoveTween;
    #endregion
    #region Properties
    public MeshRenderer MeshRenderer { get; set; }
    #endregion
    [Inject]
    private void Constructor(StackManager stackManager)
    {
        _StackManager = stackManager;
    }
    public void MoveHorizontal(float targetX)
    {
        _HorizontalMoveTween = transform.DOMoveX(targetX, 4f).SetEase(Ease.Linear).SetSpeedBased();
        _HorizontalMoveTween.OnComplete(() =>
        {
            _StackManager.CanStackPlace();
        });
    }
    public void StopMove()
    {
        _HorizontalMoveTween.Kill();
    }
    public void SetLocalPosition(Vector3 localPosition)
    {
        transform.localPosition = localPosition;
    }
    public Vector3 GetLocalPosition()
    {
        return transform.localPosition;
    }
    public void SetSize(float size)
    {
        transform.localScale = new Vector3(size, 1f, Constants.StackStepSize);
    }
    public float GetSize()
    {
        return transform.localScale.x;
    }
    public void SetColor(Color color)
    {
        if (_MaterialPropertyBlock == null)
            _MaterialPropertyBlock = new MaterialPropertyBlock();
        _MaterialPropertyBlock.SetColor(Constants.Color, color);
        MeshRenderer.SetPropertyBlock(_MaterialPropertyBlock);
    }
    public Color GetColor()
    {
        return _MaterialPropertyBlock.GetColor(Constants.Color);
    }
    public class Pool : MemoryPool<Stack>
    {
        protected override void OnCreated(Stack stack)
        {
            stack.MeshRenderer = stack.GetComponent<MeshRenderer>();
            base.OnCreated(stack);
        }
        protected override void OnDespawned(Stack stack)
        {
            stack.gameObject.SetActive(false);   
            base.OnDespawned(stack);
        }
        protected override void OnSpawned(Stack stack)
        {
            stack.gameObject.SetActive(true);
            base.OnSpawned(stack);
        }
    }
}
