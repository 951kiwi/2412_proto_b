using UnityEngine;
using UnityEngine.SceneManagement;

namespace OutGame
{
    /// <summary>
    /// シーン遷移機能
    /// </summary>
    public class SceneChanger : MonoBehaviour
    {
        public static SceneChanger Instance;

        public void Awake()
        {
            Instance = this;
        }

        /// <summary>
        /// シーン遷移
        /// </summary>
        /// <param name="sceneName">遷移したいシーン名</param>
        public void Change(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}