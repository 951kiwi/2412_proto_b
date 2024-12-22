using UnityEngine;
using UnityEngine.UI;
using System;

namespace OutGame
{
    public class ShowResultScore : MonoBehaviour
    {
        [SerializeField] private Text _scoreText;
        [SerializeField] private Text[] _bestScoerTexts;

        private void Start()
        {
            var score = ResultDataStore.Score;
            _scoreText.text = $"{score}";

            var bestScores = ResultDataStore.BestScores;
            for (var i = 0; i < bestScores.Length; i++)
            {
                /* データなしなら-----を表示 ありならそのデータを表示*/
                if(bestScores[i] == -1)
                {
                    _bestScoerTexts[i].text = String.Format("{0}", "-----");
                }
                else
                {
                    _bestScoerTexts[i].text = String.Format(_bestScoerTexts[i].text, bestScores[i]);
                }
            }
        }
    }
}