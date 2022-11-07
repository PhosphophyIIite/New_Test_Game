using System.Collections.Generic;

public class HoldState : ItemState
{
    public override void Start()
    {
        base.Start();

        itemTransitions = new List<Transition>
        {
            new Transition(() => pc.testKey2, pc.m_DefaultItemState, "Hold => DefaultItem")
        };
    }

    public override void OnEnable()
    {
        base.OnEnable();

        // Debug.Log("Default State");
        pc.currentItemState = this;
    }
}