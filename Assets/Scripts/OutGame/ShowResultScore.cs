using UnityEngine;
using UnityEngine.UI;

namespace OutGame
{
    public class ShowResultScore : MonoBehaviour
    {
        [SerializeField] private Text _scoreText;

        private void Start()
        {
            var score = ResultDataStore.Score;
            _scoreText.text = $"{score}";
        }
    }
}