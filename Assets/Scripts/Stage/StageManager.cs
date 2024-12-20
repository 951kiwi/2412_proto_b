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

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        Color camera = new Color(Mathf.Sin(Time.time), 0.0f, 0.0f, 1.0f);
        ChangelightStage(camera);

        // �؂�ւ�
        if (Input.GetKeyDown(KeyCode.Z))
        {
            
        }
    }

    public void ChangelightStage(Color color)
    {
        // �����x
        float a = color.r;
        // �����_�[�@�\�؂�ւ�
        for (int i = 0; i < m_whiteObjects.Count; i++)
        {
            m_whiteObjects[i].color = new Color(1.0f,1.0f,1.0f,a);
        }
        for (int i = 0; i < m_blackObjects.Count; i++)
        {
            m_blackObjects[i].color = new Color(1.0f, 1.0f, 1.0f, 1.0f - a);
        }
    }
}
