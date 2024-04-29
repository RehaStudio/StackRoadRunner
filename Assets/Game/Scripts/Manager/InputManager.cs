using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    #region Events
    public event Action OnMouseButtonDowned;
    #endregion
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseButtonDowned?.Invoke();
        }
    }
}
