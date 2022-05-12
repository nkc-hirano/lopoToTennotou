using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cysharp.Threading.Tasks;

public class FadeScope : IDisposable
{
    private static bool isExcuting = false;
    public static bool IsExCuting => isExcuting;
    private Action fadeInFinishAct = null;
    float fadeSecond = 0.0f;

    public FadeScope(float fadeSecond, Action fadeInFinishAct = null, Action fadeOutFinishAct = null)
    {
        isExcuting = true;
        this.fadeSecond = fadeSecond;
        this.fadeInFinishAct = fadeInFinishAct;
        FadeExecutor.FadeOut(fadeSecond, fadeOutFinishAct).Forget();
    }

    public void Dispose()
    {
        FadeExecutor.FadeIn(fadeSecond, fadeInFinishAct).Forget();
        isExcuting = false;
    }
}
