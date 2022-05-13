using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using SceneController;

public class TitleAnimation : MonoBehaviour
{
    enum TitleStatus
    {
        BeforeTitleAnimation,
        TitleAnimation,
    };
    TitleStatus titleStatus;

    TitleSceneStateUpdater titleSceneStateUpdater;

    [SerializeField]
    PlayableDirector beforeTitleTimeline; // �^�C�����C���̎擾

    [SerializeField]
    PlayableDirector titleTimeline;

    private double skipTime;

    private void Start()
    {
        titleStatus = TitleStatus.BeforeTitleAnimation;

        // �^�C�g����ʑO�̃A�j���[�V�������Đ�
        beforeTitleTimeline.Play();

        titleSceneStateUpdater = GetComponent<TitleSceneStateUpdater>();
    }

    // Update is called once per frame
    private void Update()
    {
        switch (titleStatus)
        {
            case TitleStatus.BeforeTitleAnimation:
                SkipAnimation();
                break;
            case TitleStatus.TitleAnimation:
                TitleScreen();
                break;
            default:
                break;
        }

        // �^�C�g����ʑO�̃A�j���[�V����
        // �^�C�����C�����Ō�܂ōĐ����ꂽ�Ƃ��@0.1��1�𒴂��邽�߂̕␳�p�̒l
        bool isTitleIn = beforeTitleTimeline.time + 0.1f >= beforeTitleTimeline.duration;
        if (isTitleIn)
        {
            beforeTitleTimeline.Stop();      // �^�C�����C�����~�߂�
            beforeTitleTimeline.time = 0.0f; // �^�C�����C����0�ɂ���
            titleStatus = TitleStatus.TitleAnimation;
            //Debug.Log("���̃^�C�����C����");
            titleTimeline.Play();            // �^�C�g����ʂ̃A�j���[�V�������Đ�
        }
    }

    void SkipAnimation()
    {
        // �S�̂̃^�C�����猻�݂̃^�C�����Ђ��Ďc�莞�Ԃ��o��
        skipTime = beforeTitleTimeline.duration - beforeTitleTimeline.time;

        // �X�L�b�v�ł��邩�ǂ���
        if (InputSpace())
        {
            // ���݂̃^�C���Ɏc��̃^�C�������Z����
            beforeTitleTimeline.time = beforeTitleTimeline.time + skipTime;
        }
    }

    void TitleScreen()
    {
        // �^�C�g����ʂ̃^�C�����C�����J��Ԃ��Đ�����
        bool isTitleAniLoop = titleTimeline.time + 0.1f >= titleTimeline.duration;
        if (isTitleAniLoop)
        {
            //Debug.Log("�J��Ԃ��Đ�");
            titleTimeline.time = 0; // �^�C�����C�����ŏ�����ɂ���
            titleTimeline.Play();   // �^�C�����C���Đ�
        }

        if(InputSpace())
        {
            titleSceneStateUpdater.LoadNextScene();
        }
    }

    // �f�o�b�O�p�L�[
    bool InputSpace()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
}
