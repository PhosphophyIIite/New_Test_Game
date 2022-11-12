using System.Collections.Generic;

public class DefaultItemState : ItemState
{
    public override void Start()
    {
        base.Start();

        // Currently does nothing
        pc.initialItemState = this;

        itemTransitions = new List<Transition>
        {
            new Transition(() => pc.testKey, pc.m_HoldState, "DefaultItem => Hold")
        };
    }

    public override void OnEnable()
    {
        base.OnEnable();

        // Debug.Log("DefaultItem State");
        pc.currentItemState = this;
    }
}