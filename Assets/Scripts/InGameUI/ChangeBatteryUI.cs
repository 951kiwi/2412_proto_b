using System.Collections.Generic;
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
        [SerializeField, Header("バッテリー残量")] private Text _text;
        [SerializeField, Header("バッテリー画像")] private GameObject _batteryObj;
        [SerializeField, Header("画像")] private List<Sprite> _sprites;
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

        private void ChangeImage()
        {
            _batteryValue = _lightManager.getBattery();
            if (_batteryValue <= 0)
            {
                _image.sprite = _sprites[0];
            }
            else if (_batteryValue <= 10)
            {
                _image.sprite = _sprites[1];
            }
            else if (_batteryValue <= 30)
            {
                _image.sprite = _sprites[2];
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