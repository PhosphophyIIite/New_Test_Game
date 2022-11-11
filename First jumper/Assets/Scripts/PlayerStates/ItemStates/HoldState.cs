using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldState : ItemState
{
    private Transform attackPoint;
    [SerializeField]
    private Transform shotFolder;
    private bool coroutineIsNotActive = true;

    private IEnumerator ShotDelay(){
        coroutineIsNotActive = false;
        
        yield return new WaitForSeconds(pc.currentGun.FireRate);
        
        coroutineIsNotActive = true;
    }

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

        if(attackPoint == null){
            attackPoint = pc.itemHolder.transform.GetChild(0);
        }

        if(shotFolder == null){
            shotFolder = GameObject.Find("ShotFolder").transform;
        }

        pc.itemHolder.GetComponent<Renderer>().material = pc.currentGun.Texture;
        pc.itemHolder.GetComponent<Renderer>().enabled = true;
    }

    public override void Update()
    {
        base.Update();

        // if(pc.currentGun is Gun){ }  // or in the future pc.currentItem 
        // Or rename all attack moves to same name...
        if(pc.currentGun.ReloadingIsTrue){
            return;
        }

        if(pc.currentGun.CurrentAmmo <= 0f){
            pc.currentGun.Recharge();
            return;
        }

        if(pc.useIsPressed && coroutineIsNotActive){
            StartCoroutine(ShotDelay());
            pc.currentGun.Use(pc.camera, attackPoint, shotFolder);
        }

        if(pc.secondaryUseIsPressed){
            pc.currentGun.SecondaryUse();            
        }
    }

    public override void OnDisable()
    {
        base.OnDisable();

        pc.itemHolder.GetComponent<Renderer>().enabled = false;

        // Remove this later
        pc.currentGun.ResetToStartingValues();
    }
}