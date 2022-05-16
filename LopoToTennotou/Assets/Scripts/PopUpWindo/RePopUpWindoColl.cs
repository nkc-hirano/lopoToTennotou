using System;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class RePopUpWindoColl : IDisposable
{
    public float FadeTime => PopUpWindoLogic.inatance.FadeTime;
    public RePopUpWindoColl(GameObject popUpObj)
    {
        Time.timeScale = 0; // 停止
        PopUpWindoLogic.inatance.CreatePopUpWindo();    // オブジェクトの生成
        PopUpWindoLogic.inatance.SetChild(popUpObj);    // 子オブジェクトに設定
        PopUpWindoLogic.inatance.PouUpSummonWind();     // ウィンド表示
    }
    public void Dispose()
    {
        DoAsyncDispose().Forget();
    }
    async private UniTask DoAsyncDispose()
    {
        await PopUpWindoLogic.inatance.PouUpRepatriationWind();     // ウィンドウ非表示
        Time.timeScale = 1; // 起動
    }
}