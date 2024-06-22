using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TouhouPride.Menu
{
    public class Settings: MonoBehaviour
    {
        public Slider masterSlider;
        public Slider musicSlider;
        public Slider sfxSlider;
        
        private void Awake()
        {
            //AudioManager.instance.StopMusic();
        }

        public void SetMasterVolume()
        {
            AudioManager.instance.SetMasterVolume(masterSlider.value);
        }
        
        public void SetMusicVolume()
        {
            AudioManager.instance.SetMusicVolume(musicSlider.value);
        }
        
        public void SetSfxVolume()
        {
            AudioManager.instance.SetSfxVolume(sfxSlider.value);
            // play a sample audio.
            AudioManager.instance.PlayOneShot(FModReferences.instance.shoot, gameObject.transform.position);
        }

        public void ReturnToTitle()
        {
            SceneManager.LoadScene("Menu");
        }
    }
}