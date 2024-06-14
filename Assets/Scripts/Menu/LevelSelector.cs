using TouhouPride.SO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TouhouPride.Menu
{
    public class LevelSelector : MonoBehaviour
    {
        public void LoadCharacter(PlayerInfo info)
        {
            StaticData.CharacterName = info.Name;
            SceneManager.LoadScene("Main");
        }
    }
}