using System;
using FMODUnity;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace TouhouPride.Menu
{
    public class MainMenu : MonoBehaviour
    {
        public UnityEvent<EventReference, Vector3, string, int> onClick;

        private void Start()
        {
            AudioManager.instance.PlayMusic(FModReferences.instance.partnerSelect);
        }

        private void Awake()
        {
            //AudioManager.instance.PlayMusic(FModReferences.instance.partnerSelect);
            onClick.AddListener(AudioAction);
        }

        public void Play()
        {
            onClick.Invoke(FModReferences.instance.menuMove, transform.position, "PartnerSelect", 1);
            SceneManager.LoadScene("PlayerSelect");
        }

        public void AudioAction(EventReference eventRef, Vector3 pos, String parameter, int parameterValue)
        {
            AudioManager.instance.PlayOneShot(eventRef, pos);
            AudioManager.instance.ChangeMusicParameter(parameter, parameterValue);
        }
    }
}