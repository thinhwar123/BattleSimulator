using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIdleState : State
{
    private CharacterIdleStateData stateData;
    public CharacterIdleState(Character character, CharacterIdleStateData stateData) : base(character)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        character.SetVelocity(Vector3.zero);
        character.SetRotate(stateData.rotationOffset);
        character.characerIK.StopMove();
    }

    public override void Exit()
    {
        base.Exit();
        character.SetRotate(Vector3.zero);
        character.characerIK.StopBalance();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Vector3 dir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (dir!= Vector3.zero && character.canMove)
        {
            character.stateMachine.ChangeState(character.characterMoveState);
        }
        //if (Input.GetKeyDown(KeyCode.C) && character.canMove)
        //{
        //    character.stateMachine.ChangeState(character.characterPunchState);
        //}
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        character.characerIK.Balance();
        //TODO: xem lai logic
        //character.SetVelocity(Vector3.zero);
    }
}
