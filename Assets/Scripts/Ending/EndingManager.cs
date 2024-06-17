using UnityEngine;
using UnityEngine.UI;

namespace TouhouPride.Menu
{
    public class EndingManager : MonoBehaviour
    {
        public Image image;

        void Start()
        {
            AudioManager.instance.PlayMusic(FModReferences.instance.partnerSelect);
            image.sprite = StaticData.CharacterEndSprite;
        }
    }
}
