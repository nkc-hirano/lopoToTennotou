using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CrossFadeController : MonoBehaviour
{
    public static CrossFadeController instance;
    [SerializeField]
    AudioMixer mixer;                   // �I�[�f�B�I�~�L�T�[
    [SerializeField]
    AudioMixerSnapshot[] snapshots;     // ���ʂ̏��(���͑�����͂�?)
    [SerializeField]
    float fadeTime = 5;                 // �؂�ւ��I���܂ł̎���
    [SerializeField]
    float[] weights = { 1.0f, 0.0f };  // 1�����Ă�A0�����Ă��Ȃ�(���͑�����͂��A�X�i�b�v�V���b�g�̐��ƈꏏ)
    private void Awake()
    {
        instance = this;
    }

    // �N���X�t�F�[�h���s��(���g�p��)
    public void CrossFade() 
    {
        // �X�i�b�v�V���b�g�̐؂�ւ�
        weights[0] = weights[0] == 0 ? 1 : 0;
        weights[1] = weights[1] == 0 ? 1 : 0;
        mixer.TransitionToSnapshots(snapshots, weights, fadeTime);
    }
}