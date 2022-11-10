using UnityEngine;

public abstract class IBullet : ScriptableObject
{
    public GameObject bullet;
    public float bulletSpeed; 

    public virtual void OnEnable(){
        // Play sound effect
    }
}