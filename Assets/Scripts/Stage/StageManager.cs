/**********************************************************
 *
 *  StageManager.cs
 *  ステージ管理クラス
 *
 *  制作者 : 髙森 煌明
 *  制作日 : 2024/12/20
 *
 *********************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StageManager : MonoBehaviour
{
    // 各オブジェクトごとのタイルマップ
    [Header("白いステージオブジェクト"),SerializeField]
    List<Tilemap> m_whiteObjects;

    [Header("黒いステージオブジェクト"),SerializeField]
    List<Tilemap> m_blackObjects;

    // オブジェクトのレンダー機能
    [SerializeField]
    List<TilemapRenderer> m_whiteObjectsRenderer;
    [SerializeField]
    List<TilemapRenderer> m_blackObjectsRenderer;

    // Start is called before the first frame update
    void Start()
    {
        //// レンダー機能取得
        //for (int i = 0; i < m_whiteObjects.Count; i++)
        //{
        //    m_whiteObjectsRenderer[i] = m_whiteObjects[i].GetComponent<TilemapRenderer>();
        //}
        //for (int i = 0; i < m_whiteObjects.Count; i++)
        //{
        //    m_blackObjectsRenderer[i] = m_blackObjects[i].GetComponent<TilemapRenderer>();
        //}
    }

    // Update is called once per frame
    void Update()
    {
        // 切り替え
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ChangelightStage();
        }
    }

    public void ChangelightStage()
    {
        // レンダー機能切り替え
        for (int i = 0; i < m_whiteObjects.Count; i++)
        {
            m_whiteObjectsRenderer[i].enabled = m_whiteObjectsRenderer[i].enabled ? false : true;
        }
        for (int i = 0; i < m_whiteObjects.Count; i++)
        {
            m_blackObjectsRenderer[i].enabled = m_blackObjectsRenderer[i].enabled ? false : true;
        }
    }
}
