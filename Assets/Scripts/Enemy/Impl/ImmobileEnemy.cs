using TouhouPride.Enemy;
using TouhouPride.Player;
using UnityEngine;

namespace TouhouPride
{
    public class ImmobileEnemy : AEnemyController
    {
        protected override Vector2? DoesAttack()
        {
            return PlayerController.Instance.transform.position - transform.position;
        }

        protected override Vector2 Move() => Vector2.zero;
    }
}