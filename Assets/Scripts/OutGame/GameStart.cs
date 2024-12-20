using UnityEngine;

namespace OutGame
{
    /// <summary>
    /// エンターキー入力でステージ選択シーンへ
    /// </summary>
    public class GameStart : MonoBehaviour
    {
        [SerializeField, Header("ステージ選択シーンの名前")]
        private string _sceneName;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneChanger.Instance.Change(_sceneName);
            }
        }
    }
}