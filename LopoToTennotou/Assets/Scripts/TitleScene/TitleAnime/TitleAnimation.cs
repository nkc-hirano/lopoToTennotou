using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using SceneController;
using UniRx;

public class TitleAnimation : MonoBehaviour
{
    enum TitleStatus
    {
        BeforeTitleAnimation,
        TitleAnimation,
    };
    TitleStatus titleStatus;

    UIInputProvider uiInputProvider;
    TitleSceneStateUpdater titleSceneStateUpdater;

    // �^�C�g����ʑO�̃^�C�����C�����擾
    [SerializeField]
    PlayableDirector beforeTitleTimeline;

    // �^�C�g����ʂ̃^�C�����C���̎擾
    [SerializeField]
    PlayableDirector titleTimeline;

    // �^�C�����C�����X�L�b�v���鎞��
    double skipTime;

    // �G���^�[�L�[�������ꂽ��
    bool isRetunKeyDown = false;

    float fadeTime = 0.0f;

    private void Start()
    {
        titleStatus = TitleStatus.BeforeTitleAnimation;

        // �^�C�g����ʑO�̃A�j���[�V�������Đ�
        beforeTitleTimeline.Play();

        titleSceneStateUpdater = GetComponent<TitleSceneStateUpdater>();
        uiInputProvider = GetComponent<UIInputProvider>();

        Debug.Log(uiInputProvider);

        uiInputProvider.DecisionButtonObservable
            .Subscribe(unit =>
            {
                isRetunKeyDown = true;
            });
    }

    // Update is called once per frame
    private void Update()
    {
        ToTitleScreen();

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
    }

    void SkipAnimation()
    {
        // �S�̂̃^�C�����猻�݂̃^�C�����Ђ��Ďc�莞�Ԃ��o��
        skipTime = beforeTitleTimeline.duration - beforeTitleTimeline.time;

        // �t�F�[�h�\�Ȏ���
        fadeTime += Time.deltaTime;

        // �X�L�b�v�ł��邩�ǂ���
        bool isSkipFadeStart = isRetunKeyDown && fadeTime > 1.5f;
        if (isSkipFadeStart)
        {
            StartCoroutine(SkipTimeLine());
        }
    }

    void ToTitleScreen()
    {
        // �^�C�g����ʑO�̃A�j���[�V����
        // �^�C�����C�����Ō�܂ōĐ����ꂽ�Ƃ��@0.1��1�𒴂��邽�߂̕␳�p�̒l
        bool isTitleIn = beforeTitleTimeline.time + 0.1f >= beforeTitleTimeline.duration;
        if (isTitleIn)
        {
            beforeTitleTimeline.Stop();      // �^�C�����C�����~�߂�
            beforeTitleTimeline.time = 0.0f; // �^�C�����C����0�ɂ���
            titleStatus = TitleStatus.TitleAnimation;
            titleTimeline.Play();            // �^�C�g����ʂ̃A�j���[�V�������Đ�
        }
    }

    void TitleScreen()
    {
        // �^�C�g����ʂ̃^�C�����C�����J��Ԃ��Đ�����
        bool isTitleAniLoop = titleTimeline.time + 0.1f >= titleTimeline.duration;
        if (isTitleAniLoop)
        {
            titleTimeline.time = 0; // �^�C�����C�����ŏ�����ɂ���
            titleTimeline.Play();   // �^�C�����C���Đ�
        }

        if (isRetunKeyDown)
        {
            titleSceneStateUpdater.LoadNextScene(); // ���̃V�[����
            isRetunKeyDown = false;
        }
    }

    IEnumerator SkipTimeLine()
    {
        const float FADE_SECOND = 0.5f; // �t�F�[�h�A�E�g���鎞��
        using (var scope = new FadeScope(FADE_SECOND, false))
        {
            // �t�F�[�h�A�E�g���̏���
            yield return new WaitForSeconds(FADE_SECOND);

            // �^���ÂɂȂ�����̏���
            // ���݂̎��ԂɎc��̍Đ����Ԃ𑫂�
            beforeTitleTimeline.time += skipTime;
            isRetunKeyDown = false;
        }
    }
}