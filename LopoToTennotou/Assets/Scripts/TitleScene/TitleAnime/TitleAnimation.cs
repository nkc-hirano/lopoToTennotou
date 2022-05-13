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
    PlayableDirector beforeTitleTimeline; // タイムラインの取得

    [SerializeField]
    PlayableDirector titleTimeline;

    private double skipTime;

    private void Start()
    {
        titleStatus = TitleStatus.BeforeTitleAnimation;

        // タイトル画面前のアニメーションを再生
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

        // タイトル画面前のアニメーション
        // タイムラインが最後まで再生されたとき　0.1は1を超えるための補正用の値
        bool isTitleIn = beforeTitleTimeline.time + 0.1f >= beforeTitleTimeline.duration;
        if (isTitleIn)
        {
            beforeTitleTimeline.Stop();      // タイムラインを止める
            beforeTitleTimeline.time = 0.0f; // タイムラインを0にする
            titleStatus = TitleStatus.TitleAnimation;
            //Debug.Log("次のタイムラインへ");
            titleTimeline.Play();            // タイトル画面のアニメーションを再生
        }
    }

    void SkipAnimation()
    {
        // 全体のタイムから現在のタイムをひいて残り時間を出す
        skipTime = beforeTitleTimeline.duration - beforeTitleTimeline.time;

        // スキップできるかどうか
        if (InputSpace())
        {
            // 現在のタイムに残りのタイムを加算する
            beforeTitleTimeline.time = beforeTitleTimeline.time + skipTime;
        }
    }

    void TitleScreen()
    {
        // タイトル画面のタイムラインを繰り返し再生条件
        bool isTitleAniLoop = titleTimeline.time + 0.1f >= titleTimeline.duration;
        if (isTitleAniLoop)
        {
            //Debug.Log("繰り返し再生");
            titleTimeline.time = 0; // タイムラインを最初からにする
            titleTimeline.Play();   // タイムライン再生
        }

        if(InputSpace())
        {
            titleSceneStateUpdater.LoadNextScene();
        }
    }

    // デバッグ用キー
    bool InputSpace()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
}
