using UnityEngine;

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
        public RuntimeAnimatorController CharacterAnimator;
        public RuntimeAnimatorController CharacterDashAnimator;

        public float Scale = 1f;

        public Sprite BombImage;
        
        //== sound stuff==
        // all the shoot sound effects are stored in one 'Event';
        // we pass a parameter to said event based on who is firing (i.e; Alice, Marisa, Enemy, etc)
        public int ShootParamValue;
    }
}