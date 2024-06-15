using TouhouPride.VN;
using UnityEngine;

namespace TouhouPride
{
    public class IntersceneManager : MonoBehaviour
    {
        public void PlayBossStory()
        {
            VNManager.Instance.PlayBossStory();
        }
    }
}