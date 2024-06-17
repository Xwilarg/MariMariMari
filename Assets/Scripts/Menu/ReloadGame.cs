using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace TouhouPride.Menu
{
    public class ReloadGame : MonoBehaviour
    {
        public void Reload(InputAction.CallbackContext value)
        {
            if (value.performed)
            {
                SceneManager.LoadScene("Main");
            }
        }
    }
}