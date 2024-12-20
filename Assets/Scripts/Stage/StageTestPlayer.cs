using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTestPlayer : MonoBehaviour
{
    [Header("�W�����v��"), SerializeField]
    private float m_moveSpeed;

    [Header("�W�����v��"),SerializeField] 
    private float m_jumpPower;

    [SerializeField]
    private Rigidbody2D m_rigidbody;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // �ړ�
        m_rigidbody.velocity = new Vector2(Input.GetAxis("Horizontal") * m_moveSpeed, m_rigidbody.velocity.y);

        // �W�����v
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_rigidbody.AddForce(transform.up * m_jumpPower, ForceMode2D.Impulse);
        }
    }
}
