using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TouhouPride;
using TouhouPride.Manager;
using TouhouPride.Player;
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

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _controller = GetComponent<PlayerController>();
        transform.position = _target.transform.position;

        StartCoroutine(Shoot());
    }

    private void OnEnable()
    {
        _enemies.Clear();
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
        _target.GetComponent<Follower>().SetInfo(false);
        SetInfo(true);
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

        transform.position = Vector2.Lerp(zero.Position, next.Position, me01);
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
                var closest = _enemies.OrderBy(x => Vector2.Distance(x.transform.position, transform.position)).First();
                ShootingManager.Instance.Shoot(closest.transform.position - transform.position, true, AttackType.Straight, transform.position);
                yield return new WaitForSeconds(2f);
            }
            else
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _enemies.Add(collision.GetComponent<ACharacter>());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _enemies.Remove(collision.GetComponent<ACharacter>());
    }

    private class DistanceInfo
    {
        public float Distance { set; get; }
        public Vector2 Position { set; get; }
    }
}
