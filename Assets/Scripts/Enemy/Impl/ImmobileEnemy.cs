using TouhouPride.Enemy;
using TouhouPride.Manager;
using UnityEngine;

namespace TouhouPride
{
    public class ImmobileEnemy : AEnemyController
    {
        protected override Vector2? DoesAttack() => AttackClosest();

        protected override Vector2 Move() => Vector2.zero;
    }
}