using UnityEngine;

public abstract class IItem : ScriptableObject
{
    public new string name;
    public int ammo;
    public float damage;
    public Material texture;
    public float fireRate;
    public string controls;

    public virtual void Shoot(){
        Debug.Log("Shoot here");
    }

    public virtual void Reload(){
        Debug.Log("Reload here");
    }

    public virtual string GetObject(){
        Debug.Log("GetObject here");
        return "GetObject here";
    }
}