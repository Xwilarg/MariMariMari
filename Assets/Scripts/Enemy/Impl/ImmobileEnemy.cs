using TouhouPride.Enemy;
using UnityEngine;

namespace TouhouPride
{
    public class ImmobileEnemy : AEnemyController
    {
        protected override void Attack()
        { }

        protected override Vector2 Move() => Vector2.zero;
    }
}