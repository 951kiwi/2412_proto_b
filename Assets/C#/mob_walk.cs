using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mob_walk : MonoBehaviour
{
    [SerializeField] private float speed = 1f; // 移動速度
    [Header("画面外でも行動する")] public bool nonVisibleAct = false;

    private bool rightTleftF = false; // 移動方向（右:true, 左:false）
    private bool isMove = false;     // 行動状態
    private bool isdead = false;
    private mob_value data;          // 壁の接触判定データ
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        data = GetComponent<mob_value>();

        // 画面外でも動作する場合の初期化
        if (nonVisibleAct)
        {
            isMove = true;
        }
    }

    void FixedUpdate()
    {
        //頭の判定処理がtrueのとき殺す
        if (data.isHead)
        {
            isdead = true;
            this.GetComponent<Collider2D>().enabled = false;
            rb.velocity = new Vector2(2f, 4f);
            Destroy(gameObject, 5f);
        }
        if (isdead)
        {
            transform.Rotate(new Vector3(0, 0, 5));
            return;
        }

        // 画面内に入るまで動きを制限
        if (!isMove)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll; // 動きを制限
            if (sr.isVisible) // 画面に表示されたとき
            {
                isMove = true;
                rb.constraints = RigidbodyConstraints2D.None;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation; // 回転を固定
            }
            return;
        }
        // 壁に接触した場合、移動方向を反転
        if (data.isWall && !isdead)
        {
            ReverseDirection();
            return;
        }

        // 移動処理
        float moveDirection = rightTleftF ? 1 : -1; // 右移動なら1、左移動なら-1
        rb.velocity = new Vector2(speed * moveDirection, rb.velocity.y);

    }

    private void ReverseDirection()
    {
        // 移動方向を反転し、スタック防止のために軽く押し戻す
        rightTleftF = !rightTleftF;
        data.rightTleftF = rightTleftF;

        // 壁から少し離すための補正
        float pushBack = rightTleftF ? 0.1f : -0.1f;
        rb.position = new Vector2(rb.position.x + pushBack, rb.position.y);
    }
}