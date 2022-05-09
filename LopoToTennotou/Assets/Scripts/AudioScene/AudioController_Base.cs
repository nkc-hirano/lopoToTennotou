using UnityEngine;
using UnityEngine.Audio;

public class AudioController_Base : MonoBehaviour
{
    [SerializeField]
    AudioClip[] sounds;         // 音源
    AudioSource audioSource;    // 音源単一操作

    int soundNum;               // 音番号
    float limitTime;            // 残り時間
    bool onFlg;

    /// <summary>
    /// コンポーネントの設定
    /// </summary>
    /// <param name="_plaNum"> 音番号 </param>
    /// <param name="group"> グループ </param>
    /// <param name="loopFlg"> ループ再生 </param>
    public AudioSource SetSource(AudioSource audioSource, int _plaNum, AudioMixerGroup group = null, bool loopFlg = false)
    {
        Debug.Log("hai");
        this.audioSource = audioSource;
        if (0 <= _plaNum && _plaNum < sounds.Length) soundNum = _plaNum;
        else
        {
            Debug.LogError("指定した曲が存在しません");
            return null;
        }
        if (group != null) audioSource.outputAudioMixerGroup = group;
        Debug.Log("音番号:" + soundNum);

        this.audioSource.clip = sounds[soundNum];
        Debug.Log("再生する曲:" + sounds[soundNum]);
        this.audioSource.loop = loopFlg;
        Debug.Log("フラグ:" + loopFlg);
        return this.audioSource;
    }

    public void AudioPlay(float playbackTime = 0, bool crossFade = false, CrossFadeData data = new CrossFadeData(), float speed = 0)
    {
        if (crossFade) CrossFade(data, speed);
        // 再生されていない場合再生
        if (audioSource.time == 0 && !onFlg) { onFlg = true; audioSource.Play(); }
        else if (audioSource.time == 0 && onFlg && audioSource.loop) { onFlg = false; audioSource.Play(); }
        // 再生時間が指定されている場合、残り時間が0になったら停止
        //if (playbackTime != 0 && LimitTime() == 0) AudioStop();
        if (playbackTime != 0 && LimitTime(playbackTime) == 0) AudioStop();
    }
    public void AudioStop()
    {
        audioSource.Stop();
    }
    public float LimitTime(float playbackTime = 0)
    {
        // 再生時間の指定がある場合
        if (playbackTime > 0) limitTime = playbackTime - Time.deltaTime;
        // 再生時間の指定がない場合
        else if (playbackTime == 0) limitTime = sounds[soundNum].length - Time.deltaTime;
        limitTime = Mathf.Min(limitTime, 0);
        return limitTime;
    }
    void CrossFade(CrossFadeData data, float num = 0f)
    {
        data.mixer.TransitionToSnapshots(data.snapshots, data.weights, num);
    }
}