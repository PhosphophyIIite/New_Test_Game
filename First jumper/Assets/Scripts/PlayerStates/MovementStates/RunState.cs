using System.Collections.Generic;

public class RunState : MovementState
{
    public override void Start()
    {
        base.Start();

        movementTransitions = new List<Transition>
        {
            new Transition(() => pc.rb.velocity.y <= -0.1f, pc.m_FallState, "Run => Fall"),
            new Transition(() => !pc.runningIsPressed, pc.m_DefaultMovementState, "Run => DefaultItem"),
            new Transition(() => pc.grounded && pc.jumpIsPressed, pc.m_JumpState, "Run => Jumping")
        };
    }

    public override void OnEnable()
    {
        base.OnEnable();

        // Debug.Log("Run State");
        pc.currentMovementState = this;

        pc.moveSpeed = pc.WalkingSpeed * pc.RunSpeed;
    }

    public override void OnDisable()
    {
        base.OnDisable();

        pc.moveSpeed = pc.WalkingSpeed;
    }
}