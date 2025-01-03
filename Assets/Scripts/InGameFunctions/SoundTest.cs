using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTest : MonoBehaviour
{
    [SerializeField] SEManager seManager;
    [SerializeField] BGMManager bgmManager;

    // Start is called before the first frame update
    void Start()
    {
        bgmManager.Play("InGameBGM1");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            seManager.Play("Damage");
        }
        if(Input.GetKeyDown(KeyCode.B))
        {
            seManager.Play("Pinch");
        }
    }
}
