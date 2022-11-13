using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Rifle", menuName = "Rifle")]
public class Rifle : Gun
{
    public override void EnableItem(Transform attackPoint, Transform itemHolder, Transform shotFolder, Camera camera, GameObject crossHair){
        itemHolder.GetComponent<Renderer>().material = Texture;
        itemHolder.GetComponent<Renderer>().enabled = true;

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
                Debug.Log("Zoom");
                return;
            }
            
            if(GunMode == Mode.Block){
                Debug.Log("Block");
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
        DisableUI(crossHair);
    }
}