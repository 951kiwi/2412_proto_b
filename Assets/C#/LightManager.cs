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
    private float lowBatteryPercent = 0f; //バッテリーが残り少ない時の点滅
    [SerializeField] private KeyCode lightButton = KeyCode.LeftShift; //ライト点灯用ボタン

    private bool startBattery = true; //LightOn()で一回だけ使用するための変数
    private bool isCoroutine = false; //コルーチンが存在するかの確認
    private StageManager stageManager; //ステージマネージャー
    private PlayerController playerController;
    public UnityEngine.Camera camera; //カメラ取得(背景色変更用)



    void Start()
    {
        camera.backgroundColor = Color.black;
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        battery = 100f;
        
    }

    void Update()
    {
        LightOn();
        stageManager.ChangelightStage(camera.backgroundColor);
        
    }

    /// <summary>
    /// ifがtrueのときライトを消費
    /// ifがfalseのときライトを停止
    /// </summary>
    private void LightOn(){
        if (Input.GetKey (lightButton)){//ライト点灯
            if(startBattery){
                StartCoroutine("StartMinusBattery");//最初のバッテリーの減り
                startBattery = false;
            }
            
            MinusBattery();//継続的なバッテリーの減り
            if(battery < lowBatteryPercent){
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
        playerController.LightChanger(camera.backgroundColor.r);

    }



    /// <summary>
    /// fadeINする 黒→白
    /// </summary>


    void FadeIn(){
        float up = 2.5f * Time.deltaTime;
        if(camera.backgroundColor.r < 1){//白になったら処理停止
            camera.backgroundColor =new Color(
            camera.backgroundColor.r + up,
            camera.backgroundColor.g + up,
            camera.backgroundColor.b + up);
        }
        else{
            camera.backgroundColor = new Color(1,1,1);
        }
        
    }
    void FadeOut(){
        float up = -2.5f * Time.deltaTime;
        if(camera.backgroundColor.r > 0){//黒になったら処理停止
        camera.backgroundColor =new Color(
            camera.backgroundColor.r + up,
            camera.backgroundColor.g + up,
            camera.backgroundColor.b + up);
        }
        else{
            camera.backgroundColor =new Color(0,0,0);
        }
        
    }

    

    
    
    /// <summary>
    /// フレームあたりでバッテリーを減らしていく。
    /// </summary>
    void MinusBattery(){
        if(battery > 0){
            battery = battery - updateMinusBattery;
        }
        else{
            battery = 0;
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
        for(int i=0;i < 60;i++){
            yield return new WaitForSeconds(0f);
            FadeIn();
            FadeIn();
        }
        yield return new WaitForSeconds(2f);
        for(int i=0;i < 30;i++){
            yield return new WaitForSeconds(0.03f);
            FadeOut();
            FadeOut();
            FadeOut();
            FadeOut();
        }
        if(startBattery){
            
            isCoroutine = false;
            yield break;
        }
        StartCoroutine("LowBattery");
    }

    /// <summary>
    /// バッテリーにダメージを与える。
    /// </summary>
    /// <param name="damage">ダメージ数</param>
    public void DoDamageBattery(float damage){
        if(battery > 0){
            battery = battery - damage;
        }
        else{
            battery = 0;
        }
    }
    
    /// <summary>
    /// バッテリー状態を取得する
    /// </summary>
    /// <returns>battery</returns>
    public float getBattery(){
        if(battery < 0){
            battery = 0;
        }
        return battery;
    }
}
