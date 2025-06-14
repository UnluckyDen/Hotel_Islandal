using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Main.Scripts.UI.Menu
{
    public class MenuView : MonoBehaviour
    {
        private void Start()
        {
            LoadGameplayScene();
        }

        private void LoadGameplayScene()
        {
            SceneManager.LoadScene("GameplayScene");
        }
    }
}