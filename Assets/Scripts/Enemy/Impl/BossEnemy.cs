using Ink.Parsed;
using Projectiles;
using System.Collections.Generic;
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

        private Vector2 _basePos;
        private List<StandardBullet> _bullets = new();
        private Vector2 _bulletThrowDir;

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
                        _speed = 10f;
                        _direction = PlayerManager.Instance.Player.transform.position - transform.position;
                        _basePos = transform.position;
                        _timer = 2f;
                        break;

                    case BossStateType.AttackSword:
                        _speed = 0f;
                        _direction = Vector2.zero;
                        _timer = 1f;
                        _bullets.Clear();

                        var p1 = transform.position;
                        var p2 = _basePos;
                        var mid = new Vector2((p1.x + p2.x) / 2f, (p1.y + p2.y) / 2f);
                        var dir = (Vector2)PlayerManager.Instance.Player.transform.position - mid;

                        var prefab = ResourcesManager.Instance.Bullet;
                        var max = Vector2.Distance(p1, p2);
                        _bulletThrowDir = dir;
                        for (float x = 0; x < max; x += 2f)
                        {
                            var go = Instantiate(prefab, Vector2.Lerp(p1, p2, x / max), Quaternion.identity);
                            go.layer = LayerMask.NameToLayer("EnemyProjectile");
                            _bullets.Add(go.GetComponent<StandardBullet>());
                        }

                        // TODO: Spawn bullets
                        break;

                    case BossStateType.ThrowBullets:
                        _timer = 2f;
                        foreach (var b in _bullets)
                        {
                            if (b != null)
                            {
                                b.Movement(_bulletThrowDir);
                            }
                        }
                        break;

                    default:
                        throw new System.NotImplementedException();
                }
                if (_state == BossStateType.ThrowBullets) _state = BossStateType.Idle;
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
            AttackSword,
            ThrowBullets
        }
    }
}