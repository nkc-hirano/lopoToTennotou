using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CrossFadeController : MonoBehaviour
{
    [SerializeField]
    AudioMixer mixer;                   // オーディオミキサー
    [SerializeField]
    AudioMixerSnapshot[] snapshots;     // 音量の状態(数は増えるはず?)
    [SerializeField]
    float fadeTime = 5;                 // 切り替え終わるまでの時間
    [SerializeField]
    float[] weights = { 1.0f, 0.0f };  // 1がついてる、0がついていない(数は増えるはず、スナップショットの数と一緒)

    // クロスフェードを行う(他使用可)
    public void CrossFade() 
    {
        // スナップショットの切り替え
        weights[0] = weights[0] == 0 ? 1 : 0;
        weights[1] = weights[1] == 0 ? 1 : 0;
        mixer.TransitionToSnapshots(snapshots, weights, fadeTime);
    }
}