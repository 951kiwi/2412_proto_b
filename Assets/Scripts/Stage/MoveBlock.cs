/**********************************************************
 *
 *  MoveBlock.cs
 *  動くブロック
 *
 *  制作者 : 髙森 煌明
 *  制作日 : 2024/12/20
 *
 *********************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlock : MonoBehaviour
{
    // 動かす軸
    private enum Direction
    {
        Up, 
        Down,
        Left,
        Right
    }

    // 移動時間
    [SerializeField] 
    private float m_moveTime = 1;
    // 移動量
    [SerializeField] 
    private float m_moveLength = 1;
    // 軸
    [SerializeField] private Direction m_direction;
    // 初期座標
    private Vector2 m_initPosition;

    /// <summary>
    /// 実行前初期化処理
    /// </summary>
    private void Awake()
    {
        m_initPosition = transform.localPosition;
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    private void Update()
    {
        // 周期の移動時間を考慮した時間を計算
        var t = 4 * m_moveLength * Time.time / m_moveTime;
        // 往復した値を計算
        float value = Mathf.PingPong(t, 2 * m_moveLength) - m_moveLength;
        // 変更座標
        Vector2 changePos = Vector2.zero;

        // 方向に応じた処理
        switch (m_direction)
        {
            case Direction.Up:
                changePos.y = value;
                break;
            case Direction.Down:
                changePos.y = -value;
                break;
            case Direction.Left:
                changePos.x = -value;
                break;
            case Direction.Right:
                changePos.x = value;
                break;
        }

        // 位置を反映
        transform.localPosition = m_initPosition + changePos;
    }
}
