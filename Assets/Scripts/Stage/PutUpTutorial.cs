/**********************************************************
 *
 *  PutUpTutorial.cs
 *  チュートリアルの表示の処理
 *
 *  制作者 : 髙森 煌明
 *  制作日 : 2024/12/21
 *
 *********************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutUpTutorial : MonoBehaviour
{
    // キャンバス
    [SerializeField] 
    private Canvas m_canvas;
    // フェードの時間
    [SerializeField] 
    private float fadeDuration = 0.5f;
    // 上に動く距離
    [SerializeField] 
    private float moveDistance = 0.01f;

    // Canvasのフェード用
    private CanvasGroup canvasGroup;
    // フェード用コルーチン
    private Coroutine fadeCoroutine;
    // 初期座標
    private Vector3 initialPosition;     

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        // CanvasGroupを取得
        canvasGroup = m_canvas.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = m_canvas.gameObject.AddComponent<CanvasGroup>();
        }

        // 初期設定
        canvasGroup.alpha = 0;                  // 非表示状態
        m_canvas.enabled = false;               // 無効化
        initialPosition = m_canvas.transform.position; // 初期位置
    }

    /// <summary>
    /// プレイヤーが当たったら表示
    /// </summary>
    /// <param name="collision">当たり判定</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // フェードイン + 上移動
            StartFade(1, true);
        }
    }

    /// <summary>
    /// プレイヤーが当たったら消す
    /// </summary>
    /// <param name="collision">当たり判定</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // フェードアウト + 下移動
            StartFade(0, false);
        }
    }

    /// <summary>
    /// フェード
    /// </summary>
    /// <param name="targetAlpha">最終的な透明度</param>
    /// <param name="moveUp">移動</param>
    private void StartFade(float targetAlpha, bool moveUp)
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(FadeCanvas(targetAlpha, moveUp));
    }

    /// <summary>
    /// 表示コールチン
    /// </summary>
    /// <param name="targetAlpha">最終的な透明度</param>
    /// <param name="moveUp">移動</param>
    /// <returns></returns>
    private System.Collections.IEnumerator FadeCanvas(float targetAlpha, bool moveUp)
    {
        // 表示を有効にする
        m_canvas.enabled = true; 

        // 初期設定
        float startAlpha = canvasGroup.alpha;                  // 開始時の透明度
        Vector3 startPosition = m_canvas.transform.position;   // 開始時の位置
        float timer = 0f;　　　　　　　　　　　　　　　　　　　// 時間
        // 引数に応じて移動方向を変更
        Vector3 targetPosition = moveUp
            ? initialPosition + Vector3.up * moveDistance      // 上移動
            : initialPosition;                                 // 下移動
        

        // 終るまでループ
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float progress = timer / fadeDuration;

            // 透明度の補間
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, progress);

            // 位置の補間
            m_canvas.transform.position = Vector3.Lerp(startPosition, targetPosition, progress);

            // 位置フレーム待機
            yield return null;
        }

        // 最終状態を設定
        canvasGroup.alpha = targetAlpha;
        m_canvas.transform.position = targetPosition;

        // 完全に透明になったら無効化
        if (targetAlpha == 0)
        {
            m_canvas.enabled = false;
        }
    }
}
