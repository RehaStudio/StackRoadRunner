using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackPool
{
    private Stack.Pool _Pool;
    public StackPool(Stack.Pool pool)
    {
        _Pool = pool;
    }
    public Stack Spawn()
    {
        return _Pool.Spawn();
    }
    public void Despawn(Stack stack)
    {
        _Pool.Despawn(stack);
    }
}
