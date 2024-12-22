using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] private string[] GroundTags;

    [SerializeField] private string[] MoveGroundTags;
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
        foreach(string tag in GroundTags)
        {
            if(collision.gameObject.tag == tag)
            {
                player.is_grounded = false;
                player.is_jumping = true;
            }
            if(tag == "Needle")
            {
                player.isMoveGround = false;
            }
        }
        foreach(string tag in MoveGroundTags)
        {
            if(collision.gameObject.tag == tag)
            {
                player.transform.parent = null;
                player.isMoveGround = false;
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        foreach(string tag in GroundTags)
        {
            if(collision.gameObject.tag == tag)
            {
                player.is_grounded = true;
                player.is_jumping = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        foreach(string tag in MoveGroundTags)
        {
            if(collision.gameObject.tag == tag)
            {
                player.transform.parent = collision.gameObject.transform;
                player.isMoveGround = true;
            }
        }
        if(collision.gameObject.tag == "Needle")
        {
            player.isMoveGround = true;
        }
        if(collision.gameObject.tag == "EnemyHead")
        {
            player.StepEnemy();
        }
    }
}
