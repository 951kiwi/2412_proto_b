using System.Collections.Generic;
using UnityEngine;

namespace OutGame
{
    /// <summary>
    /// 対象のオブジェクトのX軸を基準に移動する
    /// </summary>
    public class MoveObject : MonoBehaviour
    {
        [SerializeField] private SelectStage _selectStage;
        [SerializeField, Header("動く位置")] private List<GameObject> _movePoints;
        private Vector3 _pos;
        private int _currentNum;

        private void Start()
        {
            _pos = transform.position;
        }

        private void Update()
        {
            if (_currentNum != _selectStage.StageIndex)
            {
                _currentNum = _selectStage.StageIndex;
                _pos.x = _movePoints[_selectStage.StageIndex].transform.position.x;
                transform.position = _pos;
            }
        }
    }
}