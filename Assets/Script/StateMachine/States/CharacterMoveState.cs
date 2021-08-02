using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveState : State
{
    private CharacterMoveStateData stateData;
    private CharacterMotion currentMotion;
    private Vector3 dir;
    public CharacterMoveState(Character character, CharacterMoveStateData stateData) : base(character)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        character.SetBoolAnimator(stateData.animationString, true);
        character.SetRotate(stateData.rotationOffset);
        currentMotion = CharacterMotion.STOPMOVE;
    }

    public override void Exit()
    {
        base.Exit();
        character.SetBoolAnimator(stateData.animationString, false);
        character.SetRotate(Vector3.zero);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        dir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (dir == Vector3.zero)
        {
            character.stateMachine.ChangeState(character.characterIdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (dir != Vector3.zero)
        {
            if (dir.z < 0 && currentMotion != CharacterMotion.MOVEBACKWARD)
            {
                currentMotion = CharacterMotion.MOVEBACKWARD;
                character.characerIK.MoveBackward();
            }
            else if (dir.z > 0 && currentMotion != CharacterMotion.MOVEFORWARD)
            {
                currentMotion = CharacterMotion.MOVEFORWARD;
                character.characerIK.MoveForward();
            }
            //character.SetVelocity(dir, stateData.movementSpeed);
        }
    }
}
