/**********************************************************
 *
 *  GoalChacker.cs
 *  ゴールの判定
 *
 *  制作者 : 髙森 煌明
 *  制作日 : 2024/12/21
 *
 *********************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalChecker : MonoBehaviour
{
    // ゲームマネージャー
    [SerializeField] 
    private GameManager m_gameManager;

    /// <summary>
    /// ゴールとプレイヤーとの当たり判定
    /// </summary>
    /// <param name="collision">当たり判定</param>
    void OnTriggerStay2D(Collider2D collision)
    {
        // プレイヤーと当たったら
        if (collision.gameObject.tag == "Player")
        {
            // クリア
            m_gameManager.GameClear();
        }
        
    }
}
