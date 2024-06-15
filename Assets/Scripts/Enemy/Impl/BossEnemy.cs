using UnityEngine;

namespace TouhouPride.Enemy.Impl
{
    public class BossEnemy : AEnemyController
    {
        protected override void Awake()
        {
            base.Awake();

            _canTakeDamage = false;
        }

        protected override Vector2? DoesAttack() => AttackClosest();

        protected override Vector2 Move() => Vector2.zero;

        public void AllowDamage()
        {
            _canTakeDamage = true;
        }
    }
}