using UnityEngine;

namespace TouhouPride
{
    public static class StaticData
    {
        public static string CharacterName { set; get; } = "Reimu";

        public static Sprite CharacterEndSprite { set; get; } = Resources.Load<Sprite>("Sprite Assets");
        //public static Sprite CharacterEndSprite { set; get; }
    }
}