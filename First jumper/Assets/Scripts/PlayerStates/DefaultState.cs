using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DefaultState : State
{
    public override void Start()
    {
        base.Start();

        pc.initialState = this; // does currently nothing

        transitions = new List<Transition>
        {
            new Transition(() => pc.runningIsPressed, gameObject.GetComponent<RunState>()),
            new Transition(() => pc.grounded && pc.jumpIsPressed, gameObject.GetComponent<JumpState>())
        };
    }
}
