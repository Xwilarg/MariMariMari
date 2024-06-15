using TouhouPride.Enemy.Impl;
using TouhouPride.Manager;
using TouhouPride.VN;
using UnityEngine;

namespace TouhouPride
{
    public class IntersceneManager : MonoBehaviour
    {
        [SerializeField]
        private BossEnemy _boss;

        private void Awake()
        {
            PlayerManager.Instance.Boss = _boss;
        }

        public void PlayBossStory()
        {
            VNManager.Instance.PlayBossStory();
        }
    }
}