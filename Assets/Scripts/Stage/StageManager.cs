/**********************************************************
 *
 *  StageManager.cs
 *  �X�e�[�W�Ǘ��N���X
 *
 *  ����� : ���X ����
 *  ����� : 2024/12/20
 *
 *********************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StageManager : MonoBehaviour
{
    // �e�I�u�W�F�N�g���Ƃ̃^�C���}�b�v
    [Header("�����X�e�[�W�I�u�W�F�N�g"),SerializeField]
    List<Tilemap> m_whiteObjects;

    [Header("�����X�e�[�W�I�u�W�F�N�g"),SerializeField]
    List<Tilemap> m_blackObjects;

    // �I�u�W�F�N�g�̃����_�[�@�\
    [SerializeField]
    List<TilemapRenderer> m_whiteObjectsRenderer;
    [SerializeField]
    List<TilemapRenderer> m_blackObjectsRenderer;

    // Start is called before the first frame update
    void Start()
    {
        //// �����_�[�@�\�擾
        //for (int i = 0; i < m_whiteObjects.Count; i++)
        //{
        //    m_whiteObjectsRenderer[i] = m_whiteObjects[i].GetComponent<TilemapRenderer>();
        //}
        //for (int i = 0; i < m_whiteObjects.Count; i++)
        //{
        //    m_blackObjectsRenderer[i] = m_blackObjects[i].GetComponent<TilemapRenderer>();
        //}
    }

    // Update is called once per frame
    void Update()
    {
        // �؂�ւ�
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ChangelightStage();
        }
    }

    public void ChangelightStage()
    {
        // �����_�[�@�\�؂�ւ�
        for (int i = 0; i < m_whiteObjects.Count; i++)
        {
            m_whiteObjectsRenderer[i].enabled = m_whiteObjectsRenderer[i].enabled ? false : true;
        }
        for (int i = 0; i < m_whiteObjects.Count; i++)
        {
            m_blackObjectsRenderer[i].enabled = m_blackObjectsRenderer[i].enabled ? false : true;
        }
    }
}
