using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using SceneController;

public class EndingAnimation : MonoBehaviour
{
    [SerializeField]
    PlayableDirector timeline; // �^�C�����C���擾

    EndingSceneStateUpdater endingSceneStateUpdater;

    // Start is called before the first frame update
    private void Start()
    {
        timeline.Play(); // �A�j���V�����Đ�

        endingSceneStateUpdater = GetComponent<EndingSceneStateUpdater>();
    }

    // Update is called once per frame
    private void Update()
    {
        NextScene();
    }

    void NextScene()
    {
        // �^�C�����C�����Ō�܂ōĐ����ꂽ�Ƃ�
        // 0.1��1�𒴂��邽�߂̕␳�p�̒l
        if (timeline.time + 0.1f >= timeline.duration)
        {
            Debug.Log("�^�C�g����ʂ�");
            timeline.Stop();
            timeline.time = 0.0f;
            //endingSceneStateUpdater.LoadNextScene();
        }
    }
}
