using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ステージクリア状況、ベストスコアを保存するのに使うクラス */
[Serializable] // シングルトンにするために必要
public static class SaveAndLoadManager
{

    public static void SaveData<T>(string key, T value)
    {
        /* ステージクリア状況、ベストスコアを保存する処理 */
        Debug.Log("saveKey: " + key);
        Debug.Log("saveValue: " + value);
        if(value == null)
        {
            Debug.Log("value is null");
        }
        var json = JsonUtility.ToJson(value); // 値をシリアライズ
        Debug.Log("savejson: " + json);
        PlayerPrefs.SetString(key, json); // キーと値を保存

        PlayerPrefs.Save(); // 変更を保存
        /* デバッグ用 */
        /*
        if(PlayerPrefs.HasKey(key))
        {
            Debug.Log("hasKey2: " + key);
            var json2 = PlayerPrefs.GetString(key);
            T data = JsonUtility.FromJson<T>(json2);
            int data2 = JsonUtility.FromJson<int>(json2);

            Debug.Log("data: " + data);
            Debug.Log("data2: " + data2);
        }
        */
    }

    public static T LoadData<T>(string key) where T : new()
    {
        /* ステージクリア状況、ベストスコアを読み込む処理 */
        if(PlayerPrefs.HasKey(key)) // キーが存在するかどうか
        {
            Debug.Log("hasKey: " + key);
            var json = PlayerPrefs.GetString(key); // キーに対応する値を読み込む
            Debug.Log("json: " + json);
            return JsonUtility.FromJson<T>(json); // 値をデシリアライズして返す
        }
        else
        {
            Debug.Log("elseを通過");
            return new T(); // キーが存在しない場合はデフォルト値を返す
        }
    }
}
