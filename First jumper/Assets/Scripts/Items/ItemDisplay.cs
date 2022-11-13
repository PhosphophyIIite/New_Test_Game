using UnityEngine;

public class ItemDisplay : MonoBehaviour
{
    public Gun gun;
    private Material m_Material;

    void Start()
    {
        if(gun == null){
            return;
        }

        gun.GetObject();

        if(gun.ItemModel != null && gun.ItemModel is GameObject){
            Instantiate(gun.ItemModel);
        }
    }
}