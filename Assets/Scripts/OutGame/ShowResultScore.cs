using UnityEngine;
using UnityEngine.UI;
using System;

namespace OutGame
{
    public class ShowResultScore : MonoBehaviour
    {
        [SerializeField] private Text _scoreText;
        [SerializeField] private Text[] _bestScoerTexts;
        float Scoretime = 0;
        [SerializeField] private float ScoreShowTime = 1.0f;
        bool hasShow = false;

        int score = 0;

        private void Start()
        {
            score = ResultDataStore.Score;
            _scoreText.text = $"{0}";
            Scoretime = 0;
            ShowBests();
        }

        private void Update()
        {
            if (hasShow) return;
            Scoretime += Time.deltaTime;
            _scoreText.text = $"{(int)(score * (Scoretime / ScoreShowTime))}";
            if (Scoretime >= ScoreShowTime)
            {
                _scoreText.text = $"{score}";
                hasShow = true;
            }
        }

        private void ShowBests()
        {
            var bestScores = ResultDataStore.BestScores;
            for (var i = 0; i < bestScores.Length; i++)
            {
                /* データなしなら-----を表示 ありならそのデータを表示*/
                if(bestScores[i] == -1)
                {
                    _bestScoerTexts[i].text = String.Format(_bestScoerTexts[i].text, "-----");
                }
                else
                {
                    _bestScoerTexts[i].text = String.Format(_bestScoerTexts[i].text, bestScores[i]);
                }
            }
        }
    }
}