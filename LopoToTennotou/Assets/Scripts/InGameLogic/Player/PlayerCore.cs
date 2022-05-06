using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace GameCore
{
    // プレイヤーの窓口クラス
    public class PlayerCore : MonoBehaviour
    {
        [SerializeField]
        float addSecondArmPower = 0.0f;
        [SerializeField]
        PlayerStateType initState = PlayerStateType.Stop;

        ReactiveProperty<PlayerStateType> currentState = new ReactiveProperty<PlayerStateType>(PlayerStateType.Run);
        Subject<Vector2> currentDistanceSubject = new Subject<Vector2>();
        ReactiveProperty<float> currentRightArmPower = new ReactiveProperty<float>(0.0f);
        ReactiveProperty<float> currentLeftArmPower = new ReactiveProperty<float>(0.0f);

        public IReadOnlyReactiveProperty<PlayerStateType> readOnlyCurrentState 
            => currentState;
        public IObservable<Vector2> CurrentDistanceObservable
            => currentDistanceSubject;
        public IReadOnlyReactiveProperty<float> readOnlyCurrentRightArmPower 
            => currentRightArmPower;
        public IReadOnlyReactiveProperty<float> readOnlyCurrentLeftArmPower 
            => currentLeftArmPower;

        private void Start()
        {
            currentState.Value = initState;
        }

        public void ArmRightPowerAdd()
        {
            if (currentState.Value == PlayerStateType.Stop) { return; }
            currentRightArmPower.Value = addSecondArmPower * Time.deltaTime;
        }

        public void ArmLeftPowerAdd()
        {
            if (currentState.Value == PlayerStateType.Stop) { return; }
            currentLeftArmPower.Value = addSecondArmPower * Time.deltaTime;
        }

        public void ArmPowerReset()
        {
            float INITIALIZE_VALUE = 0.0f;
            currentRightArmPower.Value = INITIALIZE_VALUE;
            currentLeftArmPower.Value = INITIALIZE_VALUE;
        }

        public void PositionUpdate(Vector2 distance)
        {
            if (currentState.Value == PlayerStateType.Action) { return; }
            if (currentState.Value == PlayerStateType.Stop) { return; }
            currentDistanceSubject.OnNext(distance);
        }
    }
}
