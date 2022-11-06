using System.Collections.Generic;
using UnityEngine;

public class InBetweenState : State
{
    public override void Start()
    {
        base.Start();

        transitions = new List<Transition>
        {
            new Transition(() => true, pc.currentState, pc.currentState + " => " + pc.currentState)
        };
    }

    public override void OnEnable()
    {
        base.OnEnable();

        // Debug.Log("InBetween State");
        pc.currentState = this;
    }
}