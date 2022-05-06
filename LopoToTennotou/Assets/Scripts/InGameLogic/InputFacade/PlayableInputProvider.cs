using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.InputSystem;
using System;

namespace GameCore
{
    public class PlayableInputProvider : MonoBehaviour
    {
        [SerializeField]
        bool isUseKeyboard = false;
        [SerializeField]
        GameObject playerGameObject;

        PlayerCore core;

        private void Start()
        {
            playerGameObject.TryGetComponent(out core);

            var gamepadEnableObservable = this.UpdateAsObservable()
                .Where(unit => !isUseKeyboard)
                .Select(unit => Gamepad.current)
                .Where(current => current != null);

            var keyboardEnableObservable = this.UpdateAsObservable()
                .Where(unit => isUseKeyboard)
                .Select(unit => Keyboard.current)
                .Where(current => current != null);

            gamepadEnableObservable.Subscribe(current =>
            {

            });

            keyboardEnableObservable.Subscribe(current =>
            {
                Vector2 distance = Vector2.zero;

                // 上下
                if (current.wKey.isPressed) { distance += Vector2.up; }
                if (current.sKey.isPressed) { distance += Vector2.down; }
                // 左右   
                if (current.aKey.isPressed) { distance += Vector2.left; }
                if (current.dKey.isPressed) { distance += Vector2.right; }

                core.PositionUpdate(distance);

                // もの押すアクション
                if (current.zKey.isPressed) { core.ArmLeftPowerAdd(); }
                if (current.xKey.isPressed) { core.ArmRightPowerAdd(); }
            });
        }
    }
}
