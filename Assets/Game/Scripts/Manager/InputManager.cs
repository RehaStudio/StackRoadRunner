using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public event Action OnMouseButtonDowned;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseButtonDowned?.Invoke();
        }
    }
}
