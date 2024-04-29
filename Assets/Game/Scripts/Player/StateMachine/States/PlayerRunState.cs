using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : BaseState<Player>
{
    public PlayerRunState(Player currentObject, BaseStateMachine<Player> baseStateMachine) : base(currentObject, baseStateMachine)
    {

    }

    public override void Enter()
    {
        CurrentObject._Animator.SetTrigger(PlayerStateMachineKeys.Run);
    }

    public override void Execute()
    {

    }

    public override void Exit()
    {

    }

    public override void OnEvent()
    {

    }
}
