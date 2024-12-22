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
        [SerializeField] private Fade _fade;
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
            _fade.FadeOut();
            yield return new WaitForSecondsRealtime(0.8f); // Time.timescaleが0の時にも正常に動作するようにRealtimeを使用
            SceneManager.LoadScene(sceneName);
        }
    }
}