using UnityEngine;

public class FollowerController : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    private void Awake()
    {
        transform.position = _target.position;
    }
}
