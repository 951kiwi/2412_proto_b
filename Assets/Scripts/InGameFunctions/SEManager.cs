using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour
{
    /* AudioSourceを同時に鳴らしたい音の数だけ用意 */
    protected AudioSource[] audioSources = new AudioSource[20];

    /* 名前と音を登録するためのインナークラス */
    [System.Serializable]
    public class SEData
    {
        public string name; // 音の名前
        public AudioClip clip; // 音
    }

    /* 別名(name)で音を再生するためのDictionary */
    private Dictionary<string, SEData> seDictionary = new Dictionary<string, SEData>();

    [SerializeField] SEData[] seDatas; // 音の名前と音を登録する配列
    // Start is called before the first frame update
    void Awake()
    {
        /* AudioSourceリストの数だけAudioSourceを生成して配列に格納 */
        for(int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i] = gameObject.AddComponent<AudioSource>();
        }

        /* seDictionaryに名前と音を登録 */
        foreach(SEData seData in seDatas)
        {
            seDictionary.Add(seData.name, seData);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private AudioSource GetUnusedAudioSource()
    {
        /* 使用されていないAudioSourceを探して返す */
        for(int i = 0; i < audioSources.Length; i++)
        {
            if(!audioSources[i].isPlaying)
            {
                return audioSources[i];
            }
        }
        return null; // 使用されていないAudioSourceがない場合はnullを返す
    }

    /* 名前から音を再生する */
    public void Play(string name)
    {
        /* 名前に対応するSEDataを取得 */
        SEData seData;
        if(seDictionary.TryGetValue(name, out seData)) // 名前に対応するSEDataがある場合
        {
            Debug.Log(name + "を再生");
            Debug.Log(seData.clip);
            Play(seData.clip); // SEDataに登録されている音を再生
        }
        else
        {
            Debug.LogWarning(name + "という名前のSEは登録されていません");
        }
    }

    /* クリップから音を再生する */
    public void Play(AudioClip clip)
    {
        /* 使用されていないAudioSourceを取得 */
        AudioSource audioSource = GetUnusedAudioSource();
        if(audioSource != null) // 使用されていないAudioSourceがある場合
        {
            /* AudioSourceに音をセットして再生 */
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
