using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class FadeFaçade : MonoBehaviour
{
    static FadeFaçade instance = null;
    bool isFadeFinish = false;

    public void Initialize(float initFadeSecond)
    {
        instance ??= this;
        FadeExecutor.FadeInInitialize(initFadeSecond);
    }

    public void FadeOut(float fadeSecond)
    {
        FadeProcess(fadeSecond).Forget();
    }

    public void FadeIn()
    {
        isFadeFinish = true;
    }

    private async UniTask FadeProcess(float fadeSecond)
    {
        using (var scope = new FadeScope(fadeSecond, false, null,
            () => isFadeFinish = true))
        {
            await UniTask.WaitUntil(() => isFadeFinish);
            Debug.Log(isFadeFinish);
        }
    }
}
