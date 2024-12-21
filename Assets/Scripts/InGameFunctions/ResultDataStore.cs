using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class ResultDataStore
{
    private static int score = 0; // 今回のスコア
    private static int[] bestScores = new int[3]; // ベストスコアを格納する配列
    // public static List<int> bestScoresList = new List<int>(); // 将来的にベストスコアも表示させる
    // Start is called before the first frame update

    public static int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
        }
    }

    public static int[] BestScores
    {
        get
        {
            return bestScores;
        }
        set
        {
            bestScores = value;
        }
    }
}
