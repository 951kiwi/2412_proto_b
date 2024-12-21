/**********************************************************
 *
 *  MoveBlock.cs
 *  “®‚­ƒuƒƒbƒN
 *
 *  §ìÒ : ûüX àŠ–¾
 *  §ì“ú : 2024/12/20
 *
 *********************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlock : MonoBehaviour
{
    [SerializeField]
    private int m_speed;
    [SerializeField]
    private Vector2 m_startPos;
    [SerializeField]
    private float m_length;
    [SerializeField]
    private Rigidbody2D m_rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        m_startPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float posX = m_startPos.x + Mathf.PingPong(Time.time * m_speed, m_length);
        m_rigidBody.MovePosition(new Vector3(posX, m_startPos.y));
    }
}
