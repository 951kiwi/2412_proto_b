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
        Head,
        Wall,
        Ground,
    }
    [HideInInspector] public bool isWall = false;
    [HideInInspector] public bool isHead = false;
    [SerializeField] private SampleEnum part;
    [SerializeField] Animator anim;
    private mob_value data;
    private string groundTag = "Ground";
    private string playerTag = "PlayerReg";
    private string enemyTag = "Enemy";


    void Start(){
        data = this.transform.parent.gameObject.GetComponent<mob_value>();
    }    
    #region//接触判定
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (part == SampleEnum.Wall)
        {
            if (collision.tag == groundTag || collision.tag == enemyTag)
            {
                data.isWall = true;
            }
        }
        else if (part == SampleEnum.Head){
            if (collision.tag == playerTag){
                data.isHead = true;
                anim.SetBool("isGrounded", true);
            }
        }
        else if(part == SampleEnum.Ground){
            if (collision.tag == groundTag){
                data.isGround = true;
                anim.SetBool("isGrounded", true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (part == SampleEnum.Wall)
        {
            if (collision.tag == groundTag || collision.tag == enemyTag)
            {
                data.isWall = false;
            }
        }
        else if (part == SampleEnum.Head){
            if (collision.tag == playerTag){
                data.isHead = false;
                anim.SetBool("isGrounded", false);
            }
        }
        else if(part == SampleEnum.Ground){
            if (collision.tag == groundTag){
                data.isGround = false;
                anim.SetBool("isGrounded", false);
            }
        }
    }
    #endregion
}