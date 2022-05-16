using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public struct AudioData
{
    public AudioMixerGroup group;   // 曲のグループ　(BGMならBGMのグループ内のものをSEならSEのグループ内のものを使う)
    public AudioClip sound;         // 音源
    public AudioSource audioSource; // 音を鳴らすコンポーネント
    public float playTime;          // 再生する時間   (正直殆ど未実装)
    public bool bgmFlg;             // BGM再生するか
}