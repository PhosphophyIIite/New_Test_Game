using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RunState : State
{
    public override void Start()
    {
        base.Start();

        transitions = new List<Transition>
        {
            new Transition(() => !pc.runningIsPressed, pc.m_DefaultState, "")
        };
    }

    public override void OnEnable()
    {
        base.OnEnable();

        // Debug.Log("Run State");
        pc.currentState = this;
    }

    public void OnRun(InputValue movementValue)
    {
        Debug.Log("Run State");
        
        pc.moveSpeed = pc.WalkingSpeed * pc.RunSpeed;
    }

    public override void OnDisable()
    {
        base.OnDisable();

        pc.moveSpeed = pc.WalkingSpeed;
    }
}
