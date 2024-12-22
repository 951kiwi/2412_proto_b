using System.Collections.Generic;
using UnityEngine;

namespace OutGame
{
    /// <summary>
    /// ADキー入力でステージを変更
    /// キー入力でそのステージシーンへ遷移
    /// 登録された数まではステージ解放
    /// 到達したステージの次まではステージ解放
    /// 
    /// </summary>
    public class SelectStage : MonoBehaviour
    {
        [SerializeField, Header("各ステージシーンの名前")]
        private List<string> _stageNames;

        [SerializeField, Header("シーン遷移キー")] private KeyCode _keyCode = KeyCode.Space;

        private int _stageIndex;
        private GameManager.ReachStageData _reachStageData;

        /// <summary> 選択中のインデックス番号 _num:0で第一ステージに該当 </summary>
        public int StageIndex => _stageIndex;

        private void Start()
        {
            _reachStageData = SaveAndLoadManager.LoadData<GameManager.ReachStageData>("ReachStage");
        }

        private void Update()
        {
            NumChange();
            if (Input.GetKeyDown(_keyCode))
            {
                SceneChanger.Instance.FadeChange(_stageNames[_stageIndex]);
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneChanger.Instance.FadeChange("Title");
            }
        }

        /// <summary>
        /// 到達済みのステージ数未満なら真
        /// 真：加算可能（次のステージを選択できる）
        /// </summary>
        /// <returns></returns>
        private bool Check()
        {
            Debug.Log($"reach : {_reachStageData.reachStage}   _stageIndex : {_stageIndex}");
            return _reachStageData.reachStage > _stageIndex;
        }

        private void NumChange()
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (_stageIndex > 0)
                {
                    _stageIndex--;
                }
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (_stageIndex < _stageNames.Count - 1 && Check())
                {
                    _stageIndex++;
                }
            }
        }
    }
}