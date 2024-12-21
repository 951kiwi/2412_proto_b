using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mob : MonoBehaviour
{
    [SerializeField] private float jump = 5f;
    [SerializeField] private float speed = 1f;
    [Header("接触判定あたま")] public EnemyCollisionCheck HeadcheckCollision;
    [Header("画面外でも行動する")] public bool nonVisibleAct = false;
    public bool rightTleftF = false;
    private bool ismove = false;
    private bool isdead = false;
    private Rigidbody2D rb;
    private mob_value data;
    private SpriteRenderer sr;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        data = this.GetComponent<mob_value>();
        if (nonVisibleAct)
        {
            ismove = true;
        }
    }

    void Update()
    {
        //頭の判定処理がtrueのとき殺す
        if (data.isHead)
        {
            isdead = true;
            this.GetComponent<Collider2D>().enabled = false;
            rb.velocity = new Vector2(2f, 4f);
            Destroy(gameObject, 5f);
        }

        //動きが制限されているとき
        if (!ismove)
        {
            //すべて動き制限
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            if (sr.isVisible)//画面に写ったら
            {
                ismove = true;
                rb.constraints = RigidbodyConstraints2D.None;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                rb.AddForce(new Vector2(0, -2f));
            }
        }

        if (isdead)
        {
            transform.Rotate(new Vector3(0, 0, 5));
            return;
        }

        if (!isdead && ismove)
        {
            // 移動処理
            float moveDirection = rightTleftF ? 1 : -1;
            rb.velocity = new Vector2(speed * moveDirection, rb.velocity.y);
        }
        //生きていて、動けて、地面に付いてるとき
        if (!isdead && ismove && data.isGround)
        {
            // ジャンプ処理
            rb.velocity = new Vector2(rb.velocity.x, jump);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 壁にあたっていたら
        if (data.isWall)
        {
            rightTleftF = !rightTleftF;
            data.rightTleftF = rightTleftF;
        } 
    }
}
