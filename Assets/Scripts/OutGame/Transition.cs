using UnityEngine;

namespace OutGame
{
    /// <summary>
    /// キー入力で特定のシーンへ遷移
    /// </summary>
    public class Transition : MonoBehaviour
    {
        [SerializeField, Header("ステージ選択シーンの名前")]
        private string _sceneName;

        [SerializeField, Header("シーン遷移キー")] private KeyCode _keyCode = KeyCode.Space;

        private void Update()
        {
            if (Input.GetKeyDown(_keyCode))
            {
                SceneChanger.Instance.FadeChange(_sceneName);
            }
        }
    }
}