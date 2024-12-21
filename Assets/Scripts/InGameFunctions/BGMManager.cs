using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    /* 名前と音を登録するためのインナークラス */
    [System.Serializable]
    public class BGMData
    {
        public string name; // 音の名前
        public AudioClip clip; // 音
    }

    /* 別名(name)で音を再生するためのDictionary */
    private Dictionary<string, BGMData> bgmDictionary = new Dictionary<string, BGMData>();

    /* オーディオソース */
    private AudioSource audioSource;

    [SerializeField] BGMData[] bgmDatas; // 音の名前と音を登録する配列
    void Awake()
    {
        audioSource = GetComponent<AudioSource>(); // AudioSourceを取得

        /* bgmDictionaryに名前と音を登録 */
        foreach(BGMData bgmData in bgmDatas)
        {
            bgmDictionary.Add(bgmData.name, bgmData);
        }
        
    }

    /* 名前から音を再生する */
    public bool Play(string name)
    {
        /* 名前に対応するBGMDataを取得 */
        BGMData bgmData;
        if(bgmDictionary.TryGetValue(name, out bgmData)) // 名前に対応するBGMDataがある場合
        {
            Debug.Log(name + "を再生");
            Debug.Log(bgmData.clip);
            bool isSuccess = Play(bgmData.clip); // BGMDataに登録されている音を再生
            return isSuccess;
        }
        else
        {
            Debug.LogWarning(name + "という名前のBGMは登録されていません");
            return false;
        }
    }

    /* クリップからBGMを再生する */
    public bool Play(AudioClip clip)
    {
        if(audioSource != null) // audioSourceの取得に成功している場合
        {
            audioSource.clip = clip;
            audioSource.loop = true; // ループ再生
            audioSource.Play();
            return true;
        }
        else
        {
            Debug.LogWarning("AudioSourceが取得できていません");
            return false;
        }
    }
}
