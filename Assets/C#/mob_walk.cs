using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mob_wolk : MonoBehaviour
{
    [SerializeField] private float jump = 5f;
    [SerializeField] private float speed = 1f;
    [Header("接触判定")] public EnemyCollisionCheck checkCollision;
    [Header("接触判定あたま")] public EnemyCollisionCheck HeadcheckCollision;
    [Header("画面外でも行動する")] public bool nonVisibleAct = false;
    [SerializeField]  bool rightTleftF = false;
    bool ismove = false;
    bool isdead = false;
    private Rigidbody2D rb;
    private SpriteRenderer sr = null;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        if (nonVisibleAct)
        {
            ismove = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!ismove)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            if (sr.isVisible)
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

        }
        if (HeadcheckCollision.isHead)
        {
            isdead = true;
            this.GetComponent<Collider2D>().enabled = false;
            rb.velocity = new Vector2(2f, 4f);
            Destroy(rb, 5f);
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (checkCollision.isWall)
        {
            rightTleftF = !rightTleftF;
        }


        if (!isdead && ismove)
        {
            if (rightTleftF)
            {
                Vector2 vector2 = new Vector2(speed, jump);
                rb.velocity = vector2;
            }
            else
            {
                Vector2 vector2 = new Vector2(-speed, jump);
                rb.velocity = vector2;
            }
        }



    }

}
