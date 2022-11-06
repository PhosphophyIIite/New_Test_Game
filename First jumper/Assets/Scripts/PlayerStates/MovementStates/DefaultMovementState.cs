using System.Collections.Generic;

public class DefaultMovementState : MovementState
{
    public override void Start()
    {
        base.Start();

        pc.initialMovementState = this; // does currently nothing

        movementTransitions = new List<Transition>
        {
            new Transition(() => pc.runningIsPressed, pc.m_RunState, "DefaultItem => Run"),
            new Transition(() => pc.grounded && pc.jumpIsPressed, pc.m_JumpState, "DefaultItem => Jumping")
        };
    }

    public override void OnEnable()
    {
        base.OnEnable();

        // Debug.Log("Default State");
        pc.currentMovementState = this;
    }
}