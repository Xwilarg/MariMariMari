using System.Collections;
using TouhouPride.Love;
using TouhouPride.Manager;
using TouhouPride.Utils;
using UnityEngine;

namespace TouhouPride.Enemy
{
    public abstract class AEnemyController : ACharacter
    {
        private bool _isActive;
        public bool IsActive
        {
            set
            {
                if (!_isActive && value) // First time being activated
                {
                    StartCoroutine(AttackTimerCoroutine());
                }
                _isActive = value;
            }
            get => _isActive;
        }

        private Rigidbody2D _rb;
        private SpriteRenderer _sr;

        private int _targettingLayer;

        protected abstract Vector2 Move();
        protected abstract Vector2? DoesAttack();
        protected virtual float MoveSpeed => 1f;

        protected virtual bool PlayMoveAnimations => true;

        protected override void Awake()
        {
            base.Awake();

            _rb = GetComponent<Rigidbody2D>();
            _sr = GetComponent<SpriteRenderer>();
            _targettingLayer = LayerMask.GetMask("Wall", "Player");

            var detector = GetComponentInChildren<Detector>();
            detector.OnEnter.AddListener((c) =>
            {
                if (c.CompareTag("Player"))
                {
                    IsActive = true;
                }
            });
            detector.GetComponent<CircleCollider2D>().radius = Info.Range;
        }

        protected override void Die()
        {
            base.Die();

            if (Random.Range(0, 100) < 10)
            {
                var go = Instantiate(ResourcesManager.Instance.Heart, transform.position, Quaternion.identity);
                go.GetComponent<PartnerPickup>()._partnerTarget = PlayerManager.Instance.Follower.Info;
            }
        }

        protected override void Start()
        {
            base.Start();

            EnemyManager.Instance.Register(this);
        }

        private void OnDestroy()
        {
            AudioManager.instance.PlayOneShot(FModReferences.instance.defeat, gameObject.transform.position);
            EnemyManager.Instance.Unregister(this);
        }

        private void FixedUpdate()
        {
            _rb.velocity = Move().normalized * MoveSpeed;
            if (PlayMoveAnimations)
            {
                _anim.SetFloat("X", OneOne(_rb.velocity.x));
                _anim.SetFloat("Y", OneOne(_rb.velocity.y));
            }
        }

        protected Vector2? AttackClosest()
            => PlayerManager.Instance.GetPriorityTarget(transform.position).transform.position - transform.position;

        protected override void TakeDamage()
        {
            AudioManager.instance.PlayOneShot(FModReferences.instance.hit, gameObject.transform.position);
            base.TakeDamage();
            StartCoroutine(TakeDamageEffect());
            IsActive = true;
        }

        private IEnumerator TakeDamageEffect()
        {
            _sr.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(.1f);
            _sr.color = Color.white;
        }

        private IEnumerator AttackTimerCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(Info.ReloadTime);
                var attackDir = DoesAttack();
                if (attackDir.HasValue)
                {
                    var hit = Physics2D.Raycast(transform.position, attackDir.Value, 100f, _targettingLayer);
                    Debug.DrawLine(transform.position, hit.collider == null ? (transform.position + (Vector3)attackDir.Value) : hit.point, Color.red, 1f);
                    if (hit.collider != null && hit.collider.CompareTag("Player"))
                    {
                        Shoot(attackDir.Value, false, 0);
                    }
                    else
                    {
                        yield return new WaitForEndOfFrame();
                    }
                }
            }
        }
        
        
    }
}
