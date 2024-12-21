using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] KeyCode LightSwitchKey = KeyCode.LeftShift;
    [SerializeField] KeyCode JumpKey = KeyCode.Space;

    [SerializeField] private string[] DamegeTags;
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

    [SerializeField, Range(0f, 1f)] private float LightStrength = 0f;

    private float RespawnHeight = -10f;
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

        if(Input.GetKey(LightSwitchKey) || isPlayerTest)
        {
            LightChanger(LightStrength);
        }

        if(Input.GetKey(KeyCode.O) && isPlayerTest)
        {
            PlayerGameOver();
        }

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
            anim.SetBool("isJumping", false);
            if(input_jump) Jump();//Jump
        }
        anim.SetBool("isGrounded", is_grounded);

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
        anim.SetBool("isJumping", true);
        is_grounded = false;
    }

    public void LightChanger(float _lightStrength)
    {
        anim.SetFloat("LightStrength", _lightStrength);
    }

    private float lastLandLerp = 1.58f;
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

    [SerializeField] private LightManager _lightManager;
    [SerializeField] private float Damage_value = 10f;
    [SerializeField] private float DamageCoolTime = 1f;

    [SerializeField] SEManager _seManager;

    private bool isDamageValid = true;

    void OnCollisionStay2D(Collision2D collision)
    {
        if(!isDamageValid) return;
        if(!isControllable) return;
        foreach(string tag in DamegeTags)
        {
            if(collision.gameObject.tag == tag)
            {
                if(_lightManager != null) _lightManager.DoDamageBattery(Damage_value);
                if(_seManager != null) _seManager.Play("Damage");
                isDamageValid = false;
                StartCoroutine(DamageCool());
            }
        }
    }

    IEnumerator DamageCool()
    {
        anim.SetBool("isDamaged", true);
        yield return new WaitForSeconds(DamageCoolTime/2f);
        anim.SetBool("isDamaged", false);
        yield return new WaitForSeconds(DamageCoolTime/2f);
        isDamageValid = true;
    }

    public void PlayerGameOver()
    {
        isControllable = false;
        anim.SetBool("isGameOver", true);
    }
}
