using UnityEngine;

[CreateAssetMenu(fileName = "New PistolBullet", menuName = "PistolBullet")]
public class PistolBullet : IBullet
{
    public override void OnEnable(){
        // Play a sound effect;
    }
}