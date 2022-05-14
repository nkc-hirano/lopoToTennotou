using System;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class RePopUpObjColl : IDisposable
{
    public float FadeTime => PopUpObjctLogic.inatance.FadeTime;
    public RePopUpObjColl(GameObject popUpObj)
    {
        Time.timeScale = 0; // 停止
        PopUpObjctLogic.inatance.CreatePopUpObj();    // 
        PopUpObjctLogic.inatance.SetChild(popUpObj);    // 子オブジェクトに設定
        PopUpObjctLogic.inatance.PouUpSummonMove();  // ポップアップ表示
    }
    public void Dispose()
    {
        DoAsyncDispose().Forget();
    }
    async private UniTask DoAsyncDispose()
    {
        await PopUpObjctLogic.inatance.PouUpRepatriationMove();     // ウィンドウ非表示
        Time.timeScale = 1; // 起動
    }
}