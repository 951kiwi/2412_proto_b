using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Canvas表示とゲームマネージャーの2つの責任を持ってしまっている気がする? */
public class GameManager : MonoBehaviour
{
    [SerializeField] Transform player; // プレイヤーの位置情報
    [SerializeField] GameObject gameOverUI; // ゲームオーバーのUI
    [SerializeField] GameObject pauseUI; // 一時停止のUI

    /* 止めたり再生したりする必要があるコンポーネント */
    [SerializeField] PlayerController playerController;
    [SerializeField] LightManager lightManager;

    [SerializeField] float fallBorder = -10f; // 落下判定の境界値
    [SerializeField] float fallDamage = 10f; // 落下ダメージ

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

        InitUI(); // UI初期化
    }

    // Update is called once per frame
    void Update()
    {
        /* まだゲームオーバーでないかつ、バッテリー残量が0以下になったらゲームオーバー状態にする */
        if(!isGameOver && lightManager.getBattery() <= 0)
        {
            GameOver();
        }

        /* ゲームオーバー状態でエンターキーが押されたらゲームを再スタートする */
        if(isGameOver && Input.GetKeyDown(KeyCode.Return))
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

        if(player.position.y < fallBorder) // プレイヤーが落下した場合
        {
            FallInHole();
        }
    }

    /* UI初期化 */
    void InitUI()
    {
        gameOverUI.SetActive(false); // ゲームオーバーのUIを非表示にする
        pauseUI.SetActive(false); // 一時停止のUIを非表示にする
    }

    /* 穴に落ちた時の処理 */
    public void FallInHole()
    {
        Debug.Log("穴に落ちた");
        /* プレイヤーを最後の接地点に戻す */
        Vector3 targetPos = playerController.lastLandPos; // 最後の接地点を受け取る
        player.position = targetPos; // プレイヤーを最後の接地点に戻す
        
        /* プレイヤーのHPを減らす */
        lightManager.DoDamageBattery(fallDamage);
    }

    /* ゲームオーバー状態にする */
    void GameOver()
    {
        Debug.Log("ゲームオーバー");
        isGameOver = true;
        /* ゲームオーバーのUIを表示する */
        gameOverUI.SetActive(true);
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
        /* 各コンポーネントを停止させる */
        playerController.enabled = false;
        lightManager.enabled = false;

        /* 一時停止のUIを表示する */
        pauseUI.SetActive(true);
    }

    /* ゲームを再開する */
    void ResumeGame()
    {
        Debug.Log("ゲームを再開");
        isPaused = false;
        
        Time.timeScale = 1f; // ゲームの時間を再開する
        /* 各コンポーネントを再開させる */
        playerController.enabled = true;
        lightManager.enabled = true;

        /* 一時停止のUIを非表示にする */
        pauseUI.SetActive(false);
    }

    public void GameClear()
    {
        Debug.Log("ゲームクリア");
        /* 現在のステージの数値を取得 */
        int stageNum = int.Parse(nowLoadingSceneName.Substring(SCENESUBSTRING));
        int stageScore = CalcScore(); // スコアを計算する

        UpdateReachStage(stageNum); // ステージクリア状況を更新する
        UpdateBestScores(stageNum, stageScore); // ベストスコアを更新する
        
        /* ゲームクリアのUIを表示する */
        /* ここでゲームクリアのUIを表示する処理を呼び出す */ /* あとで修正 */
    }

    void UpdateReachStage(int stageNum)
    {
        /* ステージクリア状況を読み込む */
        int reachStage = SaveAndLoadManager.LoadData<int>("ReachStage");

        /* 現在のステージがステージクリア状況よりも大きい場合 */
        if(reachStage < stageNum)
        {
            /* ステージクリア状況を更新する */
            SaveAndLoadManager.SaveData("ReachStage", stageNum);
        }
    }

    void UpdateBestScores(int stageNum, int score)
    {
        /* ベストスコアを読み込む */
        List<int> bestScores = SaveAndLoadManager.LoadData<List<int>>("BestScores" + stageNum);

        bestScores.Add(score); // ベストスコアに現在のスコア(バッテリー残量に依存)を追加 /* あとで修正 */
        bestScores.Reverse(); // ベストスコアを降順に並び替える

        /* ベストスコアの数が上限を超えている場合 */
        if(bestScores.Count > BESTSCORELIMIT)
        {
            bestScores.RemoveAt(bestScores.Count - 1); // ベストスコアの最後の要素(ランク範囲外)を削除
        }

        /* ベストスコアを保存する */
        SaveAndLoadManager.SaveData("BestScores" + stageNum, bestScores);
    }

    int CalcScore()
    {
        /* スコアを計算する処理 */
        return 0; // 仮の値
    }
}
