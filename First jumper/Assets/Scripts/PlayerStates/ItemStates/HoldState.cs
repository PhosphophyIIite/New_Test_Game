using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoldState : ItemState
{
    [SerializeField]
    private Transform shotFolder;
    [SerializeField]
    private GameObject crossHair;
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

        if(crossHair == null){
            crossHair = GameObject.Find("CrossHair");
        }

        if(shotFolder == null){
            shotFolder = GameObject.Find("ShotFolder").transform;
        }

        pc.currentItem.EnableItem(attackPoint, pc.itemHolder.transform, shotFolder, pc.camera, crossHair);
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

        pc.currentItem.DisableItem(crossHair);
    }
}