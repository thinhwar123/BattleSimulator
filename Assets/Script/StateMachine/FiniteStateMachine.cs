using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine
{
    public State currentState;

    public void Initialize(State startState)
    {
        currentState = startState;
        currentState.Enter();
    }
    public void ChangeState(State newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
}
