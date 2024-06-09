using UnityEngine;

namespace TouhouPride.SO
{
    [CreateAssetMenu(menuName = "ScriptableObject/PlayerInfo", fileName = "PlayerInfo")]
    public class PlayerInfo : ScriptableObject
    {
        public float Speed;
        public AttackType AttackType;
        public float ReloadTime;
    }
}