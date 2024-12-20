using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LightManager : MonoBehaviour
{
    public float battery;
    private float startMinusBattery = 5f;
    private float updateMinusBattery = 0.01f;
    private bool startBattery = true;
    private bool isCoroutine = false;
    private KeyCode lightButton = KeyCode.Space;
    private StageManager stageManager;
    public UnityEngine.Camera camera;

    //ここからしたの変数は消す。
    public TextMeshProUGUI textMeshPro;


    void Start()
    {
        camera.backgroundColor = Color.black;
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
        battery = 100f;
        
    }

    void Update()
    {
        //debug用消す
        textMeshPro.text = $"{(int)battery}";
        
        LightOn();
        
    }

    /// <summary>
    /// ifがtrueのときライトを消費
    /// ifがfalseのときライトを停止
    /// </summary>
    private void LightOn(){
        if (Input.GetKey (lightButton)){//ライト点灯
            if(startBattery){
                StartCoroutine("StartMinusBattery");//最初のバッテリーの減り
                stageManager.ChangelightStage();
                startBattery = false;
            }
            
            MinusBattery();//継続的なバッテリーの減り
            if(battery < 90){
                if(!isCoroutine){
                    isCoroutine = true;
                    StartCoroutine("LowBattery");
                }
            }
            else{
                FadeIn();
            }

        }
        else{//ライト消灯
            FadeOut();
            startBattery = true;
        }
    }



    /// <summary>
    /// fadeINする 黒→白
    /// </summary>
    void FadeIn(){
        float up = 0.05f;
        if(camera.backgroundColor != Color.white){//白になったら処理停止
            camera.backgroundColor =new Color(
            camera.backgroundColor.r + up,
            camera.backgroundColor.g + up,
            camera.backgroundColor.b + up);
        }
        
    }
    void FadeOut(){
        float up = -0.05f;
        if(camera.backgroundColor != Color.black){//黒になったら処理停止
        camera.backgroundColor =new Color(
            camera.backgroundColor.r + up,
            camera.backgroundColor.g + up,
            camera.backgroundColor.b + up);
        }
    }
    
    /// <summary>
    /// フレームあたりでバッテリーを減らしていく。
    /// </summary>
    void MinusBattery(){
        if(battery > 0){
            battery = battery - updateMinusBattery;
        }
    }

    /// <summary>
    /// 最初の0.5秒はバッテリーの減りを早くする
    /// </summary>
    /// <returns></returns>
    IEnumerator StartMinusBattery(){
    float sec = 0.5f;
    for (int i=0 ; i< 10 ; i++){
            battery = battery - (startMinusBattery/10);
            yield return new WaitForSeconds(sec/10);
        }

    }
    

    /// <summary>
    /// バッテリーが減った場合に点滅させる。
    /// </summary>
    IEnumerator LowBattery(){
        for(int i=0;i < 30;i++){
            yield return new WaitForSeconds(0.025f);
            FadeIn();
        }
        yield return new WaitForSeconds(2f);
        for(int i=0;i < 15;i++){
            yield return new WaitForSeconds(0.03f);
            FadeOut();
        }
        if(startBattery){
            
            isCoroutine = false;
            yield break;
        }
        StartCoroutine("LowBattery");
    }
}
