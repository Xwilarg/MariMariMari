using UnityEngine;
using UnityEngine.UI;

namespace TouhouPride.Menu
{
    public class EndScreen : MonoBehaviour
    {
        [SerializeField]
        private Image _image;

        private void Awake()
        {
            _image.sprite = StaticData.CharacterEndSprite;
        }
    }
}