using UnityEngine;

public class ItemDisplay : MonoBehaviour
{
    public Gun gun;
    private Material m_Material;

    void Start()
    {
        gun.GetObject();

        GetComponent<Renderer>().material = gun.texture;
    }
}