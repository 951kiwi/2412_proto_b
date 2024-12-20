using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FB_Checker : MonoBehaviour
{
    [SerializeField] PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            player.hit_Wall = false;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            player.hit_Wall = true;
        }
    }
}
