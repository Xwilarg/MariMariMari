using TouhouPride.Manager;
using TouhouPride.SO;
using UnityEngine;

namespace TouhouPride
{
    public abstract class ACharacter : MonoBehaviour
    {
        [SerializeField]
        protected PlayerInfo _info;
        public virtual PlayerInfo Info { get => _info; }

        private int _health;

        protected virtual void TakeDamage()
        { }

        protected virtual void Awake()
        { }

        protected virtual void Start()
        {
            _health = Info.MaxHealth;
        }

        protected void Shoot(Vector2 direction, bool targetEnemy)
        {
            ShootingManager.Instance.Shoot(direction, targetEnemy, _info.AttackType, transform.position);
        }

        protected bool _canTakeDamage = true;

        public void TakeDamage(int amount)
        {
            if (!_canTakeDamage)
            {
                return;
            }
            _health -= amount;

            if (_health <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                TakeDamage();
            }
        }
    }
}