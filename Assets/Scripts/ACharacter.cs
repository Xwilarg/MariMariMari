using System.Collections.Generic;
using TouhouPride.Manager;
using TouhouPride.Map;
using TouhouPride.SO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TouhouPride
{
    public abstract class ACharacter : MonoBehaviour
    {
        [SerializeField]
        protected PlayerInfo _info;
        public virtual PlayerInfo Info { get => _info; }

        public RectTransform HealthBar { set; private get; }

        protected Animator _anim;

        private int _health;

        private List<IRequirement<ACharacter>> _requirements = new();
        public void AddRequirement(IRequirement<ACharacter> req)
        {
            _requirements.Add(req);
        }

        protected virtual void TakeDamage()
        { }

        protected virtual void Awake()
        {
            _anim = GetComponent<Animator>();
        }

        protected virtual void Start()
        {
            _health = Info.MaxHealth;
            if (Info.CharacterAnimator != null)
            {
                _anim.runtimeAnimatorController = Info.CharacterAnimator;
            }
            transform.localScale = Vector3.one * Info.Scale;
        }

        protected float OneOne(float x)
        {
            if (x < 0f) return -1f;
            if (x > 0f) return 1f;
            return 0f;
        }

        protected void Shoot(Vector2 direction, bool targetEnemy, int eventSoundParameter)
        {
            ShootingManager.Instance.Shoot(direction, targetEnemy, _info.AttackType, transform.position, eventSoundParameter);
        }

        protected bool _canTakeDamage = true;

        public void TakeDamage(int amount)
        {
            if (!_canTakeDamage)
            {
                return;
            }
            _health -= amount;

            if (_health <= 0)
            {
                foreach (var r in _requirements)
                {
                    r.Unlock(this);
                }
                if (HealthBar != null)
                {
                    HealthBar.localScale = Vector3.zero;
                    if (PlayerManager.Instance.Follower.gameObject.activeInHierarchy)
                    {
                        StaticData.CharacterEndSprite = PlayerManager.Instance.Follower.Info.BombImage;
                    }
                    else
                    {
                        StaticData.CharacterEndSprite = ResourcesManager.Instance.SoloSprite;
                    }
                    SceneManager.LoadScene("Ending");
                }
                Die();
                Destroy(gameObject);
            }
            else
            {
                if (HealthBar != null)
                {
                    HealthBar.localScale = new(_health / (float)Info.MaxHealth, 1f, 1f);
                }
                TakeDamage();
            }
        }

        protected virtual void Die()
        { }
    }
}