using TouhouPride;
using TouhouPride.SO;
using UnityEngine;

namespace Projectiles
{
    public class StandardBullet : MonoBehaviour
    {
        [SerializeField] private BulletInfo _bulletInfo;
        
        private Rigidbody2D _rigidbody2D;

        public Vector2 direction = Vector2.zero;

        private void Awake()
        {
            _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            Destroy(this.gameObject, _bulletInfo.timeToLive);
        }

        // movement code. can be overriden for custom movements.
        // TODO; do we want this to be a Scriptable Object?
        public virtual void Movement(Vector2 direction)
        {
            this.direction = direction;
            _rigidbody2D.AddForce(direction * _bulletInfo.bulletSpeed, ForceMode2D.Impulse);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.TryGetComponent<ACharacter>(out var c))
            {
                c.TakeDamage(_bulletInfo.damage);
            }
            Destroy(this.gameObject);
        }
    }
}
