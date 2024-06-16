using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TouhouPride;
using TouhouPride.Manager;
using TouhouPride.Player;
using TouhouPride.Utils;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField]
    private PlayerController _target;

    private PlayerController _controller;
    private SpriteRenderer _sr;

    private List<DistanceInfo> _distances = new();

    private float _totalDistance = 0f;
    private Vector2 _lastFollowerPos;

    private const float FollowOffset = 3f;

    private readonly List<ACharacter> _enemies = new();

    private int _targettingLayer;

    private CircleCollider2D _detectorRange;

    private Animator _anim;

    private void Awake()
    {
        // get animation component
        _anim = GetComponent<Animator>();
        
        _sr = GetComponent<SpriteRenderer>();
        _controller = GetComponent<PlayerController>();

        _targettingLayer = LayerMask.GetMask("Wall", "Enemy");

        var d = GetComponentInChildren<Detector>();
        d.OnEnter.AddListener((c) =>
        {
            if (c.CompareTag("Enemy"))
            {
                _enemies.Add(c.GetComponent<ACharacter>());
            }
        });
        d.OnExit.AddListener((c) =>
        {
            if (c.CompareTag("Enemy"))
            {
                _enemies.Remove(c.GetComponent<ACharacter>());
            }
        });
        _detectorRange = d.GetComponent<CircleCollider2D>();

        StartCoroutine(Shoot());
    }

    private void Start()
    {
        UpdateProperties();
    }

    /// <summary>
    /// This must be called on PlayerInfo change
    /// </summary>
    public void UpdateProperties()
    {
        _detectorRange.radius = _controller.Info.Range;
    }

    public void ResetFollowing()
    {
        _enemies.Clear();
        transform.position = _target.transform.position;
        _lastFollowerPos = transform.position;
        _totalDistance = 0f;
    }

    public void SetInfo(bool isFollowing)
    {
        gameObject.layer = LayerMask.NameToLayer(isFollowing ? "Follower" : "Player");
        _sr.sortingOrder = isFollowing ? -1 : 0;

        _controller.enabled = !isFollowing;
        enabled = isFollowing;
        if (!isFollowing)
        {
            _controller.TargetMe();
        }
    }

    public void Switch()
    {
        var f = _target.GetComponent<Follower>();
        f.SetInfo(false);
        f.ResetFollowing();
        SetInfo(true);
        ResetFollowing();
    }

    private void Update()
    {
        if (_target == null || _lastFollowerPos == (Vector2)_target.transform.position)
        {
            return; // Our target didn't move (or is dead)
        }
        _totalDistance += Vector2.Distance(_lastFollowerPos, (Vector2)_target.transform.position);
        _lastFollowerPos = _target.transform.position;
        _distances.Add(new() { Distance = _totalDistance, Position = _lastFollowerPos });

        var myDistance = _totalDistance - FollowOffset;
        if (myDistance < 0f)
        {
            return; // Start of the game, we wait a bit to get some distance
        }

        var zero = _distances[0];
        var next = _distances[1];

        while (myDistance > next.Distance)
        {
            _distances.RemoveAt(0);
            zero = _distances[0];
            next = _distances[1];
        }

        var me = myDistance - zero.Distance;
        var max = next.Distance - zero.Distance;
        var me01 = me / max;

        var old = transform.position;
        transform.position = Vector2.Lerp(zero.Position, next.Position, me01);
        var dir = transform.position - old;
        
        // Animation stuff; we'll just copy the target's current float values.
        _anim.SetFloat("X", OneOne(dir.x));
        _anim.SetFloat("Y", OneOne(dir.y));
        _anim.enabled = dir.magnitude != 0f;
    }
    
    private float OneOne(float x)
    {
        if (x < 0f) return -1f;
        if (x > 0f) return 1f;
        return 0f;
    }


    private IEnumerator Shoot()
    {
        while (true)
        {
            while (!enabled)
            {
                yield return new WaitForEndOfFrame();
            }
            if (_enemies.Any())
            {
                var closests = _enemies.OrderBy(x => Vector2.Distance(x.transform.position, transform.position));
                bool didShoot = false;
                foreach (var closest in closests)
                {
                    var attackDir = closest.transform.position - transform.position;
                    var hit = Physics2D.Raycast(transform.position, attackDir, 100f, _targettingLayer);
                    if (hit.collider != null && hit.collider.CompareTag("Enemy"))
                    {
                        ShootingManager.Instance.Shoot(attackDir, true, _controller.Info.AttackType, transform.position);
                        didShoot = true;
                        Debug.DrawLine(transform.position, hit.point, Color.black, 1f);
                        break;
                    }
                    else
                    {
                        Debug.DrawLine(transform.position, hit.collider == null ? (transform.position + attackDir) : hit.point, Color.blue, 1f);
                    }
                }

                yield return (didShoot ? new WaitForSeconds(_controller.Info.ReloadTime) : new WaitForEndOfFrame());
            }
            else
            {
                yield return new WaitForEndOfFrame();
            }

            _enemies.RemoveAll(x => x.gameObject == null);
        }
    }

    private class DistanceInfo
    {
        public float Distance { set; get; }
        public Vector2 Position { set; get; }
    }
}
