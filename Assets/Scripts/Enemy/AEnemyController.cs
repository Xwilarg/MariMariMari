using System.Collections;
using UnityEngine;

namespace TouhouPride.Enemy
{
    public abstract class AEnemyController : ACharacter
    {
        private bool _isActive;
        protected bool IsActive
        {
            set
            {
                if (!_isActive && value) // First time being activated
                {
                    StartCoroutine(AttackTimerCoroutine());
                }
                _isActive = value;
            }
            get => _isActive;
        }

        private Rigidbody2D _rb;

        private int _wallLayer;

        protected abstract Vector2 Move();
        protected abstract Vector2? DoesAttack();

        protected override void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            var l1 = 1 << LayerMask.NameToLayer("Wall");
            var l2 = 1 << LayerMask.NameToLayer("Player");
            _wallLayer = l1 | l2;
            base.Awake();
        }

        private void FixedUpdate()
        {
            if (IsActive)
            {
                _rb.velocity = Move();
            }
            else
            {
                _rb.velocity = Vector2.zero;
            }
        }

        protected override void TakeDamage()
        {
            base.TakeDamage();

            IsActive = true;
        }

        private IEnumerator AttackTimerCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(Info.ReloadTime);
                var attackDir = DoesAttack();
                if (attackDir.HasValue)
                {
                    Debug.DrawLine(transform.position, transform.position + (Vector3)attackDir.Value, Color.red, 1f);
                    var hit = Physics2D.Raycast(transform.position, attackDir.Value, 100f, _wallLayer);
                    if (hit.collider != null && hit.collider.CompareTag("Player"))
                    {
                        Shoot(attackDir.Value, false);
                    }
                    else
                    {
                        yield return new WaitForEndOfFrame();
                    }
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                IsActive = true;
            }
        }
    }
}
