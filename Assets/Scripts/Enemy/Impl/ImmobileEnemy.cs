using TouhouPride.Enemy;
using TouhouPride.Manager;
using UnityEngine;

namespace TouhouPride
{
    public class ImmobileEnemy : AEnemyController
    {
        protected override Vector2? DoesAttack()
        {
            return InputsManager.Instance.GetPriorityTarget(transform.position).transform.position - transform.position;
        }

        protected override Vector2 Move() => Vector2.zero;
    }
}