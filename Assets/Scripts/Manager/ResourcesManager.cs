using UnityEngine;

namespace TouhouPride.Manager
{
    public class ResourcesManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _bullet;
        public GameObject Bullet => _bullet;

        public static ResourcesManager Instance { private set; get; }

        private void Awake()
        {
            Instance = this;
        }
    }
}