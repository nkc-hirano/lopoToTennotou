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
            // 決定
            if (current.enterKey.isPressed) { Debug.Log("決定"); }
            // キャンセル
            if (current.backspaceKey.isPressed) { Debug.Log("キャンセル"); }
            // スタート
            if (current.tabKey.isPressed) { Debug.Log("スタート"); }
            // セレクト
            if (current.spaceKey.isPressed) { Debug.Log("セレクト"); }

            // 上下左右
            Vector2 crossMovement = Vector2.zero;
            if (current.upArrowKey.isPressed) { crossMovement += Vector2.up; }
            if (current.downArrowKey.isPressed) { crossMovement += Vector2.down; }
            if (current.leftArrowKey.isPressed) { crossMovement += Vector2.left; }
            if (current.rightArrowKey.isPressed) { crossMovement += Vector2.right; }

        });
    }
}
