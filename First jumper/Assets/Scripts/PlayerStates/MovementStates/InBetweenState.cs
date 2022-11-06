using System.Collections.Generic;

public class InBetweenState : MovementState
{
    public override void Start()
    {
        base.Start();

        movementTransitions = new List<Transition>
        {
            new Transition(() => true, pc.currentMovementState, pc.currentMovementState + " => " + pc.currentMovementState)
        };
    }

    public override void OnEnable()
    {
        base.OnEnable();

        // Debug.Log("InBetween State");
        pc.currentMovementState = this;
    }
}