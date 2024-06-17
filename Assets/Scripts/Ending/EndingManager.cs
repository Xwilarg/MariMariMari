using TouhouPride;
using TouhouPride.SO;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class EndingManager : MonoBehaviour
{
    public Image image;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioManager.instance.PlayMusic(FModReferences.instance.partnerSelect);
        image.sprite = StaticData.CharacterEndSprite;
    }
}
