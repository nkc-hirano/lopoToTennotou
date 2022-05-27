using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MVPModule;

namespace GameCore
{
public class GameTimerModel: MonoModelBase<float>
{
	public override void PropertyValueChange(float value) 
	{
		property.Value = value; 
	}
}
}