using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public struct AudioData
{
    public AudioMixerGroup group;   // �Ȃ̃O���[�v�@(BGM�Ȃ�BGM�̃O���[�v���̂��̂�SE�Ȃ�SE�̃O���[�v���̂��̂��g��)
    public AudioClip sound;         // ����
    public AudioSource audioSource; // ����炷�R���|�[�l���g
    public float playTime;          // �Đ����鎞��   (�����w�ǖ�����)
    public bool bgmFlg;             // BGM�Đ����邩
}