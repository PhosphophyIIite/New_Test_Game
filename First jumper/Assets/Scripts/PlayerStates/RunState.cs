using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RunState : State
{
    public override void Start()
    {
        base.Start();

        ConditionDefault conditionDefault = new ConditionDefault(pc);

        transitions = new List<Transition>
        {
            new Transition(conditionDefault, gameObject.GetComponent<DefaultState>())
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

    public class ConditionDefault : Condition
    {
       public ConditionDefault(PlayerController pc) : base(pc)
       {
       }

       public override bool CheckCondition()
       {
            return (!pc.runningIsPressed);
       }
    }
}
