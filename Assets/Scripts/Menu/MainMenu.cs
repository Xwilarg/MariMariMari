using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TouhouPride.Menu
{
    public class MainMenu : MonoBehaviour
    {
        private void Awake()
        {
            AudioManager.instance.PlayMusic(FModReferences.instance.partnerSelect);
        }

        public void Play()
        {
            AudioManager.instance.PlayOneShot(FModReferences.instance.menuMove, transform.position);
            AudioManager.instance.ChangeMusicParameter("PartnerSelect", 1);
            SceneManager.LoadScene("PlayerSelect");
        }
    }
}