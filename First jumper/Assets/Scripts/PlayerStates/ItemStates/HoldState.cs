using System.Collections.Generic;
using UnityEngine;

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

        Debug.Log(pc.currentGun.GetObject());

        pc.itemHolder.GetComponent<Renderer>().material = pc.currentGun.texture;
        pc.itemHolder.GetComponent<Renderer>().enabled = true;
    }

    public override void Update()
    {
        base.Update();

        if(pc.attackIsPressed)
            pc.currentGun.Shoot();

        if(pc.secondaryAttackIsPressed)
            pc.currentGun.SecondarySkill();
    }

    public override void OnDisable()
    {
        base.OnDisable();

        pc.itemHolder.GetComponent<Renderer>().enabled = false;
    }
}