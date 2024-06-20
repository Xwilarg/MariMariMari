using UnityEngine;
using UnityEngine.UI;

namespace TouhouPride.Menu
{
    public class EndingManager : MonoBehaviour
    {
        public Image image;

        public Sprite perfect;

        void Start()
        {
            AudioManager.instance.PlayMusic(FModReferences.instance.partnerSelect);
            /*if (StaticData.IsPerfect) TODO
            {
                image.sprite = perfect;
            }
            else
            {
            }*/
            image.sprite = StaticData.CharacterEndSprite;
        }
    }
}
