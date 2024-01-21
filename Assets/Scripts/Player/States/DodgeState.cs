using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeState : MonoBehaviour, State
{
    private StateMachine _stateMachine;
    
    public void Enter(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void Tick()
    {

    }
}
