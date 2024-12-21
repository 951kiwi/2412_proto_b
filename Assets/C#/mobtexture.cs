using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mobtexture : MonoBehaviour
{
    private mob_value data;
    private Vector3 originalScale; // ���̃X�P�[����ێ�

    void Start()
    {
        data = this.transform.parent.gameObject.GetComponent<mob_value>();
        originalScale = transform.localScale; // �����X�P�[����ۑ�
    }

    void FixedUpdate()
    {
        if (!data.rightTleftF)
        {
            // �E����
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
        }
        else
        {
            // ������
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
        }
    }
}