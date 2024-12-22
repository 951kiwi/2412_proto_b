using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace InGameUI
{
    /// <summary>
    /// バッテリー残量に応じて表示する物を変える
    /// 画像と数値
    /// </summary>
    public class ChangeBatteryUI : MonoBehaviour
    {
        [SerializeField, Header("残量(数値)")] private Text _text;
        [SerializeField, Header("残量(画像)")] private GameObject _batteryObj;
        [SerializeField, Header("バッテリー画像s")] private List<Sprite> _sprites;
        [SerializeField] private ShakeUI _shakeUI;
        [SerializeField, Header("拡大率")] private float _sizeUp = 3f;
        [SerializeField, Header("拡大にかかる時間")] private float _duration = 1f;
        [SerializeField] private bool _canCall; // 一度だけ実行
        private LightManager _lightManager;
        private Image _image;
        private int _batteryValue;

        private void Start()
        {
            _lightManager = FindObjectOfType<LightManager>();
            _image = _batteryObj.GetComponent<Image>();
        }

        private void Update()
        {
            ChangeImage();
            _text.text = $"{_batteryValue}";
        }

        private void ScaleChangeLoop()
        {
            if (!_canCall) return; // 一度だけ呼ぶ
            _text.transform.DOScale(_sizeUp, _duration) // 拡大
                .SetLoops(-1, LoopType.Yoyo) // 無限ループでYoyo（行ったり来たり）
                .SetEase(Ease.InOutSine); // 滑らかなイージング
            _canCall = false;
        }

        private void ChangeImage()
        {
            _batteryValue = (int)Math.Ceiling(_lightManager.getBatteryRate() * 100f);
            if (_batteryValue <= 0)
            {
                _image.sprite = _sprites[0];
            }
            else if (_batteryValue <= 15)
            {
                _image.sprite = _sprites[1];
                if (_shakeUI) _shakeUI.Shake();
                _text.color = Color.red;
                ScaleChangeLoop();
            }
            else if (_batteryValue <= 30)
            {
                _image.sprite = _sprites[2];
                _text.color = Color.yellow;
            }
            else if (_batteryValue <= 50)
            {
                _image.sprite = _sprites[3];
            }
            else if (_batteryValue <= 70)
            {
                _image.sprite = _sprites[4];
            }
            else if (_batteryValue <= 90)
            {
                _image.sprite = _sprites[5];
            }
            else if (_batteryValue <= 100)
            {
                _image.sprite = _sprites[6];
            }
        }
    }
}