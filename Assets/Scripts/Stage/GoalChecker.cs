/**********************************************************
 *
 *  GoalChacker.cs
 *  �S�[���̔���
 *
 *  ����� : ���X ����
 *  ����� : 2024/12/21
 *
 *********************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalChecker : MonoBehaviour
{
    // �Q�[���}�l�[�W���[
    [SerializeField] 
    private GameManager m_gameManager;

    /// <summary>
    /// �S�[���ƃv���C���[�Ƃ̓����蔻��
    /// </summary>
    /// <param name="collision">�����蔻��</param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        // �v���C���[�Ɠ���������
        if (collision.gameObject.tag == "Player")
        {
            // �N���A
            m_gameManager.GameClear();
        }
        
    }
}
