using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
public class Gun : Rifle
{
    public enum Mode
    {
        Zoom, // default
        Burst,
        Block
    }

    [SerializeField]
    private int maxAmmo = 0;
    public int MaxAmmo{
        // set { maxAmmo = value; }
        get { return maxAmmo; }
    }

    [SerializeField]
    private int currentAmmo = 0;
    public int CurrentAmmo{
        set { currentAmmo = value; }
        get { return currentAmmo; }
    }

    [SerializeField]
    private float damage = 0f;
    public float Damage{
        // set { damage = value; }
        get { return damage; }
    }

    [Rename("Shot delay in Seconds")]
    [SerializeField]
    private float fireRate = 0f;
    public float FireRate{
        // set { fireRate = value; }
        get { return fireRate; }
    }

    [SerializeField]
    private bool reloadingIsTrue = false;
    public bool ReloadingIsTrue{
        set { reloadingIsTrue = value; }
        get { return reloadingIsTrue; }
    }

    [SerializeField]
    private float bulletsPerTap = 1f;
    public float BulletsPerTap{
        // set { bulletsPerTap = value; }
        get { return bulletsPerTap; }
    }

    [SerializeField]
    private float bulletsPerBurstShot = 1f;
    public float BulletsPerBurstShot{
        // set { bulletsPerBurstShot = value; }
        get { return bulletsPerBurstShot; }
    }
    
    [SerializeField]
    private float spread = 0f;
    public float Spread{
        // set { spread = value; }
        get { return spread; }
    }

    [SerializeField]
    private Mode mode;
    public Mode GunMode{
        // set { mode = value; }
        get { return mode; }
    }

    [SerializeField]
    private GameObject muzzleFlash;
    public GameObject MuzzleFlash{
        get { return muzzleFlash; }
    }

    private GameObject muzzleFlashInstantiated;
    public GameObject MuzzleFlashInstantiated{
        set { muzzleFlashInstantiated = value; }
        get { return muzzleFlashInstantiated; }
    }

    [SerializeField]
    private RifleBullet bullet;
    private IEnumerator reloadRoutine;
    public TextMeshProUGUI ammuntionDisplay;

    private IEnumerator ReloadRoutine(){
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(ReloadTime);

        CurrentAmmo = MaxAmmo;
        if(ammuntionDisplay != null){
            UpdateGUI();
        }

        reloadingIsTrue = false;
    }

    public override void Use(Camera camera, Transform attackPoint, Transform bulletFolder){
        // Debug.Log("Do some shooting with " + name);

        if(CurrentAmmo <= 0f || reloadingIsTrue){
            return;
        }
        
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        Vector3 targetPoint = ray.GetPoint(5f);
        
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(Random.Range(-Spread, Spread), Random.Range(-Spread, Spread), Random.Range(-Spread, Spread));

        GameObject newBullet = Instantiate(bullet.bullet, attackPoint.position, Quaternion.identity, bulletFolder.transform);
        newBullet.transform.forward = directionWithSpread.normalized;

        newBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * bullet.bulletSpeed, ForceMode.Impulse);
        // newBullet.GetComponent<Rigidbody>().AddForce(camera.transform.up * upwardForce, ForceMode.Impulse); // If you have bouncing bullets

        MuzzleFlashInstantiated.GetComponent<ParticleSystem>().Play();

        CurrentAmmo--;

        if(ammuntionDisplay != null){
            UpdateGUI();
        }
    }

    private void UpdateGUI(){
        ammuntionDisplay.SetText(CurrentAmmo / BulletsPerTap + " / " + MaxAmmo / BulletsPerTap);
    }

    // Burst only
    public override void SecondaryUse(Camera camera, Transform attackPoint, Transform bulletFolder){
        if(mode == Mode.Burst){   	  
            if(CurrentAmmo >= BulletsPerBurstShot){
                for(int i = 0; i < BulletsPerBurstShot; i++){
                    Use(camera, attackPoint, bulletFolder);
                }
            }
        }
    }

    public override void SecondaryUse()
    {
        if(mode == Mode.Zoom){
            // Debug.Log("Zoom");
        }

        if(mode == Mode.Block){
            // Debug.Log("Block");
        }
    }

    public override void Recharge(){
        // Debug.Log("Do some reloading");

        ReloadingIsTrue = true;

        reloadRoutine = ReloadRoutine();
        CoroutineCaller.instance.StartCoroutine(reloadRoutine);
    }

    // Maybe add to reset to starting values later
    public override void StopRecharge(){
        ReloadingIsTrue = false;

        if(reloadRoutine == null){
            return;
        }

        CoroutineCaller.instance.StopCoroutine(reloadRoutine);
    }

    public void RemoveParticles(){
        Destroy(MuzzleFlashInstantiated);
    }

    public override string GetObject(){
        // Debug.Log("Name: " + name + ", Ammo: " + ammo + ", Texture: " + texture + ", Firerate: " + fireRate + ", Controls: " + controls);

        // Keep up-to-date
        return "Name: " + name + ", Texture: " + Texture + ", Controls: " + Controls + ", Reloadtime(S): " + ReloadTime + ", Max Ammo: " + MaxAmmo + 
        ", Current Ammo: " + CurrentAmmo + ", Damage: " + Damage + " Shot Delay(S): " + FireRate + ", Bullets per BurstShot: " + BulletsPerBurstShot + 
        ", SecondaryUse Delay: " + SecondaryUseDelay + ", ReloadIsTrue: " + ReloadingIsTrue + ", Spread: " + Spread + ", Mode: " + GunMode;
    }
}