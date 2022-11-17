using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Rifle", menuName = "Rifle")]
public class Rifle : Gun
{
    public override void EnableItem(Transform attackPoint, Transform itemHolder, Transform shotFolder, Camera camera, GameObject crossHair){
        ItemModelInstantiated = Instantiate(ItemModel, itemHolder.transform);

        crossHair.SetActive(true);

        if(MuzzleFlashInstantiated == null){
            MuzzleFlashInstantiated = Instantiate(MuzzleFlash, attackPoint.position, camera.transform.rotation, itemHolder.transform);
        }

        if(CrossHair == null){
            CrossHair = crossHair.GetComponent<Image>();
        }
        
        if(AmmuntionDisplay == null){
            AmmuntionDisplay = GameObject.Find("AmmoDisplay").GetComponent<TextMeshProUGUI>();
        }

        if(animator == null){
            animator = itemHolder.GetComponent<Animator>();
        }
    }
    
    public override void Use(bool useIsPressed, bool rechargeIsPressed, bool secondaryUseIsPressed, Camera camera, Transform attackPoint, Transform shotFolder){
        if(ReloadingIsTrue || ShotDelayRoutineIsActive){
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
        if(secondaryUseIsPressed){
            if(GunMode == Mode.Burst){
                CoroutineCaller.instance.StartCoroutine(ShotDelay(SecondaryUseDelay));
                Burst(camera, attackPoint, shotFolder);  
                return;
            }

            if(GunMode == Mode.Zoom){
                if(!ShotDelayRoutineIsActive){ // Give unique boolean to fix shooting bug when zooming
                    if(!ZoomIsTrue){
                        ZoomIsTrue = true;
                        Zoom();
                        CoroutineCaller.instance.StartCoroutine(ShotDelay(SecondaryUseDelay));
                        return;
                    }
                     
                    if(ZoomIsTrue){
                        ZoomIsTrue = false;
                        Zoom();
                        CoroutineCaller.instance.StartCoroutine(ShotDelay(SecondaryUseDelay));
                        return;
                    }
                }
                return;
            }
            
            if(GunMode == Mode.Block){
                Debug.Log("Block");
                Block();
                return;
            }
  
            return;        
        }

        // Camera camera, float targetZoom, float zoomDuration

        SetGUIColor(Color.black, CrossHair);
    }

    public override void DisableItem(GameObject crossHair)
    {
        StopRecharge();
        RemoveParticles();
        RemoveGameModels();
        DisableUI(crossHair);
    }
}