using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Gun : IItem
{
    public enum Mode
    {
        Zoom, // default
        Burst,
        Block
    }

    #region Variables

    [SerializeField]
    private int maxAmmo = 0;
    protected int MaxAmmo{
        // set { maxAmmo = value; }
        get { return maxAmmo; }
    }

    [SerializeField]
    private int currentAmmo = 0;
    protected int CurrentAmmo{
        set { currentAmmo = value; }
        get { return currentAmmo; }
    }

    [SerializeField]
    private float damage = 0f;
    protected float Damage{
        // set { damage = value; }
        get { return damage; }
    }

    [Rename("Shot delay in Seconds")]
    [SerializeField]
    private float fireRate = 0f;
    protected float FireRate{
        // set { fireRate = value; }
        get { return fireRate; }
    }

    [SerializeField]
    private bool reloadingIsTrue = false;
    protected bool ReloadingIsTrue{
        set { reloadingIsTrue = value; }
        get { return reloadingIsTrue; }
    }

    [SerializeField]
    private float bulletsPerTap = 1f;
    protected float BulletsPerTap{
        // set { bulletsPerTap = value; }
        get { return bulletsPerTap; }
    }

    [SerializeField]
    private float bulletsPerBurstShot = 1f;
    protected float BulletsPerBurstShot{
        // set { bulletsPerBurstShot = value; }
        get { return bulletsPerBurstShot; }
    }
    
    [SerializeField]
    private float spread = 0f;
    private float Spread{
        // set { spread = value; }
        get { return spread; }
    }

    [SerializeField]
    private Mode mode;
    protected Mode GunMode{
        // set { mode = value; }
        get { return mode; }
    }

    private Image crossHair;
    protected Image CrossHair{
        set { crossHair = value; }
        get { return crossHair; }
    }


    [SerializeField]
    private GameObject muzzleFlash;
    protected GameObject MuzzleFlash{
        get { return muzzleFlash; }
    }

    private GameObject muzzleFlashInstantiated;
    protected GameObject MuzzleFlashInstantiated{
        set { muzzleFlashInstantiated = value; }
        get { return muzzleFlashInstantiated; }
    }

    [SerializeField]
    private IBullet bullet;
    private IBullet Bullet{
        get { return bullet; }
    }

    private IEnumerator reloadRoutine;
    private IEnumerator ReloadRoutine{
        set { reloadRoutine = value; }
        get { return reloadRoutine; }
    }

    private TextMeshProUGUI ammuntionDisplay;
    protected TextMeshProUGUI AmmuntionDisplay{
        set { ammuntionDisplay = value; }
        get { return ammuntionDisplay; }
    }

    private bool shotDelayRoutineIsActive = false;
    protected bool ShotDelayRoutineIsActive{
        set { shotDelayRoutineIsActive = value; }
        get { return shotDelayRoutineIsActive; }
    }

    #endregion

    #region Functions

    private IEnumerator ReloadCoRoutine(){
        Debug.Log("Reloading...");
        SetGUIColor(Color.black, CrossHair);

        yield return new WaitForSeconds(RechargeTime);

        CurrentAmmo = MaxAmmo;
        if(ammuntionDisplay != null){
            UpdateGUI(ammuntionDisplay, CurrentAmmo / BulletsPerTap + " / " + MaxAmmo / BulletsPerTap);
        }

        reloadingIsTrue = false;
    }

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

        SetGUIColor(Color.magenta, CrossHair);
        
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

    public void Zoom(Camera camera, float targetZoom, float zoomDuration)
    {
        if(mode == Mode.Zoom){
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

        reloadRoutine = ReloadCoRoutine();
        CoroutineCaller.instance.StartCoroutine(reloadRoutine);
    }

    protected void SetGUIColor(Color color, Image guiElement){
        guiElement.color = color;
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

    protected void DisableUI(GameObject crossHairGameObject){
        crossHairGameObject.SetActive(false);
    }

    // Maybe move this to child class? Maybe Find a library for this? Maybe only show public and protected variables?
    public override string GetObject(){
        // Debug.Log("Name: " + name + ", Ammo: " + ammo + ", Texture: " + texture + ", Firerate: " + fireRate + ", Controls: " + controls);

        // Keep up-to-date
        return "Name: " + name + ", Texture: " + Texture + ", Controls: " + Controls + ", Reloadtime(S): " + RechargeTime + ", Max Ammo: " + MaxAmmo + 
        ", Current Ammo: " + CurrentAmmo + ", Damage: " + Damage + " Shot Delay(S): " + FireRate + ", Bullets per BurstShot: " + BulletsPerBurstShot + 
        ", SecondaryUse Delay: " + SecondaryUseDelay + ", ReloadIsTrue: " + ReloadingIsTrue + ", Spread: " + Spread + ", Mode: " + GunMode;
    }

    #endregion
}