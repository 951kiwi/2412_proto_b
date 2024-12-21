using UnityEngine;
using UnityEngine.UI;

namespace OutGame
{
    public class ShowUnlockStage : MonoBehaviour
    {
        [SerializeField, Header("デバッグ中か")] private bool _isDebug;
        [SerializeField] private Text _text;

        private void Start()
        {
            if (_isDebug) ShowDebug();
        }

        private void ShowDebug()
        {
            GameManager.ReachStageData reachStageData =
                SaveAndLoadManager.LoadData<GameManager.ReachStageData>("ReachStage");
            var a = reachStageData.reachStage;
            _text.text = $"reach stage : {a}";
        }
    }
}