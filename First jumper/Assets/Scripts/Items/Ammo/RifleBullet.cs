using UnityEngine;

[CreateAssetMenu(fileName = "New RifleBullet", menuName = "RifleBullet")]
public class RifleBullet : IBullet
{
    public override void OnEnable(){
        // Play a sound effect;
    }
}