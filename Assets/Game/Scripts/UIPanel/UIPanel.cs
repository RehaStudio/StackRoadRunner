using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class UIPanel : MonoBehaviour
{
    #region Fields
    [SerializeField] private CanvasGroup _CanvasGroup;
    #endregion
    public virtual void Initialize()
    {
        
    }
    public virtual void Show()
    {
        gameObject.SetActive(true);
        _CanvasGroup.DOFade(1f, .2f).From(0f);
    }
    public virtual void Hide() 
    {
        gameObject.SetActive(false);
        _CanvasGroup.DOFade(0f, .2f).From(1f);
    }
}
