using UnityEngine;

public class CoroutineCaller : MonoBehaviour
{
     public static CoroutineCaller instance;
     
     void Start() {
         CoroutineCaller.instance = this;
     }
}