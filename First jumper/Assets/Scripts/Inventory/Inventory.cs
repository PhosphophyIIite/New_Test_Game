using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Public Variables

    public static List<IItem> items = new List<IItem>();
    public Rifle sniper;
    public Rifle ak47; // Remove this when implementing invetory system
    public Rifle p90; // Remove this when implementing invetory system

    #endregion

    #region Unity Methods

    public void Awake(){
        // items.Add(Instantiate(sniper));
        items.Add(Instantiate(ak47));
        items.Add(Instantiate(p90));
        
        foreach(IItem gun in items){
            Debug.Log(gun);
        }
    }

    #endregion
}