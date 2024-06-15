using System.Collections.Generic;
using TouhouPride.Manager;
using TouhouPride.Map;
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

        private List<IRequirement<ACharacter>> _requirements = new();
        public void AddRequirement(IRequirement<ACharacter> req)
        {
            _requirements.Add(req);
        }

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
                foreach (var r in _requirements)
                {
                    r.Unlock(this);
                }
                Destroy(gameObject);
            }
            else
            {
                TakeDamage();
            }
        }
    }
}