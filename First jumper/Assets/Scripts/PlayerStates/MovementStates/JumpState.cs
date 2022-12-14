using System.Collections.Generic;
using UnityEngine;

public class JumpState : MovementState
{
    public override void Start()
    {
        base.Start();

        movementTransitions = new List<Transition>
        {
            new Transition(() => pc.grounded && pc.rb.velocity.y <= 0 && pc.groundedPredict, pc.m_InBetweenState, "Jump(predict) => InBetween(Jump)"),
            new Transition(() => !pc.grounded && pc.rb.velocity.y <= 0, pc.m_FallState, "Jump => Fall")
            // new Transition(() => pc.grounded && pc.rb.velocity.y == 0 && !pc.jumpIsPressed !pc.groundPredict, pc.m_DefaultState, "Jumping => Default"),
            // new Transition(() => pc.grounded && pc.rb.velocity.y <= 0 && pc.jumpIsPressed, pc.m_InBetweenState, "Jumping => Jumping") // should never hit this condition
        };
    }

    public override void OnEnable()
    {
        base.OnEnable();

        // Debug.Log("Jump State");
        pc.currentMovementState = this;

        pc.jumpIsPressed = false;
        pc.groundedPredict = false;

        // Set vertical velocity to 0
        pc.rb.velocity = new Vector3(pc.rb.velocity.x, 0.0f, pc.rb.velocity.z);

        // Add jump force
        pc.rb.AddForce(0.0f, pc.jumpForce, 0.0f);
    }

    public override void OnDisable()
    {
        base.OnDisable();

        pc.groundedPredict = false;
        pc.jumpIsPressed = false;
    }
}