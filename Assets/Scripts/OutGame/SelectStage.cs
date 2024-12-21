using System.Collections.Generic;
using UnityEngine;

namespace OutGame
{
    /// <summary>
    /// ADキー入力でステージを変更
    /// Enterキー入力でそのステージシーンへ遷移
    /// </summary>
    public class SelectStage : MonoBehaviour
    {
        [SerializeField, Header("各ステージシーンの名前")]
        private List<string> _stageNames;

        [SerializeField, Header("シーン遷移キー")] private KeyCode _keyCode = KeyCode.Space;

        private int _num;

        /// <summary> 選択中のインデックス番号 _num:0で第一ステージに該当 </summary>
        public int Num => _num;

        private void Update()
        {
            NumChange();
            if (Input.GetKeyDown(_keyCode))
            {
                SceneChanger.Instance.FadeChange(_stageNames[_num]);
            }
        }

        private void NumChange()
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (_num > 0) _num--;
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (_num < _stageNames.Count - 1) _num++;
            }
        }
    }
}