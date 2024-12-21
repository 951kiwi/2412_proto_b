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
    // �e�F�̃^�C���}�b�v
    [Header("�����^�C��"),SerializeField]
    List<Tilemap> m_whiteTiles;
    [Header("�����^�C��"), SerializeField]
    List<Tilemap> m_blackTiles;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        //Color camera = new Color(Mathf.Sin(Time.time), 0.0f, 0.0f, 1.0f);
        //ChangelightStage(camera);
    }

    public void ChangelightStage(Color color)
    {
        // �����x
        float a = color.r;
        // �F�̐؂�ւ�
        for (int i = 0; i < m_whiteTiles.Count; i++)
        {
            m_whiteTiles[i].color = new Color(1.0f,1.0f,1.0f,1.0f - a);
        }
        for (int i = 0; i < m_blackTiles.Count; i++)
        {
            m_blackTiles[i].color = new Color(0f, 0f, 0f, a);
        }
    }
}
