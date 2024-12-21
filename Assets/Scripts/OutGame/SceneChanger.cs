using System.Collections;
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
        private Fade _fade;

        public void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _fade = FindObjectOfType<Fade>();
        }

        /// <summary>
        /// シーン遷移
        /// </summary>
        /// <param name="sceneName">遷移したいシーン名</param>
        public void Change(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        /// <summary>
        /// fadeするシーン遷移
        /// </summary>
        /// <param name="sceneName">遷移したいシーン名</param>
        public void FadeChange(string sceneName)
        {
            StartCoroutine(FadeCoroutine(sceneName));
        }

        private IEnumerator FadeCoroutine(string sceneName)
        {
            _fade.FadeIn();
            yield return new WaitForSeconds(0.5f);
            SceneManager.LoadScene(sceneName);
        }
    }
}