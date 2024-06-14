using System;
using TouhouPride;
using TouhouPride.SO;
using UnityEngine;

namespace Projectiles
{
    public class LaserBullet: StandardBullet
    {

        // For some reason, trying to do GetComponent 
        private LineRenderer _lineRenderer;
        public Transform laserHit;
        public float laserRange = 10;
        
        public bool laserIsActive;

        //public Vector2 aim;
        
        private void Awake()
        {
            // get line renderer component.
            _lineRenderer = GetComponent<LineRenderer>();
            //_lineRenderer.enabled = true;
            _lineRenderer.useWorldSpace = true;
            // disable line renderer component.

        }

        public void DrawLaser(Vector2 origin, Vector2 target)
        {
            _lineRenderer.SetPosition(0, origin);
            _lineRenderer.SetPosition(1, target);
        }

        private void Update()
        {
            //DrawLaser(transform.position, Vector2.left);
            
        }
    }
}