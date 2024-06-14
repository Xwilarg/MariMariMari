using UnityEngine;
using UnityEngine.SceneManagement;

namespace TouhouPride.Menu
{
    public class MainMenu : MonoBehaviour
    {
        public void Play()
        {
            SceneManager.LoadScene("PlayerSelect");
        }
    }
}