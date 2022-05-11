using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using System.Threading;
using System;

public class FadeExecutor : MonoBehaviour
{
    internal static FadeExecutor instance = null;

    [SerializeField]
    Image fadeImage;
    Color fadeImageColor = new Color();

    bool isFadeExcuteTiming = false;
    bool isFadeAlpha = true;

    float currentTime = 0.0f;

    CancellationTokenSource fadeInSource = new CancellationTokenSource();
    CancellationTokenSource fadeOutSource = new CancellationTokenSource();

    internal CancellationTokenSource FadeInSource => fadeInSource;
    internal CancellationTokenSource FadeOutSource => fadeOutSource;
    internal bool IsFadeAlpha => isFadeAlpha;
    internal bool IsFadeExcuteTiming => isFadeExcuteTiming;

    // Start is called before the first frame update
    void Start()
    {
        instance ??= this;
        fadeImageColor = fadeImage.color;
    }

    // Debug
    private async UniTask DebugFadeControll()
    {
        using (FadeScope scope = new FadeScope(1.0f))
        {
            await UniTask.Delay(3000);
        }
    }

    async internal static UniTask FadeIn(float executeSecondTime, Action finishAct = null, CancellationToken cancellationToken = default)
    {
        if (instance.isFadeExcuteTiming) { return; }
        if (instance.isFadeAlpha) { return; }

        instance.isFadeExcuteTiming = true;
        while (instance.currentTime < executeSecondTime)
        {
            instance.fadeImage.color = new Color(instance.fadeImageColor.r,
                instance.fadeImageColor.g,
                instance.fadeImageColor.b,
                1 - (instance.currentTime / executeSecondTime));
            // �K���K�v
            instance.currentTime += Time.deltaTime;
            Debug.LogFormat($"<color=cyan>[Fade]{instance.currentTime}</color>");
            cancellationToken.ThrowIfCancellationRequested();
            await UniTask.Yield();
        }
        instance.isFadeExcuteTiming = false;
        instance.isFadeAlpha = true;
        instance.currentTime = 0.0f;
        if (finishAct != null) { finishAct(); }
    }

    async internal static UniTask FadeOut(float executeSecondTime, Action finishAct = null, CancellationToken cancellationToken = default)
    {
        if (instance.isFadeExcuteTiming) { return; }
        if (!instance.isFadeAlpha) { return; }

        instance.isFadeExcuteTiming = true;
        while (instance.currentTime < executeSecondTime)
        {
            instance.fadeImage.color = new Color(instance.fadeImageColor.r,
                instance.fadeImageColor.g,
                instance.fadeImageColor.b,
                instance.currentTime / executeSecondTime);
            // �K���K�v
            instance.currentTime += Time.deltaTime;
            Debug.LogFormat($"<color=green>[Fade]{instance.currentTime}</color>");
            cancellationToken.ThrowIfCancellationRequested();
            await UniTask.Yield();
        }
        instance.isFadeExcuteTiming = false;
        instance.isFadeAlpha = false;
        instance.currentTime = 0.0f;
        if (finishAct != null) { finishAct(); }
    }

    internal void FadeAlphaChange(bool isFadeAlpha)
    {
        if (isFadeAlpha)
        {
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 0);
        }
        else
        {
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1);
        }
    }

    internal void FadeColorChange(Color color)
    {
        fadeImageColor = color - new Color(0, 0, 0, color.a);
    }

    private void OnDestroy()
    {
        fadeInSource.Cancel();
        fadeOutSource.Cancel();
    }
}
