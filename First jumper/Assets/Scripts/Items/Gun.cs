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
        set { if (maxAmmo == 0 ) maxAmmo = value; }
        get { return maxAmmo; }
    }

    [SerializeField]
    private float damage = 0;
    public float Damage{
        set { if (damage == 0 ) damage = value; }
        get { return damage; }
    }
    
    [SerializeField]
    private float currentAmmo = 0f;
    public float CurrentAmmo{
        set { currentAmmo = value; }
        get { return currentAmmo; }
    }

    [Rename("Shot delay in Seconds")]
    [SerializeField]
    private float fireRate = 0f;
    public float FireRate{
        set { if (fireRate == 0f ) fireRate = value; }
        get { return fireRate; }
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

    public override void Use(Camera camera, Transform attackPoint, Transform bulletFolder){
        // Debug.Log("Do some shooting with " + name);

        if(currentAmmo <= 0f){
            return;
        }
        
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        Vector3 targetPoint;
        if(Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(75);
        
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0f);

        GameObject newBullet = Instantiate(bullet.bullet, attackPoint.position, Quaternion.identity, bulletFolder.transform);
        newBullet.transform.forward = directionWithSpread.normalized;

        newBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * bullet.bulletSpeed, ForceMode.Impulse);

        currentAmmo--;
        // instantiate bullet with coroutine here?
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
        Debug.Log("Do some reloading");
    }
    
    // Resets all changable values in the SO
    public void ResetToStartingValues(){
        if(maxAmmo != 0){
            currentAmmo = MaxAmmo;
        }
    }

    public override string GetObject(){
        // Debug.Log("Name: " + name + ", Ammo: " + ammo + ", Texture: " + texture + ", Firerate: " + fireRate + ", Controls: " + controls);

        // Needs update...
        return "!!!UPDATE ME!!! Name: " + name + ", Ammo: " + currentAmmo + ", Texture: " + Texture + ", Firerate: " + fireRate + ", Controls: " + Controls + " !!!UPDATE ME!!!";
    }
}