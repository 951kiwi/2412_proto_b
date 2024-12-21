using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionCheck : MonoBehaviour
{
    /// <summary>
    /// 判定内に敵か壁がある
    /// </summary>

    public enum SampleEnum
    {
        None,
        Wall,
        Head,
    }

    [HideInInspector] public bool isWall = false;
    [HideInInspector] public bool isHead = false;
    [SerializeField] private SampleEnum part;

    private string groundTag = "Ground";
    private string playerTag = "PlayerReg";
    private string enemyTag = "Enemy";


    #region//接触判定
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (part == SampleEnum.Wall)
        {
            if (collision.tag == groundTag || collision.tag == enemyTag)
            {
                isWall = true;
            }
        }
        else if (part == SampleEnum.Head){
            if (collision.tag == playerTag){
                Debug.Log(collision.tag);
                isHead = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (part == SampleEnum.Wall)
        {
            if (collision.tag == groundTag || collision.tag == enemyTag)
            {
                isWall = false;
            }
        }
        else if (part == SampleEnum.Head){
            if (collision.tag == playerTag){
                isHead = false;
            }
        }
    }
    #endregion
}