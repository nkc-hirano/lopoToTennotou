using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using SceneController;
using UniRx;

public class IntroductionAnimation : MonoBehaviour
{
    [SerializeField]
    FadeFaçade fadeFaçade;

    [SerializeField]
    PlayableDirector timeline; // タイムラインの取得

    UIInputProvider uiInputProvider;
    IntroductionSceneStateUpdater introductionSceneStateUpdater;

    // めくられるページ数
    [SerializeField]
    int pageNum;

    // 1ページがめくられる時間
    [SerializeField]
    private float turnAnimationTime;

    [SerializeField]
    private float inputSkipTime;

    // いま表示されているページ数
    private int currentpageNum = 1;

    // いまポーズ中か
    bool isPause = false;

    // 長押しする時間
    float inputTime = 0;

    // エンターキーが押されたか
    bool isRetunKeyDown = false;

    // エンターキーから離れたか
    bool isRetunKeyUp = false;

    // Start is called before the first frame update
    private void Start()
    {
        timeline.Play(); // アニメーションを再生
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
                isRetunKeyUp = true;
            });
        fadeFaçade.FadeIn();
    }

    // Update is called once per frame
    private void Update()
    {
        Debug.Log(isRetunKeyDown);
        // ページ数ごとにタイムラインを一時停止
        if (turnAnimationTime * currentpageNum <= timeline.time)
        {
            timeline.Pause(); // 一時停止する
            isPause = true;   // 一時停止中
        }
        // ページ数が最後のページまで到達してないとき
        // キーを押したとき
        if (isRetunKeyDown == true && pageNum >= currentpageNum)
        {
            // 一時停止しているとき
            if (isPause)
            {
                timeline.Play();     // タイムラインを再生
                isPause = false;     // 一時停止解除
                currentpageNum += 1; // 次のページへ
            }
            else
            {
                timeline.time = turnAnimationTime * currentpageNum;
            }
            isRetunKeyDown = false;
        }
        else
        {
            if(isRetunKeyDown) // キー長押し
            {
                // キーを長押ししている時間
                inputTime += Time.deltaTime;
            }
            else
            {
                // キーを離したら時間を0にする
                inputTime = 0.0f;
            }
            if(inputSkipTime <= inputTime)
            {
                // 次のシーンへ
                introductionSceneStateUpdater.LoadNextScene();
            }
        }

        // タイムラインが最後まで再生されたら
        if(timeline.time + 0.1f >= timeline.duration)
        {
            timeline.Stop();   // タイムラインを止める
            timeline.time = 0.0f; // タイムライン初期化

            // 次のシーンへ
            introductionSceneStateUpdater.LoadNextScene();
        }
    }
}
