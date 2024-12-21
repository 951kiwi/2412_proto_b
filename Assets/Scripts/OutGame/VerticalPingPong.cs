using UnityEngine;

namespace OutGame
{
    /// <summary>
    /// 往復する垂直方向移動
    /// </summary>
    public class VerticalPingPong : MonoBehaviour
    {
        [SerializeField, Header("移動速度")] private float _speed = 2f;
        [SerializeField, Header("移動範囲（高さ）")] private float _height = 5f;
        private Vector3 _pos;
        private Vector3 _startPos;

        private void Start()
        {
            _startPos = transform.position;
        }


        private void Update()
        {
            var newY = Mathf.PingPong(Time.time * _speed, _height);
            _pos = transform.position;
            transform.position = new Vector3(_pos.x, _startPos.y + newY, _pos.z);
        }
    }
}