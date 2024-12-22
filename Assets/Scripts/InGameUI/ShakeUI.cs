using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace InGameUI
{
    public class ShakeUI : MonoBehaviour
    {
        [SerializeField, Header("シェイクする対象")] private Image _targetImage;
        [SerializeField, Header("シェイクの時間")] private float _duration = 0.5f;
        [SerializeField, Header("シェイクの強さ")] private float _strength = 20f;
        [SerializeField, Header("振動の回数")] private int _vibrato = 10;
        [SerializeField, Header("ランダム度合い")] private float _randomness = 90f;
        [SerializeField, Header("インターバル")] private float _interval = 2f;
        [SerializeField] private bool _canShake;
        [SerializeField, Header("デバッグ中か")] private bool _isDebug;
        WaitForSecondsRealtime _wfsr;
        private RectTransform _rectTransform;
        private Tween _shakeTween;
        private Vector2 _initialPosition;

        private void Start()
        {
            _rectTransform = _targetImage.GetComponent<RectTransform>();
            _initialPosition = _targetImage.rectTransform.anchoredPosition;
            _wfsr = new WaitForSecondsRealtime(_interval);
        }

        private void Update()
        {
            if (_isDebug)
            {
                if (Input.GetKeyDown(KeyCode.S))
                {
                    Shake();
                }
            }
        }

        public void Shake()
        {
            if (!_canShake) return;
            _shakeTween = _rectTransform.DOShakeAnchorPos(_duration, _strength, _vibrato, _randomness)
                .OnComplete(() =>
                {
                    // アニメーション終了時に初期位置に戻す
                    _rectTransform.anchoredPosition = _initialPosition;
                    StartCoroutine(ControlShake());
                });
        }

        public void ShakeStop()
        {
            _shakeTween.Kill();
        }

        private IEnumerator ControlShake()
        {
            _canShake = false;
            yield return _wfsr;
            _canShake = true;
        }
    }
}