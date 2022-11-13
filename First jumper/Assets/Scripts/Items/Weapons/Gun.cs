using System.Collections;
using UnityEngine;
using TMPro;

public class Gun : IItem
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
            UpdateGUI(ammuntionDisplay, CurrentAmmo / BulletsPerTap + " / " + MaxAmmo / BulletsPerTap);
        }

        reloadingIsTrue = false;
    }

    protected bool shotDelayRoutineIsActive = false;
    protected IEnumerator ShotDelay(float waitTimeInSec){
        shotDelayRoutineIsActive = true;
        
        yield return new WaitForSeconds(waitTimeInSec);
        
        shotDelayRoutineIsActive = false;
    }

    public void Shoot(Camera camera, Transform attackPoint, Transform bulletFolder){
        // If checked in child class should never be called.
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
        // newBullet.GetComponent<Rigidbody>().AddForce(camera.transform.up * upwardForce, ForceMode.Impulse); // If you have bouncing bullets // Add this to Shoot function with upforce as parameter

        MuzzleFlashInstantiated.GetComponent<ParticleSystem>().Play();

        CurrentAmmo--;

        if(ammuntionDisplay != null){
            UpdateGUI(ammuntionDisplay, CurrentAmmo / BulletsPerTap + " / " + MaxAmmo / BulletsPerTap);
        }
    }

    private void UpdateGUI(TextMeshProUGUI display, string text){
        display.SetText(text);
    }

    // Burst only
    public void Burst(Camera camera, Transform attackPoint, Transform bulletFolder){
        if(mode == Mode.Burst){   	  
            if(CurrentAmmo >= BulletsPerBurstShot){
                for(int i = 0; i < BulletsPerBurstShot; i++){
                    Shoot(camera, attackPoint, bulletFolder);
                }
            }
        }
    }

    public void Zoom()
    {
        if(mode == Mode.Zoom){
            // Debug.Log("Zoom");
        }
    }

    public void Block()
    {
        if(mode == Mode.Block){
            // Debug.Log("Block");
        }
    }

    public override void Recharge(){
        ReloadingIsTrue = true;

        reloadRoutine = ReloadRoutine();
        CoroutineCaller.instance.StartCoroutine(reloadRoutine);
    }

    protected void StopRecharge(){
        ReloadingIsTrue = false;

        if(reloadRoutine == null){
            return;
        }

        CoroutineCaller.instance.StopCoroutine(reloadRoutine);
    }

    protected void RemoveParticles(){
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