using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPunchState : State
{
    private CharacterPunchStateData stateData;
    public CharacterPunchState(Character character, CharacterPunchStateData stateData) : base(character)
    {
        this.stateData = stateData;
    }

    public override void AnimationFinish()
    {
        base.AnimationFinish();

        character.stateMachine.ChangeState(character.characterIdleState);
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        character.SetVelocity(Vector3.zero);
        character.SetTriggerAnimator(stateData.animationString);
        character.SetRotate(stateData.rotationOffset);
    }

    public override void Exit()
    {
        base.Exit();
        character.SetRotate(Vector3.zero);

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
