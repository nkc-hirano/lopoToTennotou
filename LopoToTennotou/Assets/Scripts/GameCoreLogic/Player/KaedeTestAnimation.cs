using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore;
using System;

public class KaedeTestAnimation : MonoBehaviour
{
    // デバッグ用
    public event Action<PlayerStateType> StateChangeEventHandler;
    PlayerStateType state = PlayerStateType.Run;    // 前回のアニメション

    public PlayerStateType PlayerState              // 今回のアニメーション
    {
        // value が受け取った値
        get { return state; }
        set 
        {
            if (state == value) return;         // 前回と一緒なら実行停止
            StateChangeEventHandler(value);     // 今回のアニメーションを実行する
            state = value;                      // 今回のを前回にする
        }
    }

    void Update()
    {
        // デバッグ用
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
