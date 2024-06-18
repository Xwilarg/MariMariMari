using System.Collections;
using FMOD.Studio;
using Projectiles;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
		
		private IEnumerator HomeIn(GameObject bullet)
		{
			// wait
			yield return new WaitForSeconds(0.5f);

			// home in
			_enemiesInScene = GameObject.FindGameObjectsWithTag("Enemy");

			if (bullet && _enemiesInScene.Length > 0)
			{
				// lets just target first in array for now.
				bullet.GetComponent<HomingBullet>().StartTargeting(_enemiesInScene[0]);
			}
		}

		public void Shoot(Vector2 direction, bool targetEnemy, AttackType attack, Vector2 pos, int soundEventParameter)
		{
			direction = direction.normalized;

			switch (attack)
			{
				case AttackType.Straight:
					// play SFX
					AudioManager.instance.PlayOneShotParam(FModReferences.instance.shoot, gameObject.transform.position, "SHOOT", soundEventParameter);
					
					var prefab = ResourcesManager.Instance.Bullet;

					var go = Instantiate(prefab, pos, Quaternion.identity);

					go.layer = targetEnemy ? LayerMask.NameToLayer("PlayerProjectile") : LayerMask.NameToLayer("EnemyProjectile");

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
					// play SFX
					AudioManager.instance.PlayOneShotParam(FModReferences.instance.shoot, gameObject.transform.position, "SHOOT", soundEventParameter);

					// TODO: get enemies in immediate vicinity, and then aim the bullet there. 

					var homingPrefab = ResourcesManager.Instance.HomingBullet;

					var goHoming = Instantiate(homingPrefab, pos, Quaternion.identity);

					goHoming.layer = targetEnemy ? LayerMask.NameToLayer("PlayerProjectile") : LayerMask.NameToLayer("EnemyProjectile");

					// Throw the projectile in direction
					goHoming.GetComponent<StandardBullet>().Movement(direction);

					StartCoroutine(HomeIn(goHoming));
					break;
				case AttackType.Laser:
					var layer = LayerMask.GetMask(targetEnemy ? "Enemy" : "Player", "Wall");
					var maxDist = 10f;

                    var laserPrefab = ResourcesManager.Instance.Laser;
                    var goLaser = Instantiate(laserPrefab, pos, Quaternion.identity);
					print("destroying laser");
					Destroy(goLaser, 1f);
					var laser = goLaser.GetComponent<LineRenderer>();

					print("hit stuff");
                    var hit = Physics2D.Raycast(pos, direction, maxDist, layer);
					if (hit.collider != null)
                    {
                        laser.SetPositions(new[] { (Vector3)pos, (Vector3)(hit.point) });
						if (hit.collider.TryGetComponent<ACharacter>(out var c))
						{
							c.TakeDamage(1);
						}
                    }
					else
                    {
						laser.SetPositions(new[] { (Vector3)pos, (Vector3)(pos + (direction * maxDist)) });

                    }
					
					break;
			}
		}
	}
}