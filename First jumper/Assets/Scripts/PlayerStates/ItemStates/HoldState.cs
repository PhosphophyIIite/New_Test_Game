using System.Collections.Generic;
using UnityEngine;

public class HoldState : ItemState
{
    [SerializeField]
    private Transform shotFolder;
    private Transform attackPoint;

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

        // Debug.Log("Hold State");
        pc.currentItemState = this;

        if(attackPoint == null){
            attackPoint = pc.itemHolder.transform.GetChild(0);
        }

        if(shotFolder == null){
            shotFolder = GameObject.Find("ShotFolder").transform;
        }

        pc.currentItem.EnableItem(attackPoint, pc.itemHolder.transform, shotFolder, pc.camera);
    }

    public override void Update()
    {
        base.Update();

        pc.currentItem.Use(pc.useIsPressed, pc.rechargeIsPressed, pc.secondaryUseIsPressed, pc.camera, attackPoint, shotFolder);
    }

    public override void OnDisable()
    {
        base.OnDisable();

        pc.itemHolder.GetComponent<Renderer>().enabled = false;

        pc.currentItem.DisableItemHandler();

        // Remove this later
        // pc.currentGun.ResetToStartingValues();
        // pc.currentGun = null;
        // pc.TestFunctionResetInventory();
    }
}