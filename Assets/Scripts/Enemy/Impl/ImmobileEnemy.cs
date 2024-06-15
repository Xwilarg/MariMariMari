using UnityEngine;

namespace TouhouPride.Enemy.Impl
{
    public class ImmobileEnemy : AEnemyController
    {
        private bool _canTakeDamage;

        protected override Vector2? DoesAttack() => AttackClosest();

        protected override Vector2 Move() => Vector2.zero;
    }
}