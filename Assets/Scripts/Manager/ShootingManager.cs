using System.Collections;
using Projectiles;
using UnityEngine;

namespace TouhouPride.Manager
{
	public class ShootingManager : MonoBehaviour
	{

		private GameObject[] _enemiesInScene;
		
		public static ShootingManager Instance { private set; get; }

        private void Awake()
        {
            Instance = this;
		}

        public IEnumerator homeIn(GameObject bullet)
        {
	        // wait
	        yield return new WaitForSeconds(0.5f);
	        
	        print("homing in");
	        
	        // home in
	        _enemiesInScene = GameObject.FindGameObjectsWithTag("Enemy");
	        
	        print("target: " + _enemiesInScene[0]);

	        
	        if (bullet)
	        {
		        bullet.GetComponent<HomingBullet>().StartTargeting(_enemiesInScene[0]);
		        // lets just see what happens if we only target the first one for now
		        /*
		        bullet.GetComponent<HomingBullet>().target = _enemiesInScene[0];
		        bullet.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		        bullet.GetComponent<HomingBullet>().isTargeting = true;
		        */
	        }
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
				case AttackType.Homing:
					// shoot bullet in direction aimed
					
					// TODO: get enemies in immediate vicinity, and then aim the bullet there. 
					
					var homingPrefab = ResourcesManager.Instance.Bullet;
					
					var goTwo = Instantiate(homingPrefab, pos, Quaternion.identity);

					goTwo.layer = targetEnemy ? LayerMask.NameToLayer("PlayerProjectile") : LayerMask.NameToLayer("EnemyProjectile");

					// TODO: i feel like this should possibly be done by the bullet itself? (feels like coupling as is)
					// Throw the projectile in direction
					goTwo.GetComponent<StandardBullet>().Movement(direction);
					
					// wait a few seconds; after go is shot, move the bullet towards an enemy reference. 
					//if (_enemiesInScene.Length > 0)
					//{
						StartCoroutine(homeIn(goTwo));
					//}
					break;
				case AttackType.Laser:
					
					break;
			}
		}
	}
}