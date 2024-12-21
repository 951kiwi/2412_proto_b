/**********************************************************
 *
 *  MoveBlock.cs
 *  �����u���b�N
 *
 *  ����� : ���X ����
 *  ����� : 2024/12/20
 *
 *********************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlock : MonoBehaviour
{
    // ��������
    private enum Direction
    {
        Up, 
        Down,
        Left,
        Right
    }

    // �ړ�����
    [SerializeField] 
    private float m_moveTime = 1;
    // �ړ���
    [SerializeField] 
    private float m_moveLength = 1;
    // ��
    [SerializeField] private Direction m_direction;
    // �������W
    private Vector2 m_initPosition;

    /// <summary>
    /// ���s�O����������
    /// </summary>
    private void Awake()
    {
        m_initPosition = transform.localPosition;
    }

    /// <summary>
    /// �X�V����
    /// </summary>
    private void Update()
    {
        // �����̈ړ����Ԃ��l���������Ԃ��v�Z
        var t = 4 * m_moveLength * Time.time / m_moveTime;
        // ���������l���v�Z
        float value = Mathf.PingPong(t, 2 * m_moveLength) - m_moveLength;
        // �ύX���W
        Vector2 changePos = Vector2.zero;

        // �����ɉ���������
        switch (m_direction)
        {
            case Direction.Up:
                changePos.y = value;
                break;
            case Direction.Down:
                changePos.y = -value;
                break;
            case Direction.Left:
                changePos.x = -value;
                break;
            case Direction.Right:
                changePos.x = value;
                break;
        }

        // �ʒu�𔽉f
        transform.localPosition = m_initPosition + changePos;
    }
}
