using UnityEngine;

namespace TouhouPride.Enemy.Impl
{
    public class LeftRightEnemy : AEnemyController
    {
        /*
        private bool _goLeft;
        private bool _goDown;
        */

        private bool _goForward;

        private Vector2 forwardDirection = Vector2.left;
        private Vector2 backwardDirection = Vector2.right;
        
        private int _wallMask;

        public bool verticalMovement = false;

        protected override void Awake()
        {
            base.Awake();

            if (verticalMovement == true)
            {
                forwardDirection = Vector2.up;
                backwardDirection = Vector2.down;
            }
            
            _wallMask = LayerMask.GetMask("Wall");
        }

        protected override Vector2? DoesAttack() => AttackClosest();

        protected override Vector2 Move()
        {
            var check = Physics2D.Raycast(transform.position, _goForward ? forwardDirection : backwardDirection, 100f, _wallMask);
            if (check.collider != null && check.distance < 1f)
            {
                _goForward = !_goForward;
            }
            return backwardDirection * (_goForward ? -1 : 1);
            
            /*
            if (verticalMovement)
            {
                var check = Physics2D.Raycast(transform.position, _goDown ? Vector2.down : Vector2.up, 100f, _wallMask);
                if (check.collider != null && check.distance < 1f)
                {
                    _goDown = !_goDown;
                }
                return Vector2.up * (_goDown ? -1 : 1);
            }
            else
            {
                var check = Physics2D.Raycast(transform.position, _goLeft ? Vector2.left : Vector2.right, 100f, _wallMask);
                if (check.collider != null && check.distance < 1f)
                {
                    _goLeft = !_goLeft;
                }
                return Vector2.right * (_goLeft ? -1 : 1);
            }
            */
        }
    }
}