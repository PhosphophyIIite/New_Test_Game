using UnityEngine;

[CreateAssetMenu(fileName = "New SniperBullet", menuName = "SniperBullet")]
public class SniperBullet : IBullet
{
    public override void OnEnable(){
        // Play a sound effect;
    }
}