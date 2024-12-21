using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mob : MonoBehaviour
{
    private Rigidbody2D rb;
    public float beforePosY = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(beforePosY == this.transform.position.y){
            Vector2 vector2= new Vector2(-1f,5f);
            rb.AddForce(vector2,ForceMode2D.Impulse);
        }
        else{
            beforePosY = this.transform.position.y;
        }
    }
}
