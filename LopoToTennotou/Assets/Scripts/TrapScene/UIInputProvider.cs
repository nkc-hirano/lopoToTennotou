using UniRx;
using System;
using UnityEngine;
using UniRx.Triggers;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class UIInputProvider : MonoBehaviour
{
    [SerializeField] bool isUseKeyboard = true;
    // [SerializeField]はデバッグ用
    [SerializeField] bool uiActionableFlg = true;

    // 決定
    private Subject<Unit> decisionButtonSubject = new Subject<Unit>();
    public IObservable<Unit> DecisionButtonObservable => decisionButtonSubject;
    
    // キャンセル
    private Subject<Unit> cancelButtonSubject = new Subject<Unit>();
    public IObservable<Unit> CancelButtonObservable => cancelButtonSubject;

    // スタート
    private Subject<Unit> startButtonSubject = new Subject<Unit>();
    public IObservable<Unit> StartButtonObservable => startButtonSubject;

    // セレクト
    private Subject<Unit> selectButtonSubject = new Subject<Unit>();
    public IObservable<Unit> SelectButtonObservable => selectButtonSubject;
    
    // 十字
    private ReactiveProperty<Vector2> crossMovementProperty = new ReactiveProperty<Vector2>();
    public IReadOnlyReactiveProperty<Vector2> crossMovementObservable => crossMovementProperty; 

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
            if (current.enterKey.wasPressedThisFrame)
            {
                Debug.Log("決定");
                decisionButtonSubject.OnNext(Unit.Default);
            }
            // キャンセル
            if (current.backspaceKey.wasPressedThisFrame)
            {
                Debug.Log("キャンセル");
                cancelButtonSubject.OnNext(Unit.Default); 
            }
            // スタート
            if (current.tabKey.wasPressedThisFrame)
            {
                Debug.Log("スタート");
                startButtonSubject.OnNext(Unit.Default);
            }
            // セレクト
            if (current.spaceKey.wasPressedThisFrame)
            {
                Debug.Log("セレクト");
                selectButtonSubject.OnNext(Unit.Default);
            }

            // 上下左右
            Vector2 crossMovement = Vector2.zero;
            if (current.upArrowKey.wasPressedThisFrame)
            {
                Debug.Log("上");
                crossMovement += Vector2.up; 
            }
            if (current.downArrowKey.wasPressedThisFrame)
            {
                Debug.Log("下"); 
                crossMovement += Vector2.down; 
            }
            if (current.leftArrowKey.wasPressedThisFrame)
            {
                Debug.Log("左");
                crossMovement += Vector2.left; 
            }
            if (current.rightArrowKey.wasPressedThisFrame)
            {
                Debug.Log("右");
                crossMovement += Vector2.right;
            }
            crossMovementProperty.Value += crossMovement;
        });
    }
}