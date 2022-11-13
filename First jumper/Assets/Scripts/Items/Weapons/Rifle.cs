using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "New Rifle", menuName = "Rifle")]
public class Rifle : Gun
{
    public override void EnableItem(Transform attackPoint, Transform itemHolder, Transform shotFolder, Camera camera){
        itemHolder.GetComponent<Renderer>().material = Texture;
        itemHolder.GetComponent<Renderer>().enabled = true;

        if(MuzzleFlashInstantiated == null){
            MuzzleFlashInstantiated = Instantiate(MuzzleFlash, attackPoint.position, camera.transform.rotation, itemHolder.transform);
        }
        
        if(ammuntionDisplay == null){
            ammuntionDisplay = GameObject.Find("AmmoDisplay").GetComponent<TextMeshProUGUI>();
        }
    }
    
    public override void Use(bool useIsPressed, bool rechargeIsPressed, bool secondaryUseIsPressed, Camera camera, Transform attackPoint, Transform shotFolder){
        if(ReloadingIsTrue || shotDelayRoutineIsActive){
            return;
        }

        if((CurrentAmmo <= 0f && useIsPressed) 
        || (rechargeIsPressed && CurrentAmmo != MaxAmmo)
        || (secondaryUseIsPressed && BulletsPerBurstShot > CurrentAmmo )){

            Recharge();
            return;
        }

        if(useIsPressed){
            CoroutineCaller.instance.StartCoroutine(ShotDelay(FireRate));
            Shoot(camera, attackPoint, shotFolder);
            return;
        }
        
        // Maybe move this to new function
        if(secondaryUseIsPressed && GunMode == Mode.Burst){
            CoroutineCaller.instance.StartCoroutine(ShotDelay(SecondaryUseDelay));
            Burst(camera, attackPoint, shotFolder);    
            return;        
        }
    }

    public override void DisableItemHandler()
    {
        StopRecharge();
        RemoveParticles();
    }
}