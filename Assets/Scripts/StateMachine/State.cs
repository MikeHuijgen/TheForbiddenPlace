using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface State
{
    public void Enter();
    public void Tick(){}
    public void FixedTick(){}
    public void Exit(){}
}
