using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Canvas表示とゲームマネージャーの2つの責任を持ってしまっている気がする? */
public class GameManager : MonoBehaviour
{
    [Serializable]
    struct ReachStageData
    {
        public int reachStage; // ステージクリア状況
    }

    [Serializable]
    struct BestScoresData
    {
        public List<List<int>> bestScores; // ベストスコア
    }
    public float testScore; // テスト用スコア
    public int testStage; // テスト用ステージ番号

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

    private const string REACHSTAGEKEY = "ReachStage"; // ステージクリア状況のキー
    private const string BESTSCOREKEY = "BestScores"; // ベストスコアのキー
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

        /* ゲームオーバー状態またはポーズ状態でエンターキーが押されたらゲームを再スタートする */
        if((isGameOver || isPaused) && Input.GetKeyDown(KeyCode.Return))
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
        int stageScore = CalcScore(lightManager.getBattery()); // バッテリー残量からスコアを計算する

        UpdateReachStage(stageNum); // ステージクリア状況を更新する
        UpdateBestScores(stageNum, stageScore); // ベストスコアを更新する
        
        /* ゲームクリアのUIを表示する */
        /* ここでゲームクリアのUIを表示する処理を呼び出す */ /* あとで修正 */
    }

    void UpdateReachStage(int stageNum)
    {
        /* ステージクリア状況を読み込む */
        ReachStageData reachStageData = SaveAndLoadManager.LoadData<ReachStageData>(REACHSTAGEKEY);

        int reachStage = reachStageData.reachStage; // ステージクリア状況を取得
        Debug.Log("reachStage: " + reachStage); // ステージクリア状況を表示
        /* 現在のステージがステージクリア状況よりも大きい場合 */
        if(stageNum > reachStage)
        {
            /* ステージクリア状況を更新する */
            reachStageData.reachStage = stageNum;
            SaveAndLoadManager.SaveData<ReachStageData>(REACHSTAGEKEY, reachStageData); // ステージクリア状況を保存する
            Debug.Log(stageNum);
        }
    }

    void UpdateBestScores(int stageNum, int score)
    {
        /* ベストスコアを読み込む */
        BestScoresData bestScores = SaveAndLoadManager.LoadData<BestScoresData>(BESTSCOREKEY);
        List<int> bestScoresList = bestScores.bestScores[stageNum - 1]; // 現在のステージのベストスコアを取得 0-indexed

        if(bestScoresList == null) // ベストスコアが存在しない場合
        {
            bestScoresList = new List<int>(); // ベストスコアを新しく作成する
        }

        bestScoresList.Add(score); // ベストスコアに現在のスコア(バッテリー残量に依存)を追加
        bestScoresList.Reverse(); // ベストスコアを降順に並び替える

        /* ベストスコアの数が上限を超えている場合 */
        if(bestScoresList.Count > BESTSCORELIMIT)
        {
            bestScoresList.RemoveAt(bestScoresList.Count - 1); // ベストスコアの最後の要素(ランク範囲外)を削除
        }

        /* ベストスコアを保存する */
        bestScores.bestScores[stageNum - 1] = bestScoresList; // 元データを更新 0-indexed
        SaveAndLoadManager.SaveData<BestScoresData>(BESTSCOREKEY, bestScores);
    }

    int CalcScore(float battery)
    {
        /* スコアを計算する処理 */
        int score = (int)(battery * 100); // バッテリー残量*100を整数にしてスコアとする
        return score;
    }

    public void TestReachStage()
    {
        int stageNum = testStage; // テスト用ステージ番号
        UpdateReachStage(stageNum); // ステージクリア状況を更新する
        var key = REACHSTAGEKEY;
        Debug.Log("Test2: " + key); // キーを表示
        int loadData = SaveAndLoadManager.LoadData<int>(REACHSTAGEKEY); // ステージクリア状況を読み込む
        Debug.Log("Test: " + loadData); // ステージクリア状況を表示
    }

    public void TestBestScores()
    {
        int stageNum = 1;
        int score = CalcScore(testScore); // スコアを計算する
        UpdateBestScores(stageNum, score); // ベストスコアを更新する

        string key = BESTSCOREKEY + stageNum;
        List<int> bestScores = SaveAndLoadManager.LoadData<List<int>>(key); // ベストスコアを読み込む
        Debug.Log(String.Join(",", bestScores)); // ベストスコアを表示
    }
}
