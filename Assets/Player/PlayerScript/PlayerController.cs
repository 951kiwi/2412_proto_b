using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] KeyCode LightSwitchKey = KeyCode.LeftShift;
    [SerializeField] KeyCode JumpKey = KeyCode.Space;
    [SerializeField] bool isControllable = true;
    private Rigidbody2D rb;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    bool was_grounded;
    [SerializeField] public bool is_grounded;
    private float groundTime;
    public bool is_jumping;
    Vector3 move = Vector2.zero;
    Vector3 velocity = Vector2.zero;
    public Vector3 lastLandPos = Vector3.zero;
    private Vector3 temp_lastLandPos = Vector3.zero;
    [SerializeField] private bool isPlayerTest = false;
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

        if(Input.GetKeyDown(JumpKey))
        {
            input_jump = true;
        }

        if(Input.GetKey(LightSwitchKey))
        {
            LightSwitch(true);
        }
        else LightSwitch(false);

        if(input_horizontal != 0)//Move
        {
            move.x = input_horizontal * speed;
            this.transform.localScale = new Vector3(input_horizontal, 1, 1);
        }
        else move.x = 0;

        move.x = input_horizontal * speed;
        anim.SetFloat("Movement", Mathf.Abs(move.x));

        move.y = rb.velocity.y;
        if(is_grounded)
        {
            anim.SetBool("isJump", false);
            if(input_jump) Jump();//Jump
        }

        rb.velocity = move;
        SaveLastLandPos();

        if(this.transform.position.y < RespawnHeight && isPlayerTest)//Respawn
        {
            this.transform.position = lastLandPos;
        }
    }

    void Jump()
    {
        move.y = jumpForce;
        is_jumping = true;
        anim.SetBool("isJump", true);
        is_grounded = false;
    }

    public void LightSwitch(bool isLighted)
    {
        anim.SetBool("isLighted", isLighted);
    }

    [SerializeField] private float lastLandLerp = 1f;
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
            temp_lastLandPos = Vector3.Lerp(temp_lastLandPos, this.transform.position, lastLandLerp * Time.deltaTime);
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
