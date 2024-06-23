using TouhouPride.Manager;
using UnityEngine;

namespace TouhouPride.Enemy.Impl
{
    public class RunAwayEnemy : AEnemyController
    {
        protected override Vector2? DoesAttack() => null;

        protected override Vector2 Move() => PlayerManager.Instance.Player.transform.position - transform.position;
    }
}