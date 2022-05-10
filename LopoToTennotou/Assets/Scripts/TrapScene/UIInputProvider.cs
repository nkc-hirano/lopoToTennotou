using UniRx;
using UnityEngine;
using System.Collections.Generic;
using System;
using UniRx.Triggers;
using UnityEngine.InputSystem;

public class UIInputProvider : MonoBehaviour
{
    [SerializeField] bool isUseKeyboard = false;
    // [SerializeField]はデバッグ用
    [SerializeField] bool uiActionableFlg;

    private Subject<Unit> startButtonSubject = new Subject<Unit>();
    public IObservable<Unit> StartButtonObservable => startButtonSubject;

    private void Start()
    {
        var gamepadEnableObservable = this.UpdateAsObservable()
            .Where(unit => !isUseKeyboard)
            .Select(unit => Gamepad.current)
            .Where(current => current != null);

        var keyboardEnableObservable = this.UpdateAsObservable()
            .Where(unit => isUseKeyboard)
            .Select(unit => Keyboard.current)
            .Where(current => current != null)
            .Where(current => uiActionableFlg);

        gamepadEnableObservable.Subscribe(current => { });

        keyboardEnableObservable.Subscribe(current => 
        {
            // 決定
            if (current.enterKey.wasPressedThisFrame) { Debug.Log("決定"); }
            // キャンセル
            if (current.backspaceKey.wasPressedThisFrame) { Debug.Log("キャンセル"); }
            // スタート
            if (current.tabKey.wasPressedThisFrame) { startButtonSubject.OnNext(Unit.Default); }
            // セレクト
            if (current.spaceKey.wasPressedThisFrame) { Debug.Log("セレクト"); }

            // 上下左右
            Vector2 crossMovement = Vector2.zero;
            if (current.upArrowKey.wasPressedThisFrame) { crossMovement += Vector2.up; }
            if (current.downArrowKey.wasPressedThisFrame) { crossMovement += Vector2.down; }
            if (current.leftArrowKey.wasPressedThisFrame) { crossMovement += Vector2.left; }
            if (current.rightArrowKey.wasPressedThisFrame) { crossMovement += Vector2.right; }

        });
    }
}
