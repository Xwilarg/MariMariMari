using Projectiles;
using UnityEngine;

namespace TouhouPride.Manager
{
	public class ShootingManager : MonoBehaviour
	{
		public static ShootingManager Instance { private set; get; }

        private void Awake()
        {
            Instance = this;
		}

		public void Shoot(Vector2 direction, bool targetEnemy, AttackType attack, Vector2 pos)
		{
			direction = direction.normalized;

			switch (attack)
			{
				case AttackType.Straight:
					var prefab = ResourcesManager.Instance.Bullet;

					var go = Instantiate(prefab, pos, Quaternion.identity);

					go.layer = targetEnemy ? LayerMask.NameToLayer("PlayerProjectile") : LayerMask.NameToLayer("EnemyProjectile");

					// TODO: i feel like this should possibly be done by the bullet itself? (feels like coupling as is)
					// Throw the projectile in direction
					go.GetComponent<StandardBullet>().Movement(direction);

					break;
				case AttackType.Wave:
					/*
                    // TODO; actually make it wavey
                    var prefab = ResourcesManager.Instance.Bullet;

                    var go = Instantiate(prefab, pos, Quaternion.identity);
                    go.layer = targetEnemy ? LayerMask.NameToLayer("PlayerProjectile") : LayerMask.NameToLayer("EnemyProjectile");

                    // TODO:
                    // Throw the projectile in direction
                    
                    
                    // Make a projectile script that GetComponent<ACharacter> and call TakeDamage()
                    */
					break;
			}
		}
	}
}