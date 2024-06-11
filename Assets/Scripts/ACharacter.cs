using TouhouPride.Manager;
using TouhouPride.SO;
using UnityEngine;

namespace TouhouPride
{
    public abstract class ACharacter : MonoBehaviour
    {
        [SerializeField]
        private PlayerInfo _info;
        public PlayerInfo Info => _info;

        private int _health;

        protected virtual void TakeDamage()
        { }

        protected virtual void Awake()
        {
            _health = _info.MaxHealth;
        }

        protected void Shoot(Vector2 direction, bool targetEnemy)
        {
            ShootingManager.Instance.Shoot(direction, targetEnemy, _info.AttackType, transform.position);
        }

        public void TakeDamage(int amount)
        {
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