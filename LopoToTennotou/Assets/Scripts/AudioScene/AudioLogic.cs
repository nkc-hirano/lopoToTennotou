using UnityEngine;
using UnityEngine.Audio;

public class AudioLogic 
{
    public AudioMixer mixer;        // オーディオミキサー
    public bool stopFlg = false;    // 止まっているかのフラグ

    float[] volumeBox;              // 音量

    float playNowTime;              // 現在の再生時間
    float limitTime;                // 残りの再生時間

    // 音量の変更
    public void MixerVol(int[] _volumeBox) 
    {
        // 音量の最大で初期化、リセット
        volumeBox = new float[3] { 20, 20, 20 };

        // 計算したうえで代入
        for (int i = 0; i < _volumeBox.Length; i++)
        {
            volumeBox[i] += _volumeBox[2 - i] * 10;
        }
        // 音量設定
        mixer.SetFloat("MasterVol", volumeBox[0]);
        mixer.SetFloat("BGMVol", volumeBox[1]);
        mixer.SetFloat("SEVol", volumeBox[2]);
    }

    // AudioSourceの設定
    public AudioSource SetSource(AudioData data) 
    {
        // 時間
        playNowTime = data.playTime;

        // サウンド
        if (data.sound)
            data.audioSource.clip = data.sound;

        // グループ
        if (data.group != null)
            data.audioSource.outputAudioMixerGroup = data.group;

        // BGM
        LooPlayShot(data);
        data.audioSource.loop = data.bgmFlg;

        return data.audioSource;
    }

    // BGM再生
    public void LooPlayShot(AudioData data) 
    {
        if (data.bgmFlg)
            data.audioSource.Play();
    }

    // SE再生
    public void OnePlayShot(AudioData data) 
    {
        if (!data.bgmFlg)
            data.audioSource.PlayOneShot(data.sound);
    }

    // 単なる再生
    public void AudioPlay(AudioData data)
    {
        data.audioSource.Play();
    }

    // 残りの再生時間
    public float LimitTime()
    {
        // 時間が0以上ならフレームタイムで時間を引く
        if (playNowTime > 0) playNowTime -= Time.deltaTime;
        // 引いた値をリミットタイムとして、時間の調節する
        limitTime = playNowTime;
        limitTime = Mathf.Min(limitTime, 0);
        return limitTime;
    }
    // 音をとめる
    public void AudioStop(AudioData data)
    {
        // 再生時間が登録されているのとリミット時間が0状態ならストップフラグを立てる
        if (data.playTime != 0 && LimitTime() == 0)
            stopFlg = true;
        // フラグが経ってる時のみ止める
        if (stopFlg)
            data.audioSource.Stop();
    }
}