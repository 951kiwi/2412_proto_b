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
            _stageUI[0].GetComponent<Image>().color = Color.white;

            for (var i = 1; i <= _reachStageData.reachStage; i++)
            {
                _stageUI[i].GetComponent<Image>().color = Color.white;
            }
        }
    }
}