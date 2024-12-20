using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LightManager : MonoBehaviour
{
    private float battery; //バッテリー変数
    
    private float startMinusBattery = 5f; //ライト点灯した時の最初の加速減り
    private float updateMinusBattery = 0.01f; //ライト点灯時の継続の減り
    private float lowBatteryPercent = 90f; //バッテリーが残り少ない時の点滅
    private KeyCode lightButton = KeyCode.Space; //ライト点灯用ボタン

    private bool startBattery = true; //LightOn()で一回だけ使用するための変数
    private bool isCoroutine = false; //コルーチンが存在するかの確認
    private StageManager stageManager; //ステージマネージャー
    public UnityEngine.Camera camera; //カメラ取得(背景色変更用)

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
        textMeshPro.text = $"{getBattery()}";
        
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

    public int getBattery(){
        if(battery < 0){
            battery = 0;
        }
        return (int)battery;
    }
}
