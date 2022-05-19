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

    // タイトル画面前のタイムラインを取得
    [SerializeField]
    PlayableDirector beforeTitleTimeline;

    // タイトル画面のタイムラインの取得
    [SerializeField]
    PlayableDirector titleTimeline;

    // タイムラインをスキップする時間
    double skipTime;

    // エンターキーが押されたか
    bool isRetunKeyDown = false;

    float fadeTime = 0.0f;

    private void Start()
    {
        titleStatus = TitleStatus.BeforeTitleAnimation;

        // タイトル画面前のアニメーションを再生
        beforeTitleTimeline.Play();

        titleSceneStateUpdater = GetComponent<TitleSceneStateUpdater>();
        uiInputProvider = GetComponent<UIInputProvider>();

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
        // 全体のタイムから現在のタイムをひいて残り時間を出す
        skipTime = beforeTitleTimeline.duration - beforeTitleTimeline.time;

        // フェード可能な時間
        fadeTime += Time.deltaTime;

        // スキップできるかどうか
        bool isSkipFadeStart = isRetunKeyDown && fadeTime > 1.5f;
        if (isSkipFadeStart)
        {
            StartCoroutine(SkipTimeLine());
        }
    }

    void ToTitleScreen()
    {
        // タイトル画面前のアニメーション
        // タイムラインが最後まで再生されたとき　0.1は1を超えるための補正用の値
        bool isTitleIn = beforeTitleTimeline.time + 0.1f >= beforeTitleTimeline.duration;
        if (isTitleIn)
        {
            beforeTitleTimeline.Stop();      // タイムラインを止める
            beforeTitleTimeline.time = 0.0f; // タイムラインを0にする
            titleStatus = TitleStatus.TitleAnimation;
            titleTimeline.Play();            // タイトル画面のアニメーションを再生
        }
    }

    void TitleScreen()
    {
        // タイトル画面のタイムラインを繰り返し再生条件
        bool isTitleAniLoop = titleTimeline.time + 0.1f >= titleTimeline.duration;
        if (isTitleAniLoop)
        {
            titleTimeline.time = 0; // タイムラインを最初からにする
            titleTimeline.Play();   // タイムライン再生
        }

        if (isRetunKeyDown)
        {
            titleSceneStateUpdater.LoadNextScene(); // 次のシーンへ
            isRetunKeyDown = false;
        }
    }

    IEnumerator SkipTimeLine()
    {
        const float FADE_SECOND = 0.5f; // フェードアウトする時間
        using (var scope = new FadeScope(FADE_SECOND, false))
        {
            // フェードアウト中の処理
            yield return new WaitForSeconds(FADE_SECOND);

            // 真っ暗になった後の処理
            // 現在の時間に残りの再生時間を足す
            beforeTitleTimeline.time += skipTime;
            isRetunKeyDown = false;
        }
    }
}