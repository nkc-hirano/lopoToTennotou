using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using SceneController;
using UniRx;

public class IntroductionAnimation : MonoBehaviour
{
    [SerializeField]
    PlayableDirector timeline; // �^�C�����C���̎擾

    UIInputProvider uiInputProvider;
    IntroductionSceneStateUpdater introductionSceneStateUpdater;

    // �߂�����y�[�W��
    [SerializeField]
    int pageNum;

    // 1�y�[�W���߂����鎞��
    [SerializeField]
    private float turnAnimationTime;

    [SerializeField]
    private float inputSkipTime;

    // ���ܕ\������Ă���y�[�W��
    private int currentpageNum = 1;

    // ���܃|�[�Y����
    bool isPause = false;

    // ���������鎞��
    float inputTime = 0;

    // �G���^�[�L�[�������ꂽ��
    bool isRetunKeyDown = false;

    // Start is called before the first frame update
    private void Start()
    {
        timeline.Play(); // �A�j���[�V�������Đ�
        introductionSceneStateUpdater = GetComponent<IntroductionSceneStateUpdater>();
        uiInputProvider = GetComponent<UIInputProvider>();

        uiInputProvider.DecisionButtonObservable
            .Subscribe(unit =>
            {
                isRetunKeyDown = true;
            });

        uiInputProvider.DecisionButtonReleaseObservable
            .Subscribe(unit =>
            {
                isRetunKeyDown = false;
            });
    }

    // Update is called once per frame
    private void Update()
    {
        // �y�[�W�����ƂɃ^�C�����C�����ꎞ��~
        if (turnAnimationTime * currentpageNum <= timeline.time)
        {
            timeline.Pause(); // �ꎞ��~����
            isPause = true;   // �ꎞ��~��
        }
        // �y�[�W�����Ō�̃y�[�W�܂œ��B���ĂȂ��Ƃ�
        // �L�[���������Ƃ�
        if (isRetunKeyDown && pageNum >= currentpageNum)
        {
            // �ꎞ��~���Ă���Ƃ�
            if (isPause)
            {
                timeline.Play();     // �^�C�����C�����Đ�
                isPause = false;     // �ꎞ��~����
                currentpageNum += 1; // ���̃y�[�W��
            }
            else
            {
                timeline.time = turnAnimationTime * currentpageNum;
            }
            isRetunKeyDown = false;
        }
        else
        {   
            if (isRetunKeyDown) // �L�[������
            {
                // �L�[�𒷉������Ă��鎞��
                inputTime += Time.deltaTime;
            }
            else
            {
                // �L�[�𗣂����玞�Ԃ�0�ɂ���
                inputTime = 0.0f;
            }
            if (inputSkipTime <= inputTime)
            {
                // ���̃V�[����
                introductionSceneStateUpdater.LoadNextScene();
                inputTime = 0.0f;
            }
        }

        // ���̃y�[�W�����S�̂̃y�[�W���𒴂�����
        if (currentpageNum > pageNum)
        {
            timeline.Play();
        }
        // �^�C�����C�����Ō�܂ōĐ����ꂽ��
        if (timeline.time + 0.1f >= timeline.duration)
        {
            timeline.Stop();   // �^�C�����C�����~�߂�
            timeline.time = 0.0f; // �^�C�����C��������

            // ���̃V�[����
            introductionSceneStateUpdater.LoadNextScene();
        }
    }
}
