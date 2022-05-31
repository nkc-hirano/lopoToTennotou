using System;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class RePopUpResultColl : IDisposable
{
    public float FadeTime => PopUpResultLogic.inatance.FadeTime;
    public RePopUpResultColl()
    {
        Time.timeScale = 0; // 停止
        PopUpResultLogic.inatance.CreatePopUpResultWindo();    // オブジェクトの生成
        PopUpResultLogic.inatance.PouUpSummonWind();     // ウィンド表示
    }
    public void Dispose()
    {
        DoAsyncDispose().Forget();
    }
    async private UniTask DoAsyncDispose()
    {
        await PopUpResultLogic.inatance.PouUpRepatriationWind();     // ウィンドウ非表示
        Time.timeScale = 1; // 起動
    }
}