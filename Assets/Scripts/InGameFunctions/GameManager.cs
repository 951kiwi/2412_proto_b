using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using OutGame;
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
        public Dictionary<int, List<int>> bestScores; // ベストスコア
    }
    public float testScore; // テスト用スコア
    public int testStage; // テスト用ステージ番号

    private Transform player; // プレイヤーの位置情報
    [SerializeField] GameObject gameOverUI; // ゲームオーバーのUI
    [SerializeField] GameObject pauseUI; // 一時停止のUI

    /* 止めたり再生したりする必要があるコンポーネント */
    private PlayerController playerController;
    [SerializeField] LightManager lightManager;

    /* シーンチェンジで使うコンポーネント */
    [SerializeField] SceneChanger sceneChanger;

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
    void Awake() // ゲーム開始時に呼び出される処理
    {
        nowLoadingSceneName = SceneManager.GetActiveScene().name; // 現在読み込んでいるシーンの名前を取得

        InitUI(); // UI初期化
        Debug.Log("pause: " + isPaused);
        Time.timeScale = 1f; // ポーズからリスタートしたときに、ゲームの時間を再開する

        /* 必要なコンポーネントを取得 */
        player = GameObject.Find("Player").transform;
        playerController = player.GetComponent<PlayerController>(); // プレイヤーコントローラーを取得
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
        /* 音を鳴らす */ /* あとで修正 */
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
        int stageNum; // 宣言
        /* 現在のステージの数値を取得 */
        try
        {
            stageNum = int.Parse(nowLoadingSceneName.Substring(SCENESUBSTRING)); // シーン名からステージ番号を取得
        }
        catch(System.Exception e) // シーン名からステージ番号を取得できなかった場合
        {
            Debug.Log("ステージ番号取得エラー");
            Debug.LogWarning(e); // エラーを表示
            stageNum = 0; // ステージ番号を0にする
        }
        
        int stageScore = CalcScore(lightManager.getBattery()); // バッテリー残量からスコアを計算する

        /* 一旦データ保存は保留
        UpdateReachStage(stageNum); // ステージクリア状況を更新する
        UpdateBestScores(stageNum, stageScore); // ベストスコアを更新する
        */

        ResultDataStore.Score = stageScore; // リザルトシーンのためにスコアを保存する
        Debug.Log("スコア: " + stageScore); // スコアを表示
        
        /* ここでゲームクリアのUIを表示する処理を呼び出す */ /* あとで修正 */
        
        /* リザルトシーンに飛ばす */
        sceneChanger.FadeChange("Result");
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
            Debug.Log("reachStageData: " + reachStageData.reachStage); // ステージクリア状況を表示
            SaveAndLoadManager.SaveData<ReachStageData>(REACHSTAGEKEY, reachStageData); // ステージクリア状況を保存する
            Debug.Log(stageNum);
        }
    }

    void UpdateBestScores(int stageNum, int score)
    {
        /* ベストスコアを読み込む */
        BestScoresData bestScoresData = SaveAndLoadManager.LoadData<BestScoresData>(BESTSCOREKEY);

        /* bestScoresDataがnullの場合 */
        /*
        if(bestScoresData.bestScores)
        {
            bestScoresData = new BestScoresData(); // ベストスコアを新しく作成する
        }
        */

        Debug.Log(bestScoresData);
        Debug.Log("突破");

        /* ベストスコアのDictionaryがnullの場合 */
        if(bestScoresData.bestScores == null)
        {
            Debug.Log("dictCreate");
            bestScoresData.bestScores = new Dictionary<int, List<int>>(); // ベストスコアDicを新しく作成する
        }

        /* 現在のステージのベストスコアが登録されていない場合 */
        if(!bestScoresData.bestScores.ContainsKey(stageNum))
        {
            bestScoresData.bestScores.Add(stageNum, new List<int>()); // ベストスコアを新しく作成する
        }

        Debug.Log(bestScoresData.bestScores[stageNum]);
        Debug.Log("突破2");

        List<int> bestScoresList; // ベストスコアを新しく作成する

        /* 現在のステージのベストスコアを取得 */
        try
        {
            bestScoresList = bestScoresData.bestScores[stageNum]; // 現在のステージのベストスコアを取得 0-indexed
        }
        catch(System.Exception e) // 現在のステージのベストスコアが存在しない場合
        {
            Debug.Log("ベストスコア取得エラー");
            Debug.LogWarning(e); // エラーを表示
            List<int> ints = new List<int>();
            bestScoresData.bestScores[stageNum] = ints; // 元データにベストスコアを新しく作成する
            // bestScoresData.bestScores[stageNum - 1].Add(); // 元データにベストスコアを新しく作成する
            bestScoresList = new List<int>(); // 更新用データにベストスコアを新しく作成する
        }
        
        bestScoresList.Add(score); // ベストスコアに現在のスコア(バッテリー残量に依存)を追加
        bestScoresList.Reverse(); // ベストスコアを降順に並び替える

        /* ベストスコアの数が上限を超えている場合 */
        if(bestScoresList.Count > BESTSCORELIMIT)
        {
            bestScoresList.RemoveAt(bestScoresList.Count - 1); // ベストスコアの最後の要素(ランク範囲外)を削除
        }

        /* ベストスコアを保存する */
        bestScoresData.bestScores[stageNum] = bestScoresList; // 元データを更新 0-indexed
        SaveAndLoadManager.SaveData<BestScoresData>(BESTSCOREKEY, bestScoresData);

        Debug.Log(bestScoresData);
        Debug.Log(bestScoresData.bestScores);
        Debug.Log(bestScoresData.bestScores[stageNum]);
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
        ReachStageData loadData = SaveAndLoadManager.LoadData<ReachStageData>(REACHSTAGEKEY); // ステージクリア状況を読み込む
        Debug.Log("Test: " + loadData.reachStage); // ステージクリア状況を表示
    }

    public void TestBestScores()
    {
        int stageNum = testStage;
        int score = CalcScore(testScore); // スコアを計算する
        UpdateBestScores(stageNum, score); // ベストスコアを更新する

        BestScoresData bestScoresData = SaveAndLoadManager.LoadData<BestScoresData>(BESTSCOREKEY); // ベストスコアを読み込む
        Debug.Log(bestScoresData);
        Debug.Log(bestScoresData.bestScores);
        Debug.Log(bestScoresData.bestScores[stageNum]);

        if(bestScoresData.bestScores.ContainsKey(stageNum))
        {
            Debug.Log(String.Join(",", bestScoresData.bestScores[stageNum])); // ベストスコアを表示
        }
        else
        {
            Debug.Log("ベストスコアが存在しません");
        }
    }

    public void TestDeleteAll()
    {
        PlayerPrefs.DeleteAll(); // 全てのデータを削除する
    }
}
