using UnityEngine;

namespace TouhouPride.SO
{
    [CreateAssetMenu(menuName = "ScriptableObject/BulletInfo", fileName = "BulletInfo")]
    public class BulletInfo: ScriptableObject
    {
        [Tooltip("Damage that is dealt to enemies upon contact")]
        public int damage;
        
        [Tooltip("Speed at which bullet should move.")]
        public float bulletSpeed;

        [Tooltip("Time before bullet automatically destroys self.")]
        public float timeToLive = 5;
        
        [Tooltip("Does the bullet deal damage to the player? If false, it does.")]
        public bool isFriendly = false;
    }
}