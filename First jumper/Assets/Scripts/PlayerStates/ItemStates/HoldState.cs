using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HoldState : ItemState
{
    private Transform attackPoint;
    [SerializeField]
    private Transform shotFolder;
    private bool coroutineIsActive = false;

    private IEnumerator ShotDelay(float waitTimeInSec){
        coroutineIsActive = true;
        
        yield return new WaitForSeconds(waitTimeInSec);
        
        coroutineIsActive = false;
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

        // Debug.Log("Hold State");
        pc.currentItemState = this;

        if(attackPoint == null){
            attackPoint = pc.itemHolder.transform.GetChild(0);
        }

        if(shotFolder == null){
            shotFolder = GameObject.Find("ShotFolder").transform;
        }

        pc.itemHolder.GetComponent<Renderer>().material = pc.currentGun.Texture;
        pc.itemHolder.GetComponent<Renderer>().enabled = true;

        if(pc.currentGun.MuzzleFlashInstantiated == null){
            pc.currentGun.MuzzleFlashInstantiated = Instantiate(pc.currentGun.MuzzleFlash, attackPoint.position, pc.camera.transform.rotation, pc.itemHolder.transform);
        }
        
        if(pc.currentGun.ammuntionDisplay == null){
            pc.currentGun.ammuntionDisplay = GameObject.Find("AmmoDisplay").GetComponent<TextMeshProUGUI>();
        }
    }

    public override void Update()
    {
        base.Update();

        if(pc.currentGun != null && pc.currentGun is Gun){   // Or in the future pc.currentItem // Or rename all attack moves to same name...
            if(pc.currentGun.ReloadingIsTrue || coroutineIsActive){
                return;
            }

            if((pc.currentGun.CurrentAmmo <= 0f && pc.useIsPressed) 
            || (pc.rechargeIsPressed && pc.currentGun.CurrentAmmo != pc.currentGun.MaxAmmo)
            || (pc.secondaryUseIsPressed && pc.currentGun.BulletsPerBurstShot > pc.currentGun.CurrentAmmo )){

                pc.currentGun.Recharge();
                return;
            }

            if(pc.useIsPressed){
                StartCoroutine(ShotDelay(pc.currentGun.FireRate));
                pc.currentGun.Use(pc.camera, attackPoint, shotFolder);
                return;
            }
            
            if(pc.secondaryUseIsPressed && pc.currentGun.GunMode == Gun.Mode.Burst ){
                StartCoroutine(ShotDelay(pc.currentGun.SecondaryUseDelay));
                pc.currentGun.SecondaryUse(pc.camera, attackPoint, shotFolder);    
                return;        
            }
        }
    }

    public override void OnDisable()
    {
        base.OnDisable();

        pc.itemHolder.GetComponent<Renderer>().enabled = false;

        // This needs to be in 1 function
        pc.currentGun.StopRecharge();
        pc.currentGun.RemoveParticles();

        // Remove this later
        // pc.currentGun.ResetToStartingValues();
        // pc.currentGun = null;
        // pc.TestFunctionResetInventory();
    }
}