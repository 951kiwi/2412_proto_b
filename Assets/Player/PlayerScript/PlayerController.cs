using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] bool isControllable = true;
    private Rigidbody2D rb;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    bool was_grounded;
    [SerializeField] public bool is_grounded;
    private float groundTime;
    public bool hit_Wall;
    public bool is_jumping;
    Vector3 move = Vector2.zero;
    Vector3 velocity = Vector2.zero;
    public Vector3 lastLandPos = Vector3.zero;
    private Vector3 temp_lastLandPos = Vector3.zero;

    [SerializeField] private Animator anim;

    [SerializeField] private float RespawnHeight = -10f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isControllable)
        {
            return;
        }

        int input_horizontal = 0;
        bool input_jump = false;
        if(Input.GetKey(KeyCode.A))
        {
            input_horizontal -= 1;
        }
        if(Input.GetKey(KeyCode.D))
        {
            input_horizontal += 1;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            input_jump = true;
        }

        if(Input.GetKey(KeyCode.LeftShift))
        {
            LightSwitch(true);
        }
        else LightSwitch(false);

        if(input_horizontal != 0)//Move
        {
            move.x = input_horizontal * speed;
            if(hit_Wall) move.x = 0;
            this.transform.localScale = new Vector3(input_horizontal, 1, 1);
        }
        else move.x = 0;

        move.x = input_horizontal * speed;

        if(input_jump && is_grounded) Jump();//Jump
        else move.y = rb.velocity.y;

        rb.velocity = move;
        SaveLastLandPos();

        if(this.transform.position.y < RespawnHeight)//Respawn
        {
            this.transform.position = lastLandPos;
        }
    }

    void Jump()
    {
        move.y = jumpForce;
        is_jumping = true;
        is_grounded = false;
    }

    public void LightSwitch(bool isLighted)
    {
        anim.SetBool("isLighted", isLighted);
    }

    void SaveLastLandPos()
    {
        if(!was_grounded && is_grounded)
        {
            groundTime = 0f;
            was_grounded = true;
            temp_lastLandPos = this.transform.position;
        }
        else if(is_grounded)
        {
            groundTime += Time.deltaTime;
            temp_lastLandPos = Vector3.Lerp(transform.position, temp_lastLandPos, 0.5f * Time.deltaTime);
            if(groundTime > 1f)
            {
                lastLandPos = temp_lastLandPos;
            }
        }
        else if(was_grounded && !is_grounded)
        {
            groundTime = 0f;
            was_grounded = false;
        }
    }
}
