using UniRx;
using UnityEngine;
using UniRx.Triggers;
using UnityEngine.InputSystem;

public class UIInputProvider : MonoBehaviour
{
    [SerializeField] bool isUseKeyboard = false;
    private void Start()
    {
        var gamepadEnableObservable = this.UpdateAsObservable()
            .Where(unit => !isUseKeyboard)
            .Select(unit => Gamepad.current)
            .Where(current => current != null);

        var keyboardEnableObservable = this.UpdateAsObservable()
            .Where(unit => isUseKeyboard)
            .Select(unit => Keyboard.current)
            .Where(current => current != null);

        gamepadEnableObservable.Subscribe(current => { });

        keyboardEnableObservable.Subscribe(current => 
        {
            // ����
            if (current.enterKey.isPressed) { Debug.Log("����"); }
            // �L�����Z��
            if (current.backspaceKey.isPressed) { Debug.Log("�L�����Z��"); }
            // �X�^�[�g
            if (current.tabKey.isPressed) { Debug.Log("�X�^�[�g"); }
            // �Z���N�g
            if (current.spaceKey.isPressed) { Debug.Log("�Z���N�g"); }

            // �㉺���E
            Vector2 crossMovement = Vector2.zero;
            if (current.upArrowKey.isPressed) { crossMovement += Vector2.up; }
            if (current.downArrowKey.isPressed) { crossMovement += Vector2.down; }
            if (current.leftArrowKey.isPressed) { crossMovement += Vector2.left; }
            if (current.rightArrowKey.isPressed) { crossMovement += Vector2.right; }

        });
    }
}
