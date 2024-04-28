using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : BaseStateMachine<Player>
{
    public PlayerStateMachine(Player currentObject) : base(currentObject)
    {
        _States.Add(new PlayerRunState(currentObject,this));
        _States.Add(new PlayerDanceState(currentObject,this));
    }
}
