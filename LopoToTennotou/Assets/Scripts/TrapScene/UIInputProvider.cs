using UniRx;
using UnityEngine;
using System.Collections.Generic;
using System;
using UniRx.Triggers;
using UnityEngine.InputSystem;

public class UIInputProvider : MonoBehaviour
{
    [SerializeField] bool isUseKeyboard = true;
    // [SerializeField]�̓f�o�b�O�p
    [SerializeField] bool uiActionableFlg = true;

    // ����
    private Subject<Unit> decisionButtonSubject = new Subject<Unit>();
    public IObservable<Unit> DecisionButtonObservable => decisionButtonSubject;
    
    // �L�����Z��
    private Subject<Unit> cancelButtonSubject = new Subject<Unit>();
    public IObservable<Unit> CancelButtonObservable => cancelButtonSubject;

    // �X�^�[�g
    private Subject<Unit> startButtonSubject = new Subject<Unit>();
    public IObservable<Unit> StartButtonObservable => startButtonSubject;

    // �Z���N�g
    private Subject<Unit> selectButtonSubject = new Subject<Unit>();
    public IObservable<Unit> SelectButtonObservable => selectButtonSubject;
    
    // �\��
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
            // ����
            if (current.enterKey.wasPressedThisFrame)
            {
                Debug.Log("����");
                decisionButtonSubject.OnNext(Unit.Default);
            }
            // �L�����Z��
            if (current.backspaceKey.wasPressedThisFrame)
            {
                Debug.Log("�L�����Z��");
                cancelButtonSubject.OnNext(Unit.Default); 
            }
            // �X�^�[�g
            if (current.tabKey.wasPressedThisFrame)
            {
                Debug.Log("�X�^�[�g");
                startButtonSubject.OnNext(Unit.Default);
            }
            // �Z���N�g
            if (current.spaceKey.wasPressedThisFrame)
            {
                Debug.Log("�Z���N�g");
                selectButtonSubject.OnNext(Unit.Default);
            }

            // �㉺���E
            Vector2 crossMovement = Vector2.zero;
            if (current.upArrowKey.wasPressedThisFrame)
            {
                Debug.Log("��");
                crossMovement += Vector2.up; 
            }
            if (current.downArrowKey.wasPressedThisFrame)
            {
                Debug.Log("��"); 
                crossMovement += Vector2.down; 
            }
            if (current.leftArrowKey.wasPressedThisFrame)
            {
                Debug.Log("��");
                crossMovement += Vector2.left; 
            }
            if (current.rightArrowKey.wasPressedThisFrame)
            {
                Debug.Log("�E");
                crossMovement += Vector2.right;
            }
            crossMovementProperty.Value += crossMovement;
        });
    }
}