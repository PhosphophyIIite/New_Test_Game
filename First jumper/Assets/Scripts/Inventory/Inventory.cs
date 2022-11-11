using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Public Variables

    public static List<Gun> guns = new List<Gun>();
    public Gun ak47; // Remove this when implementing invetory system
    public Gun p90; // Remove this when implementing invetory system

    #endregion

    #region Unity Methods

    public void Awake(){
        guns.Add(Instantiate(ak47));
        guns.Add(Instantiate(p90));
        
        foreach(Gun gun in guns){
            Debug.Log(gun);
        }
    }

    #endregion
}