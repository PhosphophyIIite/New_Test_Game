using System.Collections.Generic;
using UnityEngine;

public class JumpState : State
{
    public override void Start()
    {
        base.Start();

        Debug.Log("JumpState");

        transitions = new List<Transition>
        {
            new Transition(() => pc.grounded && pc.testKey, gameObject.GetComponent<DefaultState>()) // && no velocity
        };
    }

    public override void OnEnable()
    {
        base.OnEnable();

        pc.rb.AddForce(0.0f, pc.jumpForce, 0.0f);
    }
}
