using Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIState : IAIState
{

    protected AIController controller;

    public AITransition transition { get; set; }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
    public virtual void SettingState(AIController controller)
    {

        this.controller = controller;
        transition = transition.GetComponentInChildren<AITransition>();

    }

}
