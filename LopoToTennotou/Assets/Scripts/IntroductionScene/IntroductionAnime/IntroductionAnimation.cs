using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using SceneController;

public class IntroductionAnimation : MonoBehaviour
{
    [SerializeField]
    PlayableDirector timeline; // �^�C�����C���̎擾

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

    // Start is called before the first frame update
    private void Start()
    {
        timeline.Play();
        introductionSceneStateUpdater = GetComponent<IntroductionSceneStateUpdater>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (turnAnimationTime * currentpageNum <= timeline.time)
        {
            timeline.Pause();
            isPause = true;
        }
        if (InputSpace() && pageNum >= currentpageNum)
        {
            if (isPause)
            {
                timeline.Play();
                isPause = false;
                currentpageNum += 1;
            }
            else
            {
                timeline.time = turnAnimationTime * currentpageNum;
            }
        }
        else
        {
            if(LongInputSpace())
            {
                inputTime += Time.deltaTime;
            }
            else
            {
                inputTime = 0.0f;
            }
            if(inputSkipTime <= inputTime)
            {
                introductionSceneStateUpdater.LoadNextScene();
            }
        }

        if(timeline.time + 0.1f >= timeline.duration)
        {
            timeline.Stop();
            timeline.time = 0;
            introductionSceneStateUpdater.LoadNextScene();
        }
    }

    // �f�o�b�O�p�L�[
    bool InputSpace()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
    bool LongInputSpace()
    {
        return Input.GetKey(KeyCode.Space);
    }
}
