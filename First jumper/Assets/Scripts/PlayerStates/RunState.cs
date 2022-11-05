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
            new Transition(() => !pc.runningIsPressed, gameObject.GetComponent<DefaultState>())
        };
    }

    public void OnRun(InputValue movementValue)
    {
        pc.moveSpeed = pc.WalkingSpeed * pc.RunSpeed;
    }

    public override void OnDisable()
    {
        base.OnDisable();

        pc.moveSpeed = pc.WalkingSpeed;
    }
}
