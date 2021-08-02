using UnityEngine;

public class State
{
    protected bool isExitingState;

    protected Character character;

    protected bool isAnimationFinish;
    protected float startTime;

    public State (Character character)
    {
        this.character = character;
    }
    public virtual void Enter()
    {
        DoChecks();
        startTime = Time.time;

        isAnimationFinish = false;
        isExitingState = false;
    }
    public virtual void Exit()
    {
        isExitingState = true;
        character.ResetAnimatorTranform();
    }
    public virtual void LogicUpdate()
    {

    }
    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }
    public virtual void DoChecks()
    {

    }
    public virtual void AnimationTrigger()
    {

    }
    public virtual void AnimationFinish()
    {
        isAnimationFinish = true;
    }
}
