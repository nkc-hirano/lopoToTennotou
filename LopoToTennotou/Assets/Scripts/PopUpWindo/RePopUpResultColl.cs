using System;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class RePopUpResultColl : IDisposable
{
    public float FadeTime => PopUpResultLogic.inatance.FadeTime;
    public RePopUpResultColl(GameObject popUpObj)
    {
        Time.timeScale = 0; // ��~
        PopUpResultLogic.inatance.CreatePopUpResultWindo();    // �I�u�W�F�N�g�̐���
        PopUpResultLogic.inatance.SetChild(popUpObj);    // �q�I�u�W�F�N�g�ɐݒ�
        PopUpResultLogic.inatance.PouUpSummonWind();     // �E�B���h�\��
    }
    public void Dispose()
    {
        DoAsyncDispose().Forget();
    }
    async private UniTask DoAsyncDispose()
    {
        await PopUpResultLogic.inatance.PouUpRepatriationWind();     // �E�B���h�E��\��
        Time.timeScale = 1; // �N��
    }
}