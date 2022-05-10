using UnityEngine;
using UnityEngine.Audio;

public class AudioController_Base : MonoBehaviour
{
    [SerializeField]
    AudioClip[] sounds;         // ����
    AudioSource audioSource;    // �����P�ꑀ��

    int soundNum;               // ���ԍ�
    float limitTime;            // �c�莞��
    bool onFlg;

    /// <summary>
    /// �R���|�[�l���g�̐ݒ�
    /// </summary>
    /// <param name="_plaNum"> ���ԍ� </param>
    /// <param name="group"> �O���[�v </param>
    /// <param name="loopFlg"> ���[�v�Đ� </param>
    public AudioSource SetSource(AudioSource audioSource, int _plaNum, AudioMixerGroup group = null, bool loopFlg = false)
    {
        Debug.Log("hai");
        this.audioSource = audioSource;
        if (0 <= _plaNum && _plaNum < sounds.Length) soundNum = _plaNum;
        else
        {
            Debug.LogError("�w�肵���Ȃ����݂��܂���");
            return null;
        }
        if (group != null) audioSource.outputAudioMixerGroup = group;
        Debug.Log("���ԍ�:" + soundNum);

        this.audioSource.clip = sounds[soundNum];
        Debug.Log("�Đ������:" + sounds[soundNum]);
        this.audioSource.loop = loopFlg;
        Debug.Log("�t���O:" + loopFlg);
        return this.audioSource;
    }

    public void AudioPlay(float playbackTime = 0, bool crossFade = false, CrossFadeData data = new CrossFadeData(), float speed = 0)
    {
        if (crossFade) CrossFade(data, speed);
        // �Đ�����Ă��Ȃ��ꍇ�Đ�
        if (audioSource.time == 0 && !onFlg) { onFlg = true; audioSource.Play(); }
        else if (audioSource.time == 0 && onFlg && audioSource.loop) { onFlg = false; audioSource.Play(); }
        // �Đ����Ԃ��w�肳��Ă���ꍇ�A�c�莞�Ԃ�0�ɂȂ������~
        //if (playbackTime != 0 && LimitTime() == 0) AudioStop();
        if (playbackTime != 0 && LimitTime(playbackTime) == 0) AudioStop();
    }
    public void AudioStop()
    {
        audioSource.Stop();
    }
    public float LimitTime(float playbackTime = 0)
    {
        // �Đ����Ԃ̎w�肪����ꍇ
        if (playbackTime > 0) limitTime = playbackTime - Time.deltaTime;
        // �Đ����Ԃ̎w�肪�Ȃ��ꍇ
        else if (playbackTime == 0) limitTime = sounds[soundNum].length - Time.deltaTime;
        limitTime = Mathf.Min(limitTime, 0);
        return limitTime;
    }
    void CrossFade(CrossFadeData data, float num = 0f)
    {
        data.mixer.TransitionToSnapshots(data.snapshots, data.weights, num);
    }
}