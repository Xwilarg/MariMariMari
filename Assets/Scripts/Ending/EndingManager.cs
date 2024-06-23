using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TouhouPride.Menu
{
    public class EndingManager : MonoBehaviour
    {
        public Image image;

        public Sprite perfect;

        void Start()
        {
            AudioManager.instance.StopMusic();
            AudioManager.instance.PlayMusic(FModReferences.instance.ending);
            /*if (StaticData.IsPerfect) TODO
            {
                image.sprite = perfect;
            }
            else
            {
            }*/
            image.sprite = StaticData.CharacterEndSprite;
        }

        public void ToTitle()
        {
            AudioManager.instance.StopMusic();
            SceneManager.LoadScene("Menu");
        }
    }
}
