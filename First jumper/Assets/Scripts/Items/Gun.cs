using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
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
        set { maxAmmo = value; }
        get { return maxAmmo; }
    }

    [SerializeField]
    private int currentAmmo = 0;
    public int CurrentAmmo{
        set { currentAmmo = value; }
        get { return currentAmmo; }
    }

    [SerializeField]
    private float damage = 0;
    public float Damage{
        set { damage = value; }
        get { return damage; }
    }

    [Rename("Shot delay in Seconds")]
    [SerializeField]
    private float fireRate = 0f;
    public float FireRate{
        set { fireRate = value; }
        get { return fireRate; }
    }

    [SerializeField]
    private bool reloadingIsTrue = false;
    public bool ReloadingIsTrue{
        set { reloadingIsTrue = value; }
        get { return reloadingIsTrue; }
    }
    
    [SerializeField]
    private float spread = 0f;
    public float Spread{
        get { return fireRate; }
    }

    [SerializeField]
    private Mode mode;
    [SerializeField]
    private RifleBullet bullet;
    private IEnumerator reloadRoutine;

    private IEnumerator ReloadRoutine(){
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(ReloadTime);

        CurrentAmmo = MaxAmmo;
        reloadingIsTrue = false;
    }

    public override void Use(Camera camera, Transform attackPoint, Transform bulletFolder){
        // Debug.Log("Do some shooting with " + name);

        if(CurrentAmmo <= 0f || reloadingIsTrue){
            return;
        }
        
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        Vector3 targetPoint;
        if(Physics.Raycast(ray, out hit)){
            targetPoint = hit.point;
        } 
        else{
            targetPoint = ray.GetPoint(75);
        }  
        
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(UnityEngine.Random.Range(-spread, spread), UnityEngine.Random.Range(-spread, spread), 0f);

        GameObject newBullet = Instantiate(bullet.bullet, attackPoint.position, Quaternion.identity, bulletFolder.transform);
        newBullet.transform.forward = directionWithSpread.normalized;

        newBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * bullet.bulletSpeed, ForceMode.Impulse);

        CurrentAmmo--;
    }

    public override void SecondaryUse(){
        // Debug.Log("Think of things like: zooming in, doing a burst shot or block");

        if(mode == Mode.Zoom){
            // Debug.Log("Zoom");
        }
        
        if(mode == Mode.Burst){
            // Debug.Log("Burst");
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

    public override void StopRecharge(){
        if(reloadRoutine == null){
            return;
        }

        CoroutineCaller.instance.StopCoroutine(reloadRoutine);

        ReloadingIsTrue = false;
    }
    
    // Resets all changable values in the SO
    public void ResetToStartingValues(){
        Debug.Log("Resetting Scriptable Object");
        if(maxAmmo != 0){
            currentAmmo = MaxAmmo;
        }    
    }

    public override string GetObject(){
        // Debug.Log("Name: " + name + ", Ammo: " + ammo + ", Texture: " + texture + ", Firerate: " + fireRate + ", Controls: " + controls);

        // Keep up-to-date
        return "Name: " + name + ", Texture: " + Texture + ", Controls: " + Controls + ", Reloadtime(S): " + ReloadTime + ", Max Ammo: " + MaxAmmo + 
        ", Current Ammo: " + CurrentAmmo + ", Damage: " + Damage + " Shot Delay(S): " + FireRate + ", ReloadIsTrue: " + ReloadingIsTrue + ", Spread: " + Spread + 
        ", Mode: " + mode;
    }
}