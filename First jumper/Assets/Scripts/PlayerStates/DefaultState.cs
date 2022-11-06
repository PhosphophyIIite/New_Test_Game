using System.Collections.Generic;

public class DefaultState : State
{
    public override void Start()
    {
        base.Start();

        pc.initialState = this; // does currently nothing

        transitions = new List<Transition>
        {
            new Transition(() => pc.runningIsPressed, pc.m_RunState, "Default => Run"),
            new Transition(() => pc.grounded && pc.jumpIsPressed, pc.m_JumpState, "Default => Jumping")
        };
    }

    public override void OnEnable()
    {
        base.OnEnable();

        // Debug.Log("Default State");
        pc.currentState = this;
    }
}