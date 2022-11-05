using System.Collections.Generic;
using UnityEngine;

public class FallState : State
{
    public override void Start()
    {
        base.Start();

        transitions = new List<Transition>
        {
            new Transition(() => pc.grounded && pc.rb.velocity.y <= 0 && pc.groundedPredict, pc.m_JumpState, "Falling => Jumping(predict)"),
            new Transition(() => pc.grounded, pc.m_DefaultState, "Fall => Default")
        };
    }

    public override void OnEnable()
    {
        base.OnEnable();

        // Debug.Log("FallState");
        pc.currentState = this;
    }
}