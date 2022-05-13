using System;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class RePopUpObjColl : IDisposable
{
    public float FadeTime => PopUpObjctLogic.inatance.FadeTime;
    public RePopUpObjColl(GameObject popUpObj)
    {
        Time.timeScale = 0; // ��~
        PopUpObjctLogic.inatance.CreatePopUpObj();    // 
        PopUpObjctLogic.inatance.SetChild(popUpObj);    // �q�I�u�W�F�N�g�ɐݒ�
        PopUpObjctLogic.inatance.PouUpSummonMove();  // �|�b�v�A�b�v�\��
    }
    public void Dispose()
    {
        DoAsyncDispose().Forget();
    }
    async private UniTask DoAsyncDispose()
    {
        await PopUpObjctLogic.inatance.PouUpRepatriationMove();     // �E�B���h�E��\��
        Time.timeScale = 1; // �N��
    }
}