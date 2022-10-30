using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieCube : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(transform.position.x > 100f || transform.position.y > 100f || transform.position.z > 100f || transform.position.x < -100f || transform.position.y < -100f || transform.position.z < -100f)
        {
            Debug.Log("I have been summoned at: " + transform.position);
        }
        else
        {
            Destroy(gameObject, 1f);
        }
    }
}
