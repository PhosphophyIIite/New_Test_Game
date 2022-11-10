using UnityEngine;

public abstract class IItem : ScriptableObject
{    
    [SerializeField]
    private new string name = "Default Item Name Text";
    public string Name{
        set { name = value; }
        get { return name; }
    }

    [SerializeField]
    private Material texture;
    public Material Texture{
        set { texture = value; }
        get { return texture; }
    }

    [SerializeField]
    private string controls = "Default Item Controls Text";
    public string Controls{
        set { controls = value; }
        get { return controls; }
    }

    [SerializeField]
    private float reloadTime = 1f;
    public float ReloadTime{
        set { reloadTime = value; }
        get { return reloadTime; }
    }

    public virtual void Use(Camera camera, Transform attackPoint, Transform ammoFolder){ //left click
        Debug.Log("Shoot here");
    }

    public virtual void SecondaryUse(){ //right click
        Debug.Log("Zooming, bursting, blocking or else here");
    }

    public virtual void Recharge(){
        Debug.Log("Reload here");
    }

    public virtual string GetObject(){
        Debug.Log("GetObject here");
        return "No Object Developer";
    }
}