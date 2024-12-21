using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mob : MonoBehaviour
{
    [SerializeField] private float jump = 5f;
    [SerializeField] private float speed = 1f;
    [Header("接触判定")] public EnemyCollisionCheck checkCollision;
    [Header("画面外でも行動する")] public bool nonVisibleAct = false;
    [SerializeField] bool rightTleftF = false;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameObject.Find("G_Collider") == collision.gameObject){
            Destroy(this.gameObject);
        }   
        if (checkCollision.isOn)
        {
            rightTleftF = !rightTleftF;
        }

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
