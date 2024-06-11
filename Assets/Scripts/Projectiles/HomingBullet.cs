using System;
using UnityEngine;

namespace Projectiles
{
    public class HomingBullet: StandardBullet
    {
        public GameObject target;

        public bool isTargeting = false;

        public int targetSpeed = 30;
        
        public override void Movement(Vector2 direction)
        {
            //print("cool homing stuff here.");
            //base.Movement(direction);
        }

        public void Start()
        {
            GameObject.FindGameObjectsWithTag("Enemy");
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                target = other.gameObject;
            }
        }

        public void StartTargeting(GameObject targetObj)
        {
            print("start targeting");
            _rigidbody2D.velocity = Vector2.zero;
            target = targetObj;
            isTargeting = true;
        }

        void Update()
        {
            if (isTargeting)
            {
                if (target == null)
                {
                    isTargeting = false;
                    base.Movement(new Vector2((float)Math.Sin(_rigidbody2D.rotation), (float)-Math.Cos(_rigidbody2D.rotation)));
                }
                else
                {
                    Vector2 newPosition =
                        Vector2.MoveTowards(transform.position, target.transform.position, targetSpeed * Time.deltaTime); //_bulletInfo.bulletSpeed * Time.deltaTime);
                    _rigidbody2D.MovePosition(newPosition);
                }
            }
            Destroy(this.gameObject, _bulletInfo.timeToLive);
        }
    }
}