using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultState : State
{
    public override void Awake()
    {
        //base.Awake();

        //states = new List<State>
        //{
        //    gameObject.GetComponent<FleeState>(),
        //    gameObject.GetComponent<ExtinguishState>(),
        //    gameObject.GetComponent<ReturnState>()
        //};

        // stateInitial = gameObject.GetComponent<FleeState>();
    }

    //public class ConditionSavedState : Condition
    //{
    //    public ConditionSavedState(NPCcustomController npcCustomController) : base(npcCustomController)
    //    {
    //    }

    //    public override bool Test()
    //    {
    //        return (Vector3.Distance(npcCustomController.npc.transform.position, npcCustomController.npcRecoverPosition) <= 130f && npcCustomController.health >= 75);
    //    }
    //}

    public override void Start()
    {
        // base.Start();

        // ConditionSavedState conditionSavedState = new ConditionSavedState(npcCustomController);


        transitions = new List<NewTransition>
        {
            // new NewTransition(conditionSavedState, npcCustomController.npcRecoverState)
        };

        // npcCustomController.npcStateAndHealth.text = stateInitial.GetType().Name + " / " + npcCustomController.health.ToString() + "%";
    }
}
