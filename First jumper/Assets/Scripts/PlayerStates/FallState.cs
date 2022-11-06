using System.Collections.Generic;

public class FallState : State
{
    public override void Start()
    {
        base.Start();

        transitions = new List<Transition>
        {
            new Transition(() => pc.grounded && pc.rb.velocity.y <= 0 && pc.groundedPredict, pc.m_JumpState, "Fall => Jump(predict)"),
            new Transition(() => pc.grounded && pc.rb.velocity.y >= -0.1f, pc.m_DefaultState, "Fall => Default")
        };
    }

    public override void OnEnable()
    {
        base.OnEnable();

        // Debug.Log("FallState");
        pc.currentState = this;
    }
}