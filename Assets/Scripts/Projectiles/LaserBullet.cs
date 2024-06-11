using System;
using TouhouPride;
using TouhouPride.SO;
using UnityEngine;

namespace Projectiles
{
    public class LaserBullet: StandardBullet
    {
        public override void Movement(Vector2 direction)
        {
            base.Movement(direction);
        }

        public void SetAim(Vector2 aimDirection)
        {
            //base.Movement(new Vector2((float)Math.Sin(_rigidbody2D.rotation), (float)-Math.Cos(_rigidbody2D.rotation)));
            //base._rigidbody2D.MoveRotation(Vector2.Angle(Vector2.up, aimDirection));
            //transform.rotation.SetFromToRotation();
            
            // want to rotate z value
            
            
            
            //var transformRotation = transform.rotation;
            //transformRotation.eulerAngles = aimDirection;

            float rotation = (float)Math.Asin(aimDirection.x) + -(float)Math.Acos(aimDirection.y);
            
            transform.Rotate(0, 0, rotation);
            
            print("new: " + transform.rotation);
        }
    }
}