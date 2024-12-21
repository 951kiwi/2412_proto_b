using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace OutGame
{
    public class Fade : MonoBehaviour
    {
        [SerializeField, Header("パネル")] private Image _panel;
        [SerializeField, Header("かかる時間")] private float _duration = 0.5f;

        [SerializeField, Header("シーン開始時にFadeInする")]
        private bool _flag;

        [SerializeField, Header("デバッグ中か")] private bool _isDebug;

        private SEManager _seManager;

        private void Start()
        {
            if (_flag) FadeIn();
            _seManager = FindObjectOfType<SEManager>();
        }

        private void Update()
        {
            if (_isDebug)
            {
                // テスト
                if (Input.GetKeyDown(KeyCode.F))
                {
                    FadeOut();
                }
                else if (Input.GetKeyDown(KeyCode.J))
                {
                    FadeIn();
                }
            }
        }

        /// <summary>
        /// FadeIn FadeOut
        /// </summary>
        private IEnumerator FadeImage(float startAlpha, float endAlpha)
        {
            var color = _panel.color;
            var time = 0f;
            while (time < _duration)
            {
                time += Time.deltaTime;
                var alpha = Mathf.Lerp(startAlpha, endAlpha, time / _duration);
                _panel.color = new Color(color.r, color.g, color.b, alpha);
                yield return null;
            }
        }

        /// <summary>
        /// 暗くなる
        /// </summary>
        public void FadeOut()
        {
            StartCoroutine(FadeImage(0, 1));
            if (_seManager) _seManager.Play("LightOn");
        }

        /// <summary>
        /// 明るくなる
        /// </summary>
        public void FadeIn()
        {
            StartCoroutine(FadeImage(1, 0));
            if (_seManager) _seManager.Play("LightOn");
        }
    }
}