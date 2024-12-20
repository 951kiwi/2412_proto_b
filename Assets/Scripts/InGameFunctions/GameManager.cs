using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Transform player;

    private bool isGameOver = false; // ゲームオーバーかどうか
    private bool isPaused = false; // ゲームが一時停止されているかどうか
    private string nowLoadingSceneName; // 現在読み込んでいるシーンの名前
    // Start is called before the first frame update
    void Start()
    {
        nowLoadingSceneName = SceneManager.GetActiveScene().name; // 現在読み込んでいるシーンの名前を取得
    }

    // Update is called once per frame
    void Update()
    {
        /* まだゲームオーバーでないかつ、バッテリー残量が0以下になったらゲームオーバー状態にする */
        if(!isGameOver /*&& /* バッテリー残量が0以下になったら */)
        {
            GameOver();
        }

        /* ゲームオーバー状態でスペースキーが押されたらゲームを再スタートする */
        if(isGameOver && Input.GetKeyDown(KeyCode.Space))
        {
            RestartGame();
        }

        /* Escキーが押されたら */
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            /* ゲームが一時停止されているかどうかで処理を分岐 */
            if(isPaused)
            {
                /* ゲームが一時停止されている場合、ゲームを再開する */
                ResumeGame();
            }
            else
            {
                /* ゲームが一時停止されていない場合、ゲームを一時停止する */
                PauseGame();
            }
        }
    }

    /* 穴に落ちた時の処理 */
    void FallInHole()
    {
        Debug.Log("穴に落ちた");
        /* プレイヤーを最後の接地点に戻す */
        // player.position = PlayerController.lastGroundPos; // 最後の接地点を受け取り、プレイヤーの位置をそこに戻す
        
        /* プレイヤーのHPを減らす */
        /* ここでプレイヤーのHPを減らす処理を呼び出す */
    }

    /* ゲームオーバー状態にする */
    void GameOver()
    {
        Debug.Log("ゲームオーバー");
        isGameOver = true;
        /* ゲームオーバーのUIを表示する */
        /* ここでゲームオーバーのUIを表示する処理を呼び出す */
    }

    /* ゲームを再スタートする */
    void RestartGame()
    {
        Debug.Log("ゲームを再スタート");
        SceneManager.LoadScene(nowLoadingSceneName); // 現在読み込んでいるシーンを再読み込みする
    }

    /* ゲームを一時停止する */
    void PauseGame()
    {
        Debug.Log("ゲームを一時停止");
        isPaused = true;
        
        Time.timeScale = 0; // ゲームの時間を停止する
        /* 各コンポーネントのUpdate関数を停止する */
    }

    /* ゲームを再開する */
    void ResumeGame()
    {
        Debug.Log("ゲームを再開");
        isPaused = false;
        
        Time.timeScale = 1f; // ゲームの時間を再開する
        /* 各コンポーネントのUpdate関数を再開する */
    }
}
