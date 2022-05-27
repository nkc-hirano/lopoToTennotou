using System;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class RePopUpStartStageNumberwindowColl : IDisposable
{
    public float FadeTime => PopUpStartStageNumberwindowLogic.inatance.FadeTime;
    public RePopUpStartStageNumberwindowColl(GameObject popUpObj)
    {
        Time.timeScale = 0; // ��~
        PopUpStartStageNumberwindowLogic.inatance.CreatePopUpStartStageNumberWindo();    // �I�u�W�F�N�g�̐���
        PopUpStartStageNumberwindowLogic.inatance.SetChild(popUpObj);    // �q�I�u�W�F�N�g�ɐݒ�
        PopUpStartStageNumberwindowLogic.inatance.PouUpSummonWind();     // �E�B���h�\��
    }
    public void Dispose()
    {
        DoAsyncDispose().Forget();
    }
    async private UniTask DoAsyncDispose()
    {
        await PopUpStartStageNumberwindowLogic.inatance.PouUpRepatriationWind();     // �E�B���h�E��\��
        Time.timeScale = 1; // �N��
    }
}