using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DefaultState : State
{
    // set condition for RunState
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

    public override void Start()
    {
        base.Start();

        pc.initialState = this;

        ConditionRun conditionRun = new ConditionRun(pc);

        transitions = new List<Transition>
        {
            new Transition(conditionRun, gameObject.GetComponent<RunState>())
        };
    }
}
