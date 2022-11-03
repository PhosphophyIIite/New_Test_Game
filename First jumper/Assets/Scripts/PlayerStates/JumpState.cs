using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : State
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

    public override void OnEnable()
    {   
        base.OnEnable();

        Debug.Log("JumpState");
        pc.rb.AddForce(0.0f, pc.jumpForce, 0.0f);
    }

    public override void OnDisable()
    {
        base.OnDisable();

        pc.grounded = false;
    }

    public class ConditionDefault : Condition
    {
       public ConditionDefault(PlayerController pc) : base(pc)
       {
       }

       public override bool CheckCondition()
       {
            return (pc.grounded); // && no velocity
       }
    }
}
