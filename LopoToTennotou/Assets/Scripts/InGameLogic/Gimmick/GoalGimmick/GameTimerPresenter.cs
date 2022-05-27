using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MVPModule;
using UniRx;

namespace GameCore
{
public class GameTimerPresenter: MonoPresenterBase<float>
{
        protected override void ObservableConnect()
        {
            base.ObservableConnect();
        }
}
}