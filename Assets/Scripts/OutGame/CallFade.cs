using UnityEngine;

namespace OutGame
{
    /// <summary>
    /// クリック→N秒入力停止→クリック可能
    /// </summary>
    public class CallFade : MonoBehaviour
    {
        [SerializeField] private Fade _fade;
        [SerializeField, Header("ライト点灯キー")] private KeyCode _keyCode = KeyCode.Mouse0;
        private bool _isFade;
        private bool _canFade;
        public float _inputCooldown = 2f; // N秒（入力制限時間）
        private float _cooldownTimer = 0f; // 現在のクールダウン時間
        private bool _canPressShift = true; // シフトキーを押せるかどうか

        private void Update()
        {
            // クールダウンタイマーを更新
            if (!_canPressShift)
            {
                _cooldownTimer -= Time.unscaledDeltaTime;
                if (_cooldownTimer <= 0f)
                {
                    _canPressShift = true; // 再びシフトキーを押せるようにする
                }
            }

            // シフトキーの入力処理
            if (_canPressShift && Input.GetKeyDown(_keyCode))
            {
                _canPressShift = false; // 入力を無効化
                _cooldownTimer = _inputCooldown; // クールダウンタイマーをリセット
                _isFade = !_isFade;
                if (_isFade)
                {
                    _fade.FadeIn();
                }
                else
                {
                    _fade.FadeOut();
                }
            }
        }
    }
}