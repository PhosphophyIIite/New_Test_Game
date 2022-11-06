using System.Collections.Generic;

public class HoldState : ItemState
{
    public override void Start()
    {
        base.Start();

        // movementTransitions = new List<Transition>
        // {
        //     new Transition(() => , pc.m_HoldState, "Hold => DefaultItem")
        // };
    }

    public override void OnEnable()
    {
        base.OnEnable();

        // Debug.Log("Default State");
        pc.currentItemState = this;
    }
}