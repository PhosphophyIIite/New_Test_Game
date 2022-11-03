using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DefaultState : State
{
    public override void Start()
    {
        base.Start();

        pc.initialState = this;

        ConditionRun conditionRun = new ConditionRun(pc);
        ConditionJump conditionJump = new ConditionJump(pc);

        transitions = new List<Transition>
        {
            new Transition(conditionRun, gameObject.GetComponent<RunState>()),
            new Transition(conditionJump, gameObject.GetComponent<JumpState>())
        };
    }

    public class ConditionRun : Condition
    {
       public ConditionRun(PlayerController pc) : base(pc)
       {
       }

       public override bool CheckCondition()
       {
            return (pc.runningIsPressed);
           //return (Vector3.Distance(npcCustomController.npc.transform.position, npcCustomController.npcRecoverPosition) <= 130f && npcCustomController.health >= 75);
       }
    }

    public class ConditionJump : Condition
    {
       public ConditionJump(PlayerController pc) : base(pc)
       {
       }

       public override bool CheckCondition()
       {
            return (pc.grounded && pc.jumpIsPressed);
           //return (Vector3.Distance(npcCustomController.npc.transform.position, npcCustomController.npcRecoverPosition) <= 130f && npcCustomController.health >= 75);
       }
    }
}
