using System;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class RePopUpStartStageNumberwindowColl : IDisposable
{
    public float FadeTime => PopUpStartStageNumberwindowLogic.inatance.FadeTime;
    public RePopUpStartStageNumberwindowColl()
    {
        Time.timeScale = 0; // 停止
        PopUpStartStageNumberwindowLogic.inatance.CreatePopUpStartStageNumberWindo();    // オブジェクトの生成
        PopUpStartStageNumberwindowLogic.inatance.PouUpSummonWind();     // ウィンド表示
    }
    public void Dispose()
    {
        DoAsyncDispose().Forget();
    }
    async private UniTask DoAsyncDispose()
    {
        await PopUpStartStageNumberwindowLogic.inatance.PouUpRepatriationWind();     // ウィンドウ非表示
        Time.timeScale = 1; // 起動
    }
}