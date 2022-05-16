using UnityEngine;
using UnityEngine.Audio;

public class AudioLogic 
{
    public AudioMixer mixer;        // �I�[�f�B�I�~�L�T�[
    public bool stopFlg = false;    // �~�܂��Ă��邩�̃t���O

    float[] volumeBox;              // ����

    float playNowTime;              // ���݂̍Đ�����
    float limitTime;                // �c��̍Đ�����

    // ���ʂ̕ύX
    public void MixerVol(int[] _volumeBox) 
    {
        // ���ʂ̍ő�ŏ������A���Z�b�g
        volumeBox = new float[3] { 20, 20, 20 };

        // �v�Z���������ő��
        for (int i = 0; i < _volumeBox.Length; i++)
        {
            volumeBox[i] += _volumeBox[2 - i] * 10;
        }
        // ���ʐݒ�
        mixer.SetFloat("MasterVol", volumeBox[0]);
        mixer.SetFloat("BGMVol", volumeBox[1]);
        mixer.SetFloat("SEVol", volumeBox[2]);
    }

    // AudioSource�̐ݒ�
    public AudioSource SetSource(AudioData data) 
    {
        // ����
        playNowTime = data.playTime;

        // �T�E���h
        if (data.sound)
            data.audioSource.clip = data.sound;

        // �O���[�v
        if (data.group != null)
            data.audioSource.outputAudioMixerGroup = data.group;

        // BGM
        LooPlayShot(data);
        data.audioSource.loop = data.bgmFlg;

        return data.audioSource;
    }

    // BGM�Đ�
    public void LooPlayShot(AudioData data) 
    {
        if (data.bgmFlg)
            data.audioSource.Play();
    }

    // SE�Đ�
    public void OnePlayShot(AudioData data) 
    {
        if (!data.bgmFlg)
            data.audioSource.PlayOneShot(data.sound);
    }

    // �P�Ȃ�Đ�
    public void AudioPlay(AudioData data)
    {
        data.audioSource.Play();
    }

    // �c��̍Đ�����
    public float LimitTime()
    {
        // ���Ԃ�0�ȏ�Ȃ�t���[���^�C���Ŏ��Ԃ�����
        if (playNowTime > 0) playNowTime -= Time.deltaTime;
        // �������l�����~�b�g�^�C���Ƃ��āA���Ԃ̒��߂���
        limitTime = playNowTime;
        limitTime = Mathf.Min(limitTime, 0);
        return limitTime;
    }
    // �����Ƃ߂�
    public void AudioStop(AudioData data)
    {
        // �Đ����Ԃ��o�^����Ă���̂ƃ��~�b�g���Ԃ�0��ԂȂ�X�g�b�v�t���O�𗧂Ă�
        if (data.playTime != 0 && LimitTime() == 0)
            stopFlg = true;
        // �t���O���o���Ă鎞�̂ݎ~�߂�
        if (stopFlg)
            data.audioSource.Stop();
    }
}