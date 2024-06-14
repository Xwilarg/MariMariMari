using UnityEngine;
using UnityEngine.SceneManagement;

namespace TouhouPride.Manager
{
    public class ResourcesManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _bullet;
        public GameObject Bullet => _bullet;

        [SerializeField]
        private GameObject _homingBullet;
        public GameObject HomingBullet => _homingBullet;

        [SerializeField] private GameObject _laser;
        public GameObject Laser => _laser;

        public static ResourcesManager Instance { private set; get; }

        private void Awake()
        {
            Instance = this;
            SceneManager.LoadScene("Map", LoadSceneMode.Additive);
        }
    }
}