using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteract<T>
{
    public void Interact(T interactedObject);
}
