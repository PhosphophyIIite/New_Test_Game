using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class IItem : ScriptableObject
{    
    [SerializeField]
    private new string name = "Default Item Name Text";
    public string Name{
        // set { name = value; }
        get { return name; }
    }

    [SerializeField]
    private GameObject itemModel;
    public GameObject ItemModel{
        // set { texture = value; }
        get { return itemModel; }
    }

    private GameObject itemModelInstantiated;
    public GameObject ItemModelInstantiated{
        set { itemModelInstantiated = value; }
        get { return itemModelInstantiated; }
    }

    [SerializeField]
    private string controls = "Default Item Controls Text";
    public string Controls{
        // set { controls = value; }
        get { return controls; }
    }

    [SerializeField]
    [Rename("ReloadTime in Sec")]
    private float rechargeTime = 1f;
    public float RechargeTime{
        // set { reloadTime = value; }
        get { return rechargeTime; }
    }

    [SerializeField]
    [Rename("Secondary Use Delay in Sec")]
    private float secondaryUseDelay = 1f;
    public float SecondaryUseDelay{
        // set { secondaryUseDelay = value; }
        get { return secondaryUseDelay; }
    }

    // Rifle : Gun
    public virtual void EnableItem(Transform attackPoint, Transform itemHolder, Transform shotFolder, Camera camera, GameObject crossHair){
        Debug.Log("Put OnEnable Logic here");
    }

    // Rifle : Gun
    public virtual void Use(bool useIsPressed, bool rechargeIsPressed, bool secondaryUseIsPressed, Camera camera, Transform attackPoint, Transform ammoFolder){ //left click
        Debug.Log("Rifle Use here");
    }

    // Make more Use functions for different Items

    // Rifle : Gun
    public virtual void SecondaryUse(Camera camera, Transform attackPoint, Transform ammoFolder){ //right click
        Debug.Log("Bursting here");
    }

    public virtual void SecondaryUse(){ //right click
        Debug.Log("Zooming or blocking or else here");
    }

    public virtual void Recharge(){
        Debug.Log("Reload here");
    }
    
    public virtual void DisableItem(GameObject uiElement){
        Debug.Log("Stop coroutines, set booleans to false, remove insatniated objects etc...");
    }

    public virtual string GetObject(){
        Debug.Log("GetObject here");
        return "No Object Developer";
    }
}