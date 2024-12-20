using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Transform player;

    private bool isGameOver = false; // ゲームオーバーかどうか
    private bool isPaused = false; // ゲームが一時停止されているかどうか
    private string nowLoadingSceneName; // 現在読み込んでいるシーンの名前

    private const int SCENESUBSTRING = 5; // シーン名からステージ番号を取り出すために、文字をカットする定数
    /* もし1ステージに複数のシーンがある場合は、どこかからステージ番号を持ってくる必要がある */

    private const int BESTSCORELIMIT = 3; // ベストスコアの最大数
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
        // Vector3 targetPos = PlayerController.LastLand(); // 最後の接地点を受け取る /* あとで修正 */
        // player.position = targetPos; // プレイヤーを最後の接地点に戻す
        
        /* プレイヤーのHPを減らす */
        /* ここでプレイヤーのHPを減らす処理を呼び出す */ /* あとで修正 */
    }

    /* ゲームオーバー状態にする */
    void GameOver()
    {
        Debug.Log("ゲームオーバー");
        isGameOver = true;
        /* ゲームオーバーのUIを表示する */
        /* ここでゲームオーバーのUIを表示する処理を呼び出す */ /* あとで修正 */
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
        /* 各コンポーネントのUpdate関数を停止する */ /* あとで修正 */
    }

    /* ゲームを再開する */
    void ResumeGame()
    {
        Debug.Log("ゲームを再開");
        isPaused = false;
        
        Time.timeScale = 1f; // ゲームの時間を再開する
        /* 各コンポーネントのUpdate関数を再開する */ /* あとで修正 */
    }

    public void GameClear()
    {
        Debug.Log("ゲームクリア");

        /* 現在のステージの数値を取得 */
        int stageNum = int.Parse(nowLoadingSceneName.Substring(SCENESUBSTRING));

        /* ステージクリア状況を読み込む */
        int reachStage = SaveAndLoadManager.LoadData<int>("ReachStage");

        /* 現在のステージがステージクリア状況よりも大きい場合 */
        if(reachStage < stageNum) // シーン名の5文字目以降を取り出して数値に変換 
        {
            /* ステージクリア状況を更新する */
            SaveAndLoadManager.SaveData("ReachStage", stageNum);
        }

        /* ベストスコアを読み込む */
        List<int> bestScores = SaveAndLoadManager.LoadData<List<int>>("BestScores" + stageNum);
        bestScores.Add(0); // ベストスコアに現在のスコア(バッテリー残量)を追加 /* あとで修正 */
        bestScores.Reverse(); // ベストスコアを降順に並び替える

        /* ベストスコアの数が上限を超えている場合 */
        if(bestScores.Count > BESTSCORELIMIT)
        {
            bestScores.RemoveAt(bestScores.Count - 1); // ベストスコアの最後の要素(ランク範囲外)を削除
        }

        /* ベストスコアを保存する */
        SaveAndLoadManager.SaveData("BestScores" + stageNum, bestScores);

        /* ゲームクリアのUIを表示する */
        /* ここでゲームクリアのUIを表示する処理を呼び出す */ /* あとで修正 */
    }
}
