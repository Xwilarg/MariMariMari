using TouhouPride.Manager;
using UnityEngine;

namespace TouhouPride
{
    public abstract class ACharacter : MonoBehaviour
    {
        protected void Shoot(Vector2 direction)
        {
            var bullet = ResourcesManager.Instance.Bullet;
        }
    }
}