using System.Collections;
using UnityEngine;

namespace TouhouPride.Enemy
{
    public abstract class AEnemyController : MonoBehaviour
    {
        private bool _isActive;
        protected bool IsActive
        {
            set
            {
                _isActive = value;
                if (value)
                {
                    StartCoroutine(AttackTimerCoroutine());
                }
            }
            get => _isActive;
        }

        private Rigidbody2D _rb;

        protected abstract Vector2 Move();
        protected abstract void Attack();

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
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

        private IEnumerator AttackTimerCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(2f);
                Attack();
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
