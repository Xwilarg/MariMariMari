using Projectiles;
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

        protected void Init()
        {
            _health = _info.MaxHealth;
        }

        protected void Shoot(Vector2 direction, bool targetEnemy)
        {
            direction = direction.normalized;

            switch (_info.AttackType)
            {
                case AttackType.Straight:
                    var prefab = ResourcesManager.Instance.Bullet;
                    
                    var go = Instantiate(prefab, transform.position, Quaternion.identity);
                    
                    go.layer = targetEnemy ? LayerMask.NameToLayer("PlayerProjectile") : LayerMask.NameToLayer("EnemyProjectile");
                    
                    // TODO: i feel like this should possibly be done by the bullet itself? (feels like coupling as is)
                    // Throw the projectile in direction
                    go.GetComponent<StandardBullet>().Movement(direction);
                    
                    break;
                case AttackType.Wave:
                    /*
                    // TODO; actually make it wavey
                    var prefab = ResourcesManager.Instance.Bullet;

                    var go = Instantiate(prefab, transform.position, Quaternion.identity);
                    go.layer = targetEnemy ? LayerMask.NameToLayer("PlayerProjectile") : LayerMask.NameToLayer("EnemyProjectile");

                    // TODO:
                    // Throw the projectile in direction
                    
                    
                    // Make a projectile script that GetComponent<ACharacter> and call TakeDamage()
                    */
                    break;
            }
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