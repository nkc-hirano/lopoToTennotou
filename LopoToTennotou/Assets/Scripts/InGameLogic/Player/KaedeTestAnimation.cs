using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore;
using System;

public class KaedeTestAnimation : MonoBehaviour
{
    // �f�o�b�O�p
    public event Action<PlayerStateType> StateChangeEventHandler;
    PlayerStateType state = PlayerStateType.Run;    // �O��̃A�j���V����

    public PlayerStateType PlayerState              // ����̃A�j���[�V����
    {
        // value ���󂯎�����l
        get { return state; }
        set 
        {
            if (state == value) return;         // �O��ƈꏏ�Ȃ���s��~
            StateChangeEventHandler(value);     // ����̃A�j���[�V���������s����
            state = value;                      // ����̂�O��ɂ���
        }
    }

    void Update()
    {
        // �f�o�b�O�p
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayerState = PlayerStateType.Stay;
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayerState = PlayerStateType.Run;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlayerState = PlayerStateType.Action;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            PlayerState = PlayerStateType.Fall;
        }
        if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            PlayerState = PlayerStateType.Stop;
        }
    }
}
