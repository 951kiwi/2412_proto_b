using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mobtexture : MonoBehaviour
{
    private mob_value data;
    private Vector3 originalScale; // 元のスケールを保持

    void Start()
    {
        data = this.transform.parent.gameObject.GetComponent<mob_value>();
        originalScale = transform.localScale; // 初期スケールを保存
    }

    void FixedUpdate()
    {
        if (!data.rightTleftF)
        {
            // 右向き
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
        }
        else
        {
            // 左向き
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
        }
    }
}