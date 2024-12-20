using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float underLimit = -2.5f; // カメラの下限値
    [SerializeField] float leftLimit = -2.5f; // カメラの左限値
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
    }

    void MoveCamera()
    {
        Vector3 playerPos = player.position; // プレイヤーの位置を取得
        playerPos.z = -10; // カメラを手前側に引っ張るために、Z軸の位置を-10にする
        playerPos.x = Mathf.Max(playerPos.x, leftLimit); // カメラの左限値より左には行かせない
        playerPos.y = Mathf.Max(playerPos.y, underLimit); // カメラの下限値より下には行かせない

        transform.position = playerPos; // カメラの位置をプレイヤーの位置に合わせる
    }
}
