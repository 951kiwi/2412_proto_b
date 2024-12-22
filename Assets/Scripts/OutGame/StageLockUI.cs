using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OutGame
{
    /// <summary>
    /// ステージ解放状態に応じてUI表示を変える
    /// </summary>
    public class StageLockUI : MonoBehaviour
    {
        [SerializeField, Header("ステージのUI")] private List<GameObject> _stageUI;
        private GameManager.ReachStageData _reachStageData;

        private void Start()
        {
            _reachStageData = SaveAndLoadManager.LoadData<GameManager.ReachStageData>("ReachStage");
            for (var i = 0; i < _reachStageData.reachStage + 1; i++)
            {
                _stageUI[i].GetComponent<Image>().color = Color.white;
            }
        }
    }
}