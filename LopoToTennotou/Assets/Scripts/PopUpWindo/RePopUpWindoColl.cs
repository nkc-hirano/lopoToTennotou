using System;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class RePopUpWindoColl : IDisposable
{
    public float FadeTime => PopUpWindoLogic.inatance.FadeTime;
    public RePopUpWindoColl(GameObject popUpObj)
    {
        Time.timeScale = 0; // ��~
        PopUpWindoLogic.inatance.CreatePopUpWindo();    // �I�u�W�F�N�g�̐���
        PopUpWindoLogic.inatance.SetChild(popUpObj);    // �q�I�u�W�F�N�g�ɐݒ�
        PopUpWindoLogic.inatance.PouUpSummonWind();     // �E�B���h�\��
    }
    public void Dispose()
    {
        DoAsyncDispose().Forget();
    }
    async private UniTask DoAsyncDispose()
    {
        await PopUpWindoLogic.inatance.PouUpRepatriationWind();     // �E�B���h�E��\��
        Time.timeScale = 1; // �N��
    }
}