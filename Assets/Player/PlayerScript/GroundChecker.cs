using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] private string[] Tags;
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
        foreach(string tag in Tags)
        {
            if(collision.gameObject.tag == tag)
            {
                player.is_grounded = false;
                player.is_jumping = true;
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        foreach(string tag in Tags)
        {
            if(collision.gameObject.tag == tag)
            {
                player.is_grounded = true;
                player.is_jumping = false;
            }
        }
    }
}
