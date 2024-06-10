using System.Collections.Generic;
using UnityEngine;

public class FollowerController : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    private List<DistanceInfo> _distances = new();

    private float _totalDistance = 0f;
    private Vector2 _lastFollowerPos;

    private const float FollowOffset = 3f;

    private void Awake()
    {
        transform.position = _target.position;
        _lastFollowerPos = transform.position;
    }

    private void Update()
    {
        if (_lastFollowerPos == (Vector2)_target.position)
        {
            return; // Our target didn't move
        }
        _totalDistance += Vector2.Distance(_lastFollowerPos, (Vector2)_target.position);
        _lastFollowerPos = _target.position;
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

    private class DistanceInfo
    {
        public float Distance { set; get; }
        public Vector2 Position { set; get; }
    }
}
