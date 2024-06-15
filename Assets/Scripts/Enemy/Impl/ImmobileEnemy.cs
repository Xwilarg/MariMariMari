using UnityEngine;

namespace TouhouPride.Enemy.Impl
{
    public class ImmobileEnemy : AEnemyController // TODO: when i do boss, need to disable damage until boss fight
    {
        protected override Vector2? DoesAttack() => AttackClosest();

        protected override Vector2 Move() => Vector2.zero;
    }
}