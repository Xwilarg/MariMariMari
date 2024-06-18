using TouhouPride.Manager;
using UnityEngine;

namespace TouhouPride.Enemy.Impl
{
    public class BossEnemy : AEnemyController
    {
        private BossStateType _state;
        private float _timer = 2f;

        private float _speed = 1f;
        private Vector2 _direction = Vector2.zero;

        protected override void Awake()
        {
            base.Awake();

            _canTakeDamage = false;
        }

        private void Update()
        {
            if (!IsActive) return;

            _timer -= Time.deltaTime;
            if (_timer <= 0f)
            {
                switch (_state)
                {
                    case BossStateType.Idle: // Rush toward player to attack
                        _speed = 3f;
                        _direction = PlayerManager.Instance.Player.transform.position - transform.position;
                        _timer = 2f;
                        break;

                    case BossStateType.AttackSword:
                        _speed = 0f;
                        _direction = Vector2.zero;
                        _timer = 5f;

                        // TODO: Spawn bullets
                        break;

                    default:
                        throw new System.NotImplementedException();
                }
                if (_state == BossStateType.AttackSword) _state = BossStateType.Idle;
                else _state++;
            }
        }

        protected override Vector2? DoesAttack() => null;

        protected override Vector2 Move() => _direction;

        protected override float MoveSpeed => _speed;

        public void AllowDamage()
        {
            _canTakeDamage = true;
        }

        protected override bool PlayMoveAnimations => false;

        private enum BossStateType
        {
            Idle,
            AttackSword
        }
    }
}