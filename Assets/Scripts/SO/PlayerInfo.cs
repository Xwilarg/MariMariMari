using UnityEngine;
using UnityEngine.Animations;

namespace TouhouPride.SO
{
    [CreateAssetMenu(menuName = "ScriptableObject/PlayerInfo", fileName = "PlayerInfo")]
    public class PlayerInfo : ScriptableObject
    {
        public string Name;
        public float Speed;
        public AttackType AttackType;
        public float ReloadTime;
        public int MaxHealth;
        public int Range;

        public Color Color;
        public TextAsset EndStory;
        public AnimatorOverrideController CharacterAnimator;
    }
}