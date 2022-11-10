using UnityEngine;

public class BulletDie : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 1f);
    }
}