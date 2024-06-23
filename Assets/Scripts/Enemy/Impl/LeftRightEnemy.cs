using UnityEngine;

namespace TouhouPride.Enemy.Impl
{
    public class LeftRightEnemy : AEnemyController
    {
        private bool _goForward;

        private Vector2 _forwardDirection = Vector2.left;
        private Vector2 _backwardDirection = Vector2.right;
        
        private int _wallMask;

        public bool verticalMovement = false;

        protected override void Awake()
        {
            base.Awake();

            if (verticalMovement == true)
            {
                _forwardDirection = Vector2.up;
                _backwardDirection = Vector2.down;
            }
            
            _wallMask = LayerMask.GetMask("Wall", "Water", "RayBlock");
        }

        protected override Vector2? DoesAttack() => AttackClosest();

        protected override Vector2 Move()
        {
            var check = Physics2D.Raycast(transform.position, _goForward ? _forwardDirection : _backwardDirection, 100f, _wallMask);
            if (check.collider != null && check.distance < 1f)
            {
                _goForward = !_goForward;
            }
            return _backwardDirection * (_goForward ? -1 : 1);
        }
    }
}