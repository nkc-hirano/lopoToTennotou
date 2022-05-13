using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using SceneController;

public class EndingAnimation : MonoBehaviour
{
    [SerializeField]
    PlayableDirector timeline; // タイムライン取得

    EndingSceneStateUpdater endingSceneStateUpdater;

    // Start is called before the first frame update
    private void Start()
    {
        timeline.Play(); // アニメション再生

        endingSceneStateUpdater = GetComponent<EndingSceneStateUpdater>();
    }

    // Update is called once per frame
    private void Update()
    {
        NextScene();
    }

    void NextScene()
    {
        // タイムラインが最後まで再生されたとき
        // 0.1は1を超えるための補正用の値
        if (timeline.time + 0.1f >= timeline.duration)
        {
            Debug.Log("タイトル画面へ");
            timeline.Stop();
            timeline.time = 0.0f;
            //endingSceneStateUpdater.LoadNextScene();
        }
    }
}
