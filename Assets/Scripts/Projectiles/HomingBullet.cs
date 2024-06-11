using System;
using UnityEngine;

namespace Projectiles
{
    public class HomingBullet: StandardBullet
    {
        // TODO; implement homing behavior
        public GameObject target;
        public GameObject[] targets;

        public bool isTargeting = false;

        public int targetSpeed = 30;
        
        public override void Movement(Vector2 direction)
        {
            //print("cool homing stuff here.");
            //base.Movement(direction);
        }

        public void Start()
        {
            //target = GameObject.FindWithTag("Enemy");
            targets = GameObject.FindGameObjectsWithTag("Enemy");
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
                Vector2 newPosition =
                    Vector2.MoveTowards(transform.position, target.transform.position, targetSpeed * Time.deltaTime); //_bulletInfo.bulletSpeed * Time.deltaTime);
                _rigidbody2D.MovePosition(newPosition);

                if (target == null)
                {
                    isTargeting = false;
                    //Movement(new Vector2(_rigidbody2D.rotation, transform.rotation.y));
                    base.Movement(new Vector2((float)Math.Sin(_rigidbody2D.rotation), (float)-Math.Cos(_rigidbody2D.rotation)));
                }
            }
            
            /*
            // assume first object is the nearest one.
            GameObject nearestObject = targets[0];
            float distanceToNearest = Vector2.Distance(target.transform.position, nearestObject.transform.position);

            for (int i = 1; i < targets.Length; i++)
            {
                float distanceToCurrent = Vector2.Distance(target.transform.position, targets[i].transform.position);

                if (distanceToCurrent < distanceToNearest)
                {
                    nearestObject = targets[i];
                    distanceToNearest = distanceToCurrent;
                }
            }
            
            Vector2 newPosition =
                Vector2.MoveTowards(transform.position, nearestObject.transform.position, _bulletInfo.bulletSpeed * Time.deltaTime);
            _rigidbody2D.MovePosition(newPosition);
            */
            
            /*
            

            if (target == null)
            {
                // find another target
                target = GameObject.FindWithTag("Enemy");
            }
            */
        }
    }
}