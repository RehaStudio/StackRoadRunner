using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDanceState : BaseState<Player>
{
    public PlayerDanceState(Player currentObject, BaseStateMachine<Player> baseStateMachine) : base(currentObject, baseStateMachine)
    {

    }

    public override void Enter()
    {
        CurrentObject._Animator.SetTrigger(PlayerStateMachineKeys.Dance);
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
