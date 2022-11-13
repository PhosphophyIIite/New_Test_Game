using UnityEngine;

public abstract class IBullet : ScriptableObject
{
    public GameObject bullet;
    public float bulletSpeed; 

    // Put this function in Update() to make bullet noise
    public virtual void PlaySound(){
        // Play sound effect
    }
}