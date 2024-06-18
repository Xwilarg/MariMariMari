using TouhouPride.SO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TouhouPride.Menu
{
    public class LevelSelector : MonoBehaviour
    {
        public void LoadCharacter(PlayerInfo info)
        {
            // play sound effect
            AudioManager.instance.PlayOneShot(FModReferences.instance.menuMove, transform.position);
            
            // stop music here
            AudioManager.instance.StopMusic();
            
            StaticData.CharacterName = info.Name;
            SceneManager.LoadScene("Main");
        }
    }
}