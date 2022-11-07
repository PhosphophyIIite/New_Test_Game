using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
public class Gun : IItem
{
    public override void Shoot(){
        Debug.Log("Do some shooting with " + name);
    }    

    public override void Reload(){
        Debug.Log("Do some reloading");
    }

    public override string GetObject(){
        // Debug.Log("Name: " + name + ", Ammo: " + ammo + ", Texture: " + texture + ", Firerate: " + fireRate + ", Controls: " + controls);

        return "Name: " + name + ", Ammo: " + ammo + ", Texture: " + texture + ", Firerate: " + fireRate + ", Controls: " + controls;
    }
}
