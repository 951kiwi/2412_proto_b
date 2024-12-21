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
    // 各色のタイルマップ
    [SerializeField]
    List<Tilemap> m_whiteTiles;
    [SerializeField]
    List<Tilemap> m_blackTiles;

    // 各色のオブジェクト
    [SerializeField]
    List<GameObject> m_whiteObjects;
    [SerializeField]
    List<GameObject> m_blackObjects;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        Color camera = new Color(Mathf.Sin(Time.time), 0.0f, 0.0f, 1.0f);
        ChangelightStage(camera);
    }

    public void ChangelightStage(Color color)
    {
        // 透明度
        float a = color.r;
        // レンダー機能切り替え
        for (int i = 0; i < m_whiteTiles.Count; i++)
        {
            m_whiteTiles[i].color = new Color(1.0f,1.0f,1.0f,1.0f - a);
        }
        for (int i = 0; i < m_blackTiles.Count; i++)
        {
            m_blackTiles[i].color = new Color(0f, 0f, 0f, a);
        }

        // レンダー機能切り替え
        for (int i = 0; i < m_whiteObjects.Count; i++)
        {
            m_whiteObjects[i].GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f - a);
        }
        for (int i = 0; i < m_blackObjects.Count; i++)
        {
            m_blackObjects[i].GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, a);
        }
    }
}
